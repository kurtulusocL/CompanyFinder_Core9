using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyPartnershipManager : ICompanyPartnershipService
    {
        readonly ICompanyPartnershipDal _companyPartnershipDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyPartnershipManager(ICompanyPartnershipDal companyPartnershipDal, IHttpContextAccessor httpContextAccessor)
        {
            _companyPartnershipDal = companyPartnershipDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, int? companyId, int productCategoryId, string userId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }

                var entity = new CompanyPartnership
                {
                    Title = title,
                    Detail = detail,
                    Desc = desc,
                    Price = price,
                    StartDate = startDate,
                    ExpiryDate = expiryDate,
                    CompanyId = companyId,
                    ProductCategoryId = productCategoryId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _companyPartnershipDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyPartnership entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyPartnershipDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyPartnershipDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingAvailableAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAvailable==true
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingNotAvailableAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAvailable==false
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyId==companyId
                }, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByExpiryDateAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderBy(i => i.ExpiryDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByProductCategoryIdAsync(int productCategoryId)
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(productCategoryId, "ProductCategoryId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ProductCategoryId==productCategoryId
                }, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.UserId==userId
                }, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                {

                    i=>i.UserId==userId
                }, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingByStartDateAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAvailable==true
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.StartDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {

                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<CompanyPartnership> GetByIdAsync(int? id)
        {
            try
            {
                return await _companyPartnershipDal.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the company partnership.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyPartnershipDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetAvailablePartnership(int id)
        {
            var result = await _companyPartnershipDal.SetAvailablePartnership(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyPartnershipDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyPartnershipDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotAvailablePartnership(int id)
        {
            var result = await _companyPartnershipDal.SetNotAvailablePartnership(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyPartnershipDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, bool isAvailable, int? companyId, int productCategoryId, string userId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }

                var entity = new CompanyPartnership
                {
                    Title = title,
                    Detail = detail,
                    Desc = desc,
                    Price = price,
                    StartDate = startDate,
                    ExpiryDate = expiryDate,
                    IsAvailable = isAvailable,
                    CompanyId = companyId,
                    ProductCategoryId = productCategoryId,
                    UserId = userId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _companyPartnershipDal.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingSavedPartnershipsForSaveContentPartnershipByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingPartnershipLikesforCompanyByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, y => y.Likes);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingPartnershipHitsforCompanyByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, y => y.Hits);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public IEnumerable<CompanyPartnership> GetAllIncludingCompanyPartnershipRandom()
        {
            try
            {
                return _companyPartnershipDal.GetAllInclude(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ExpiryDate<DateTime.Now
                }, null, y => y.Company).OrderByDescending(i => Guid.NewGuid()).Take(7).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingSearchResultasync(string key)
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Title.Contains(key)||i.Desc.Contains(key))
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingAdvancedSearchResultAsync(string key)
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Title.Contains(key)||i.Desc.Contains(key)||i.ProductCategory.Name.Contains(key))
                }, null, y => y.Company, y => y.Company.City, y => y.Company.Country, y => y.ProductCategory, y => y.User, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostLikedPartnershipAsync()
        {
            try
            {
                var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>1
                }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory, y => y.Company, y => y.Company.City, y => y.Company.Country);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostHitPartnershipAsync()
        {
            try
            {
                try
                {
                    var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>1
                    }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory, y => y.Company, y => y.Company.City, y => y.Company.Country);
                    return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
                }
                catch (Exception)
                {
                    return new List<CompanyPartnership>();
                }
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingMostSavedPartnershipAsync()
        {
            try
            {
                try
                {
                    var result = await _companyPartnershipDal.GetAllIncludeAsync(new Expression<Func<CompanyPartnership, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>1
                    }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory, y => y.Company, y => y.Company.City, y => y.Company.Country);
                    return result.OrderByDescending(i => i.SavedContents.Count()).Take(350).ToList();
                }
                catch (Exception)
                {
                    return new List<CompanyPartnership>();
                }
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostLikedPartnershipByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                try
                {
                    var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>1
                    }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory);
                    return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
                }
                catch (Exception)
                {
                    return new List<CompanyPartnership>();
                }
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostHitPartnershipByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                try
                {
                    var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>1
                    }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory);
                    return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
                }
                catch (Exception)
                {
                    return new List<CompanyPartnership>();
                }
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }

        public async Task<IEnumerable<CompanyPartnership>> GetAllIncludingYourMostSavedPartnershipByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                try
                {
                    var result = await _companyPartnershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyPartnership, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>1
                    }, null, y => y.Likes, y => y.Hits, y => y.SavedContents, y => y.ProductCategory);
                    return result.OrderByDescending(i => i.SavedContents.Count()).Take(350).ToList();
                }
                catch (Exception)
                {
                    return new List<CompanyPartnership>();
                }
            }
            catch (Exception)
            {
                return new List<CompanyPartnership>();
            }
        }
    }
}
