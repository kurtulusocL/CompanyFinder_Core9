using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyManager : ICompanyService
    {
        readonly ICompanyDal _companyDal;
        readonly ICompanyCategoryService _companyCategoryService;
        readonly ICompanySubcategoryService _companySubcategoryService;
        readonly ICityService _cityService;
        readonly ICountryService _countryService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyManager(ICompanyDal companyDal, ICompanyCategoryService companyCategoryService, ICompanySubcategoryService companySubcategoryService, ICityService cityService, ICountryService countryService, IHttpContextAccessor httpContextAccessor)
        {
            _companyDal = companyDal;
            _companyCategoryService = companyCategoryService;
            _companySubcategoryService = companySubcategoryService;
            _cityService = cityService;
            _countryService = countryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<SelectListItem>> CategorySelectSystem(int? companyCategoryId, string tip)
        {
            try
            {
                var result = new List<SelectListItem>();

                switch (tip)
                {
                    case "getCompanyCategories":
                        var companyCategories = await _companyCategoryService.GetAllIncludingForAddCompanyAsync();
                        result = companyCategories.Select(companyCategory => new SelectListItem
                        {
                            Text = companyCategory.Name,
                            Value = companyCategory.Id.ToString()
                        }).ToList();
                        break;

                    case "getCompanySubcategories":
                        if (companyCategoryId == null)
                        {
                            throw new ArgumentNullException(nameof(companyCategoryId), "Company Category ID can not be empty.");
                        }

                        var companySubcategories = await _companySubcategoryService.GetAllIncludingForAddCompanyByCategoryIdAsync(companyCategoryId.Value);
                        result = companySubcategories.Select(companySubcategory => new SelectListItem
                        {
                            Text = companySubcategory.Name,
                            Value = companySubcategory.Id.ToString()
                        }).ToList();
                        break;

                    default:
                        throw new ArgumentException($"Unsupported type: {tip}");
                }

                return result;
            }
            catch (Exception)
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<List<SelectListItem>> LocationSelectSystem(int? id, string tip)
        {
            try
            {
                var result = new List<SelectListItem>();

                switch (tip)
                {
                    case "getCompanyCountries":
                        var companyCountries = await _countryService.GetAllIncludingForAddCompanyAsync();
                        result = companyCountries.Select(companyCountry => new SelectListItem
                        {
                            Text = companyCountry.Name,
                            Value = companyCountry.Id.ToString()
                        }).ToList();
                        break;

                    case "getCompanyCities":
                        if (id == null)
                        {
                            throw new ArgumentNullException(nameof(id), "Country ID can not be empty.");
                        }

                        var companyCities = await _cityService.GetAllIncludingForAddCompanyAsync(id.Value);
                        result = companyCities.Select(companyCity => new SelectListItem
                        {
                            Text = companyCity.Name,
                            Value = companyCity.Id.ToString()
                        }).ToList();
                        break;

                    default:
                        throw new ArgumentException($"Unsupported type: {tip}");
                }

                return result;
            }
            catch (Exception)
            {
                return new List<SelectListItem>();
            }
        }

        public async Task<bool> CreateAsync(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/logo/");
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

                        var entity = new Company
                        {
                            Name = name,
                            Desc = desc,
                            FoundationDate = foundationDate,
                            Slogan = slogan,
                            WebsiteUrl = websiteUrl,
                            IsCommentable = isCommentable,
                            CountryId = countryId,
                            CityId = cityId,
                            CompanyCategoryId = companyCategoryId,
                            CompanySubcategoryId = companySubcategoryId,
                            UserId = userId,
                        };
                        entity.Logo = fileName;
                        var result = await _companyDal.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(Company entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = await _companyDal.GetAllIncludeByIdAsync(categoryId, "CompanyCategoryId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByCityIdAsync(int? cityId)
        {
            try
            {
                if (cityId == null)
                    throw new ArgumentNullException(nameof(cityId), "cityId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(cityId, "CityId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByCommentablesAsync()
        {
            try
            {                
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==true
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByCountryIdAsync(int countryId)
        {
            try
            {
                var result = await _companyDal.GetAllIncludeByIdAsync(countryId, "CountryId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByFoundationDateAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.FoundationDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByNotCommentablesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingBySubcategoryIdAsync(int? subcategoryId)
        {
            try
            {
                if (subcategoryId == null)
                    throw new ArgumentNullException(nameof(subcategoryId), "subcategoryId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(subcategoryId, "CompanySubcategoryId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {

                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {

                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<Company> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companyDal.GetIncludeAsync(i => i.Id == id, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.User, y => y.Appointments, y => y.Blogs, y => y.CompanyContacts, y => y.CompanyMessages, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Products, y => y.Questions, y => y.Reports, y => y.SaveContents, y => y.CompanyPartnerships, y => y.CompanyForms);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<bool> SetCommentableAsync(int id)
        {
            var result = await _companyDal.SetCommentableAsync(id);
            return result;
        }
        public async Task<bool> SetNotCommentableAsync(int id)
        {
            var result = await _companyDal.SetNotCommentableAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var active = await _companyDal.SetActiveAsync(id);
            return active;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var deActive = await _companyDal.SetDeActiveAsync(id);
            return deActive;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var deleted = await _companyDal.SetDeletedAsync(id);
            return deleted;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var notDeleted = await _companyDal.SetNotDeletedAsync(id);
            return notDeleted;
        }

        public async Task<bool> UpdateAsync(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, int id, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/logo/");
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

                        var entity = new Company
                        {
                            Name = name,
                            Desc = desc,
                            FoundationDate = foundationDate,
                            Slogan = slogan,
                            WebsiteUrl = websiteUrl,
                            IsCommentable = isCommentable,
                            CountryId = countryId,
                            CityId = cityId,
                            CompanyCategoryId = companyCategoryId,
                            CompanySubcategoryId = companySubcategoryId,
                            UserId = userId,
                            Id = id
                        };
                        entity.Logo = fileName;
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        var result = await _companyDal.UpdateAsync(entity);
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

        public Company GetCompanyLogo(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "id was null");

                return _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the logo.", ex);
            }
        }

        public int CompanyCounter()
        {
            var result = _companyDal.CompanyCounter();
            return result;
        }

        public IEnumerable<Company> GetAllIncludingLastCompaniesForAdminHome()
        {
            try
            {
                return _companyDal.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.City, y => y.Country, y => y.User).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingLastCompaniesForAdminHeader()
        {
            try
            {
                DateTime now = DateTime.UtcNow;
                DateTime last24Hours = now.AddDays(-1);

                return _companyDal.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.City, y => y.Country, y => y.User).OrderByDescending(i => i.CreatedDate >= last24Hours && i.CreatedDate <= now).Take(15).ToList();
            }
            catch (Exception)
            {

                return new List<Company>();
            }
        }

        public Company GetIncludingCompaniesByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Products, y => y.Pictures, y => y.Blogs, y => y.Customers);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Company GetIncludeCompanyForCompanyUserLogoByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<Company> GetIncludingCompanyForCompanyUserByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companyDal.GetIncludeAsync(i => i.Id == id, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents, y => y.Pictures, y => y.CompanyContacts);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompaniesForCompanyUserAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Blogs, y => y.Hits, y => y.Likes, y => y.Products, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingRandomCompanies()
        {
            try
            {
                return _companyDal.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.City, y => y.Country, y => y.CompanyCategory, y => y.CompanySubcategory).OrderByDescending(i => Guid.NewGuid()).Take(10).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public Company GetCompanyById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.Id == id, y => y.Comments);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingSearchResultAsync(string key)
        {
            try
            {
                if (!String.IsNullOrEmpty(key))
                {
                    var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                    {
                        i=>i.IsActive==true,
                        i=>i.IsDeleted==false,
                        i=>i.Name.Contains(key)
                    }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Hits, y => y.Likes, y => y.SaveContents);
                    return result.OrderByDescending(i => i.CreatedDate).ToList();
                }
                else
                {
                    throw new ArgumentNullException(nameof(key), "key was null");
                }
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompanyAdvancedSearchResultAsync(string key)
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));

                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Name.Contains(key)||i.City.Name.Contains(key)||i.Country.Name.Contains(key)||i.CompanyCategory.Name.Contains(key)||i.CompanySubcategory.Name.Contains(key))
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<Company> GetCompanyByUserIdAsync(string userId)
        {
            try
            {
                return await _companyDal.GetAsync(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IEnumerable<Company> GetAllIncludingCompaniesForExplorer()
        {
            try
            {
                return _companyDal.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Products).OrderByDescending(i => Guid.NewGuid()).Take(12).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingLastCompaniesForExplorer()
        {
            try
            {
                return _companyDal.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Products).OrderByDescending(i => i.CreatedDate).Take(8).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public Company GetIncludingCompanyForHeaderByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Company GetIncludingCompanyForCompanyUserCounter(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId, y => y.Products, y => y.Blogs, y => y.Pictures, y => y.Customers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostLikedCompaniesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostHitCompaniesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostSavedCompaniesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostAppointmentCompaniesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Appointments.Count()>0
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Appointments.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostCommentCompaniesAsync()
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Comments.Count()>0
                }, null, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingYourMostLikedCompaniesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingYourMostHitCompaniesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingYourMostSavedCompaniesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingYourMostAppointmentCompaniesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Appointments.Count()>0
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Appointments.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingYourMostCommentCompaniesAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Comments.Count()>0
                }, y => y.Country, y => y.City, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompanyMatchResultsAsync(string key)
        {
            try
            {
                var result = await _companyDal.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0,
                    i=>i.Reports.Count()==0,
                    i=>i.CompanyContacts.Count()>0,
                    i=>i.Appointments.Count()>0,
                    i=>i.Likes.Count()>0,
                    (i=>i.CompanyCategory.Name.Contains(key)||!i.CompanySubcategory.Name.Contains(key)||i.Country.Name.Contains(key)||!i.City.Name.Contains(key))
                }, null, y => y.Products, y => y.Reports, y => y.CompanyContacts, y => y.Appointments, y => y.Likes, y => y.Hits, y => y.SaveContents, y => y.CompanyCategory, y => y.CompanySubcategory, y => y.Country, y => y.City);
                return result.OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public Company GetCompanyLogoForCommentByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompanyLikesForCompanyByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, y => y.Likes);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompanyHitForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, y => y.Hits);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingSavedCompaniesForSaveContentCompanyByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, y => y.SaveContents, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesAsync()
        {
            return await _companyDal.GetAllIncludingWeeklyPopularCompaniesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesFortAdminAsync()
        {
            return await _companyDal.GetAllIncludingWeeklyPopularCompaniesFortAdminAsync();
        }

        public Company GetCompanyInformationForCommentByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
    }
}
