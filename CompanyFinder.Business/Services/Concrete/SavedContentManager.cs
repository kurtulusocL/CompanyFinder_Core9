using System.ComponentModel.Design;
using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.Business.Services.Concrete
{
    public class SavedContentManager : ISavedContentService
    {
        readonly ISavedContentDal _savedContentDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public SavedContentManager(ISavedContentDal savedContentDal, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _savedContentDal = savedContentDal;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> DeleteAsync(SavedContent entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _savedContentDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    return await _savedContentDal.DeleteAsync(data);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _savedContentDal.GetAllIncludeAsync(new Expression<Func<SavedContent, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.IsSaved==true
                }, null, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingByNotSavedAsync()
        {
            try
            {
                var result = await _savedContentDal.GetAllIncludeAsync(new Expression<Func<SavedContent, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.IsSaved==false
                }, null, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _savedContentDal.GetAllIncludeAsync(new Expression<Func<SavedContent, bool>>[] {
                i=>i.IsSaved==true
                }, null, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingNotSavedByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsSaved==false
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }
        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "productId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.SavedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<SavedContent> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _savedContentDal.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> NotSaveAsync(bool isSaved, int? companyId, int? companyPartnershipId, int? productId, int? blogId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                if (companyId != null || productId != null || blogId != null || companyPartnershipId != null)
                {
                    if (companyId != null)
                    {
                        var savedCompany = await _context.Set<Company>().Where(i => i.Id == companyId).FirstOrDefaultAsync();
                        if (savedCompany != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                CompanyId = savedCompany.Id,
                                UserId = userId,
                                IsSaved = false,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    }
                    if (productId != null)
                    {
                        var savedProduct = await _context.Set<Product>().Where(i => i.Id == productId).FirstOrDefaultAsync();
                        if (savedProduct != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                ProductId = savedProduct.Id,
                                UserId = userId,
                                IsSaved = false,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    }
                    if (blogId != null)
                    {
                        var savedBlog = await _context.Set<Blog>().Where(i => i.Id == blogId).FirstOrDefaultAsync();
                        if (savedBlog != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                BlogId = savedBlog.Id,
                                UserId = userId,
                                IsSaved = false,
                                SavedDate = DateTime.Now.ToLocalTime()
                            };
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    }
                    if (companyPartnershipId != null)
                    {
                        var savedCompanyPartnership = await _context.Set<CompanyPartnership>().Where(i => i.Id == companyPartnershipId).FirstOrDefaultAsync();
                        if (savedCompanyPartnership != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                CompanyPartnershipId = savedCompanyPartnership.Id,
                                UserId = userId,
                                IsSaved = false,
                                SavedDate = DateTime.Now.ToLocalTime()
                            };
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while cancelling of saved the entity.", ex);
            }
        }

        public async Task<bool> SaveAsync(bool isSaved, int? companyId, int? productId, int? blogId, int? companyPartnershipId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                if (companyId != null || productId != null || blogId != null || companyPartnershipId != null)
                {
                    if (companyId != null)
                    {
                        var savedCompany = await _context.Set<Company>().Where(i => i.Id == companyId).FirstOrDefaultAsync();
                        if (savedCompany != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                CompanyId = savedCompany.Id,
                                UserId = userId,
                                IsSaved = true,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveContents.AddAsync(model);
                            _context.Entry(model).State = EntityState.Added;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                    if (productId != null)
                    {
                        var savedProduct = await _context.Set<Product>().Where(i => i.Id == productId).FirstOrDefaultAsync();
                        if (savedProduct != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                ProductId = savedProduct.Id,
                                UserId = userId,
                                IsSaved = true,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveContents.AddAsync(model);
                            _context.Entry(model).State = EntityState.Added;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                    if (blogId != null)
                    {
                        var savedBlog = await _context.Set<Blog>().Where(i => i.Id == blogId).FirstOrDefaultAsync();
                        if (savedBlog != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                BlogId = savedBlog.Id,
                                UserId = userId,
                                IsSaved = true,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveContents.AddAsync(model);
                            _context.Entry(model).State = EntityState.Added;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                    if (companyPartnershipId != null)
                    {
                        var savedCompanyPartnership = await _context.Set<CompanyPartnership>().Where(i => i.Id == companyPartnershipId).FirstOrDefaultAsync();
                        if (savedCompanyPartnership != null)
                        {
                            SavedContent model = new SavedContent
                            {
                                CompanyPartnershipId = savedCompanyPartnership.Id,
                                UserId = userId,
                                IsSaved = true,
                                SavedDate = DateTime.Now.ToLocalTime(),
                            };
                            await _context.SaveContents.AddAsync(model);
                            _context.Entry(model).State = EntityState.Added;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _savedContentDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _savedContentDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _savedContentDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _savedContentDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedBlogContentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Blog, y => y.Blog.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedCompanyContentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true
                }, y => y.Company, y => y.Company.City, y => y.Company.Country);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedProductContentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSaved==true,
                    i=>i.Product.CompanyId!=null,
                    i=>i.Product.Company.CityId!=null
                }, y => y.Product, y => y.Product.Company, y => y.Product.Company.City, y => y.Product.Company.Country);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedBlogsPeopleByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.BlogId==blogId
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedCompaniesPeopleByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyId==companyId
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedProductsPeopleByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ProductId==productId
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedPartnershipContentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.UserId==userId
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedPartnershipPeopleByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyPartnershipId==companyPartnershipId
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SavedContent>();
            }
        }

        public async Task<IEnumerable<SavedContent>> GetAllIncludingSavedByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _savedContentDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<SavedContent, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Product, y => y.User, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
