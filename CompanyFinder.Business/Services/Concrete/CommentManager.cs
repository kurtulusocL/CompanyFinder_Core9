using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CommentManager(ICommentDal commentDal, IHttpContextAccessor httpContextAccessor)
        {
            _commentDal = commentDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public int CommentCounter()
        {
            var result = _commentDal.CommentCounter();
            return result;
        }

        public async Task<bool> CreateBlogCommentAsync(string? subject, string text, int? blogId, string userId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Comment
                {
                    Subject = subject,
                    Text = text,
                    BlogId = blogId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _commentDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding to Blog Comment the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyCommentAsync(string? subject, string text, int? companyId, string userId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Comment
                {
                    Subject = subject,
                    Text = text,
                    CompanyId = companyId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _commentDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding to Company Comment the entity.", ex);
            }
        }

        public async Task<bool> CreateProductCommentAsync(string? subject, string text, int? productId, string userId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Comment
                {
                    Subject = subject,
                    Text = text,
                    ProductId = productId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _commentDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding to Product Comment the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Comment entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _commentDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _commentDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _commentDal.GetAllIncludeAsync(new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingBlogCommentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludingByPropertyPathAsync(userId, "Blog.UserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted == false,
                    i=>i.Blog.UserId==userId
                }, y => y.Blog, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByBlogIdForAdminAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByCompanyIdForAdminAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByProductIdForAdminAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Comment, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Comment, bool>>[]
                {

                }, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentForCompanyBlogByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = _commentDal.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blog, y => y.CommentAnswers, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentForCompanyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _commentDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.CommentAnswers, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentForCompanyProductByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = _commentDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Product, y => y.CommentAnswers, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingCommentForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentsForExplorerBlogDetailByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = _commentDal.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.BlogId==blogId
                }, y => y.Blog, y => y.Blog.Company, y => y.Company, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentsForExplorerCompanyDetailByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _commentDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyId==companyId
                }, y => y.Company, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllIncludingCommentsForExplorerProductDetailByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = _commentDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ProductId==productId
                }, y => y.Product, y => y.Product.Company, y => y.Company, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingCompanyCommentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted == false,
                    i=>i.Company.UserId==userId
                }, y => y.Company, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _commentDal.GetAllIncludeAsync(new Expression<Func<Comment, bool>>[]
                {

                }, null, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<IEnumerable<Comment>> GetAllIncludingProductCommentsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _commentDal.GetAllIncludingByPropertyPathAsync(userId, "Product.UserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted == false,
                    i=>i.Product.UserId==userId
                }, y => y.Product, y => y.User, y => y.CommentAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IEnumerable<Comment> GetAllSiteMap()
        {
            try
            {
                var result = _commentDal.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blog, y => y.Product);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public async Task<Comment> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _commentDal.GetIncludeAsync(i => i.Id == id, y => y.Blog, y => y.Company, y => y.Product, y => y.Product.Company, y => y.User, y => y.CommentAnswers, y => y.Reports);

            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Comment GetCommentById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return _commentDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _commentDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _commentDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _commentDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _commentDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
