using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class PictureManager : IPictureService
    {
        readonly IPictureDal _pictureDal;
        public PictureManager(IPictureDal pictureDal)
        {
            _pictureDal = pictureDal;
        }

        public async Task<bool> CreateBlogImagesAsync(int? blogId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                if (images != null)
                {
                    var errors = new List<string>();
                    foreach (var file in images)
                    {
                        var model = new Picture
                        {
                            BlogId = blogId
                        };

                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/blog/multiImage/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Path is preparing: {directoryPath}");
                            Directory.CreateDirectory(directoryPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            model.ImageUrl = fileName;
                            var result = await _pictureDal.AddAsync(model);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error {fileName} : {ex.Message}");
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Blog Images the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyImagesAsync(int? companyId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                if (images != null)
                {
                    var errors = new List<string>();
                    foreach (var file in images)
                    {
                        var model = new Picture
                        {
                            CompanyId = companyId
                        };

                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/images/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Path is preparing: {directoryPath}");
                            Directory.CreateDirectory(directoryPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            model.ImageUrl = fileName;
                            var result = await _pictureDal.AddAsync(model);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error {fileName} : {ex.Message}");
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Images the entity.", ex);
            }
        }

        public async Task<bool> CreateProductImagesAsync(int? productId, IEnumerable<IFormFile> images)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                if (images != null)
                {
                    var errors = new List<string>();
                    foreach (var file in images)
                    {
                        var model = new Picture
                        {
                            ProductId = productId
                        };

                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/product/multiImage/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Path is preparing: {directoryPath}");
                            Directory.CreateDirectory(directoryPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            model.ImageUrl = fileName;
                            var result = await _pictureDal.AddAsync(model);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error {fileName} : {ex.Message}");
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Product Images the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Picture entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _pictureDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _pictureDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _pictureDal.GetAllIncludeAsync(new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingBlogImagesForCompanyUserByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                return _pictureDal.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                     {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                     }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByBlogIdForAdminAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _pictureDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                     {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                     }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                return _pictureDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Picture, bool>>[]
                     {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                     }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingByProductIdForAdminAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Picture, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingCompanyImagesForCompanyUserByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingExplorerBlogDetailImageByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = _pictureDal.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.BlogId==blogId
                }, y => y.Blog);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingExplorerCompanyDetailImageByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _pictureDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyId==companyId
                }, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public IEnumerable<Picture> GetAllIncludingExplorerProductDetailImageByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = _pictureDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ProductId==productId
                }, y => y.Product);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _pictureDal.GetAllIncludeAsync(new Expression<Func<Picture, bool>>[]
                {

                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<IEnumerable<Picture>> GetAllIncludingProductImagesForCompanyUserByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _pictureDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Picture, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Product);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Picture>();
            }
        }

        public async Task<Picture> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _pictureDal.GetIncludeAsync(i => i.Id == id, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.Blog.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public int PictureCounter()
        {
            var result = _pictureDal.PictureCounter();
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _pictureDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _pictureDal.SetDeActiveAsync(id);
            return result;

        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _pictureDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _pictureDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateBlogImageAsync(int? blogId, int id, IFormFile image)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "Product Id was null");

                if (image != null)
                {
                    var errors = new List<string>();
                    var entity = new Picture
                    {
                        BlogId = blogId,
                        Id = id
                    };
                    if (entity != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/blog/multiImage/");
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
                            entity.ImageUrl = fileName;
                            entity.UpdatedDate = DateTime.Now.ToLocalTime();
                            var result = await _pictureDal.UpdateAsync(entity);
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
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Blog Image the entity.", ex);
            }
        }

        public async Task<bool> UpdateCompanyImageAsync(int? companyId, int id, IFormFile image)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "Company Id was null");

                if (image != null)
                {
                    var errors = new List<string>();
                    var entity = new Picture
                    {
                        CompanyId = companyId,
                        Id = id
                    };
                    if (entity != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/images/");
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
                            entity.ImageUrl = fileName;
                            entity.UpdatedDate = DateTime.Now.ToLocalTime();
                            var result = await _pictureDal.UpdateAsync(entity);
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
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Company Image the entity.", ex);
            }
        }

        public async Task<bool> UpdateProductImageAsync(int? productId, int id, IFormFile image)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "Product Id was null");

                if (image != null)
                {
                    var errors = new List<string>();
                    var entity = new Picture
                    {
                        ProductId = productId,
                        Id = id
                    };
                    if (entity != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/product/multiImage/");
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
                            entity.ImageUrl = fileName;
                            entity.UpdatedDate = DateTime.Now.ToLocalTime();
                            var result = await _pictureDal.UpdateAsync(entity);
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
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating Product Image the entity.", ex);
            }
        }
    }
}