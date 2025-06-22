using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.Business.Services.Concrete
{
    public class LikeManager : ILikeService
    {
        readonly ILikeDal _likeDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public LikeManager(ILikeDal likeDal, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _likeDal = likeDal;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> DeleteAsync(Like entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _likeDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _likeDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingBlogLikesPeopleByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingCompanyLikesPeopleByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId Id was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.User, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingProductLikesPeopleByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId Id was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _likeDal.GetAllIncludeAsync(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "Blog Id was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByBlogIdForAdminAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "Blog Id was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Like, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Like, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByLikeValueAscAsync()
        {
            try
            {
                var result = await _likeDal.GetAllIncludeAsync(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Value>0
                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderBy(i => i.Value).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByLikeValueDescAsync()
        {
            try
            {
                var result = await _likeDal.GetAllIncludeAsync(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.Value).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyFormId, "CompanyFormId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByProductIdForAdminAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Like, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Value!=null,
                    i=>i.UserId==userId
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Like, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _likeDal.GetAllIncludeAsync(new Expression<Func<Like, bool>>[]
                {

                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<Like> GetByIdAsync(int? id)
        {
            try
            {

                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _likeDal.GetIncludeAsync(i => i.Id == id, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> LikeBlogAsync(int? blogId, string userId, int currentValue)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.BlogId == blogId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        BlogId = blogId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> LikeCompanyAsync(int? companyId, string userId, int currentValue)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.CompanyId == companyId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CompanyId = companyId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> LikeProductAsync(int? productId, string userId, int currentValue)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.ProductId == productId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        ProductId = productId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> LikeCompanyPartnershipAsync(int? companyPartnershipId, string userId, int currentValue)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.CompanyPartnershipId == companyPartnershipId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CompanyPartnershipId = companyPartnershipId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> LikeCompanyFormAsync(int? companyFormId, string userId, int currentValue)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.CompanyFormId == companyFormId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CompanyFormId = companyFormId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> LikeSectorNewsAsync(int? sectorNewsId, string userId, int currentValue)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var existingLike = await _context.Likes.FirstOrDefaultAsync(d => d.UserId == userId && d.SectorNewsId == sectorNewsId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        SectorNewsId = sectorNewsId,
                        UserId = userId,
                        Value = currentValue + 1,
                    };
                    await _context.Likes.AddAsync(newLike);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _likeDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _likeDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _likeDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _likeDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<IEnumerable<Like>> GetAllIncludingPartnershipLikesPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId Id was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.User, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }

        public async Task<IEnumerable<Like>> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var result = await _likeDal.GetAllIncludeByIdAsync(sectorNewsId, "SectorNewsId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }
    }
}
