using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ProductManager : IProductService
    {
        readonly IProductDal _productDal;
        readonly IProductCategoryService _productCategoryService;
        readonly IProductSubcategoryService _productSubcategoryService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public ProductManager(IProductDal productDal, IProductCategoryService productCategoryService, IProductSubcategoryService productSubcategoryService, IHttpContextAccessor httpContextAccessor)
        {
            _productDal = productDal;
            _productCategoryService = productCategoryService;
            _productSubcategoryService = productSubcategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SelectListItem>> ProductSelectSystem(int? productCategoryId, string tip)
        {
            try
            {
                var result = new List<SelectListItem>();

                switch (tip)
                {
                    case "getProductCategories":
                        var productCategories = await _productCategoryService.GetAllIncludingForAddProductAsync();
                        result = productCategories.Select(productCategory => new SelectListItem
                        {
                            Text = productCategory.Name,
                            Value = productCategory.Id.ToString()
                        }).ToList();
                        break;

                    case "getProductSubcategories":
                        if (productCategoryId == null)
                        {
                            throw new ArgumentNullException(nameof(productCategoryId), "Product Category ID can not be empty.");
                        }

                        var productSubcategories = await _productSubcategoryService.GetAllIncludingForAddProductByCategoryIdAsync(productCategoryId.Value);
                        result = productSubcategories.Select(productSubcategory => new SelectListItem
                        {
                            Text = productSubcategory.Name,
                            Value = productSubcategory.Id.ToString()
                        }).ToList();
                        break;

                    default:
                        throw new ArgumentException($"Unsupported type: {tip}");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error", ex);
            }
        }

        public async Task<bool> CreateAsync(string name, string desc, string? otherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/product/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var entity = new Product
                        {
                            Name = name,
                            Desc = desc,
                            OtherText = otherText,
                            Price = price,
                            IsCommentable = isCommentable,
                            IsAvailable = isAvailable,
                            IsQuestionable = isQuestionable,
                            AvailableDate = AvailableDate,
                            ProductCategoryId = productCategoryId,
                            ProductSubcategoryId = productSubcategoryId,
                            CompanyId = companyId,
                            UserId = userId,
                        };

                        entity.ImageUrl = fileName;
                        var result = await _productDal.AddAsync(entity);
                        if (!result)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Product entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _productDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _productDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Product, bool>>[]
                {

                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = await _productDal.GetAllIncludeByIdAsync(categoryId, "ProductCategoryId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingBySubcategoryIdAsync(int? subcategoryId)
        {
            try
            {
                if (subcategoryId == null)
                    throw new ArgumentNullException(nameof(subcategoryId), "subcategoryId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(subcategoryId, "ProductSubcategoryId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {

                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingQuestionableProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsQuestionable==true
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingNotQuestionableProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsQuestionable==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingAvailableProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAvailable==true
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingNotAvailebleProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAvailable==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingCommentableProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==true
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }
        public async Task<IEnumerable<Product>> GetAllIncludingNotCommentableProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }
        public async Task<IEnumerable<Product>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {

                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _productDal.GetIncludeAsync(i => i.Id == id, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.Reports, y => y.SaveContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetAvailableAsync(int id)
        {
            var result = await _productDal.SetAvailableAsync(id);
            return result;
        }

        public async Task<bool> SetNotAvailableAsync(int id)
        {
            var result = await _productDal.SetNotAvailableAsync(id);
            return result;
        }

        public async Task<bool> SetQuestionableAsync(int id)
        {
            var result = await _productDal.SetQuestionableAsync(id);
            return result;
        }

        public async Task<bool> SetNotQuestionableAsync(int id)
        {
            var result = await _productDal.SetNotQuestionableAsync(id);
            return result;
        }

        public async Task<bool> SetCommentableAsync(int id)
        {
            var result = await _productDal.SetCommentableAsync(id);
            return result;
        }

        public async Task<bool> SetNotCommentableAsync(int id)
        {
            var result = await _productDal.SetNotCommentableAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _productDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _productDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _productDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _productDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string name, string desc, string? otherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, int id, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/product/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var entity = new Product
                        {
                            Name = name,
                            Desc = desc,
                            OtherText = otherText,
                            Price = price,
                            IsCommentable = isCommentable,
                            IsAvailable = isAvailable,
                            IsQuestionable = isQuestionable,
                            AvailableDate = AvailableDate,
                            ProductCategoryId = productCategoryId,
                            ProductSubcategoryId = productSubcategoryId,
                            CompanyId = companyId,
                            UserId = userId,
                            Id = id,
                        };

                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        var results = await _productDal.UpdateAsync(entity);
                        if (!results)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public IEnumerable<Product> GetAllIncludingProductsForAdminHeader()
        {
            try
            {
                return _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public int ProductCounter()
        {
            var result = _productDal.ProductCounter();
            return result;
        }

        public IEnumerable<Product> GetAllSiteMap()
        {
            try
            {
                var result = _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public IEnumerable<Product> GetAllIncludingCompanyProductsByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");


                var result = _productDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.ProductCategory, y => y.ProductSubcategory);
                return result.OrderByDescending(i => i.CreatedDate).Take(6).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public Product GetIncludingProductForHeaderByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _productDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Product GetIncludingProductByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "id was null");

                return _productDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.Id == companyId, y => y.Company, y => y.Company.City, y => y.Company.Country);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Product GetIncludingProductById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return _productDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IEnumerable<Product> GetAllIncludingRandomProducts()
        {
            try
            {
                return _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsAvailable==true,
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company).OrderByDescending(i => Guid.NewGuid()).Take(10).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingSearchResultAsync(string key)
        {
            try
            {
                if (!String.IsNullOrEmpty(key))
                {
                    var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                    {
                        i=>i.IsAvailable==true,
                        i=>i.IsDeleted==false,
                        i=>i.Name.Contains(key),
                    }, y => y.Company, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Hits, y => y.Likes, y => y.SaveContents, y => y.Questions);
                    return result.OrderByDescending(i => i.CreatedDate).ToList();
                }
                else
                {
                    throw new ArgumentNullException(nameof(key), "key was null");
                }
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingProductAdvancedSearchResultAsync(string key)
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key), "key was null");

                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsAvailable==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Name.Contains(key)||i.ProductCategory.Name.Contains(key)||i.ProductSubcategory.Name.Contains(key))
                }, null, y => y.Company, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Hits, y => y.Likes, y => y.SaveContents, y => y.Questions);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public IEnumerable<Product> GetAllIncludingProductsForExplorer()
        {
            try
            {
                return _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsAvailable==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Questions).OrderByDescending(i => Guid.NewGuid()).Take(12).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public IEnumerable<Product> GetAllIncludingLastProductsForExplorer()
        {
            try
            {
                return _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsAvailable==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Questions).OrderByDescending(i => i.CreatedDate).Take(8).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingForPublicUserAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Comments, y => y.Questions, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => Guid.NewGuid()).Take(18).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public IEnumerable<Product> GetAllIncludingRandomProductForHome()
        {
            try
            {
                return _productDal.GetAllInclude(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsAvailable==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Questions, y => y.Hits, y => y.Likes, y => y.SaveContents).OrderByDescending(i => Guid.NewGuid()).Take(12).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostLikedProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostHitProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostSavedProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostQuestionProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Questions.Count()>0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Questions.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostCommentProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Comments.Count()>0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostExpensiveProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Price!=null&&i.Price.Value!=0
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Price).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingMostCheapperProductsAsync()
        {
            try
            {
                var result = await _productDal.GetAllIncludeAsync(new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Price!=null
                }, null, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderBy(i => i.Price).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostLikedProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.Likes.Count()>0,
                 i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }

        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostHitProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.Hits.Count()>0,
                 i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostSavedProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.SaveContents.Count()>0,
                 i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostQuestionProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.Questions.Count()>0,
                 i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Questions.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostCommentProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.Comments.Count()>0,
                 i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostExpensiveProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Price!=null&&i.Price.Value!=0,
                     i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Price).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingYourMostCheapperProductsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Price!=null,
                    i=>i.UserId==userId
                }, y => y.ProductCategory, y => y.ProductSubcategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Questions, y => y.SaveContents);
                return result.OrderBy(i => i.Price).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingSavedCompanyProductsForSaveContentProductByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, y => y.SaveContents, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingProductLikesForCompanyByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, y => y.Likes);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingProductHitForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _productDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Product, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, y => y.Hits);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsAsync()
        {
            return await _productDal.GetAllIncludingWeeklyPopularProductsAsync();
        }

        public async Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsForAdminAsync()
        {
            return await _productDal.GetAllIncludingWeeklyPopularProductsForAdminAsync();
        }
    }
}