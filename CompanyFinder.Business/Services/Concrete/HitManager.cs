using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class HitManager : IHitService
    {
        readonly IHitDal _hitDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public HitManager(IHitDal hitDal, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _hitDal = hitDal;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public Hit AdHit(int? adId, string userId, int currentValue)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "adId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.AdId == adId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        AdId = adId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };

                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit BlogHit(int? blogId, string userId, int currentValue)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.BlogId == blogId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        BlogId = blogId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };

                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyHit(int? companyId, string userId, int currentValue)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "company Id was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.CompanyId == companyId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyId = companyId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };

                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit ProductHit(int? productId, string userId, int currentValue)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "product Id was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.CompanyId == productId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        ProductId = productId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };

                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Hit entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _hitDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _hitDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _hitDal.GetAllIncludeAsync(new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByAdIdAsync(int? adId)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "adId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(adId, "AdId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(commentAnswerId, "CommentAnswerId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(commentId, "CommentId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(companyFormId, "CompanyFormId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Value!=null,
                    i=>i.UserId==userId
                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Hit, bool>>[]
                {

                }, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByValueAsync()
        {
            try
            {
                var result = await _hitDal.GetAllIncludeAsync(new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.Value).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _hitDal.GetAllIncludeAsync(new Expression<Func<Hit, bool>>[]
                {

                }, null, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<Hit> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _hitDal.GetIncludeAsync(i => i.Id == id, y => y.Ad, y => y.Blog, y => y.Company, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.SectorNews);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _hitDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _hitDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _hitDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _hitDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingCompanyHitsPeopleByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingProductHitsPeopleByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingBlogHitsPeopleByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public Hit CompanyPartnershipHit(int? companyPartnershipId, string userId, int currentValue)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.CompanyPartnershipId == companyPartnershipId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyPartnershipId = companyPartnershipId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };

                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyFormHit(int? companyFormId, string userId, int currentValue)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.CompanyFormId == companyFormId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyFormId = companyFormId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };
                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit SectorNewsHit(int? sectorNewsId, string userId, int currentValue)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                var existingHit = _context.Hits.FirstOrDefault(d => d.UserId == userId && d.SectorNewsId == sectorNewsId);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        SectorNewsId = sectorNewsId,
                        UserId = string.IsNullOrEmpty(userId) ? "VISITOR" : userId,
                        Value = currentValue + 1,
                    };
                    _context.Hits.Add(newHit);
                    _context.SaveChanges();
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingPartnershipHitsPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var result = await _hitDal.GetAllIncludeByIdAsync(sectorNewsId, "", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }
    }
}
