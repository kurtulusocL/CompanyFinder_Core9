using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CommentAnswerManager : ICommentAnswerService
    {
        readonly ICommentAnswerDal _commentAnswerDal;
        readonly IHttpContextAccessor _httpContextAcccessor;
        public CommentAnswerManager(ICommentAnswerDal commentAnswerDal, IHttpContextAccessor httpContextAccessor)
        {
            _commentAnswerDal = commentAnswerDal;
            _httpContextAcccessor = httpContextAccessor;
        }

        public int CommentAnswerCounter()
        {
            var result = _commentAnswerDal.CommentAnswerCounter();
            return result;
        }

        public async Task<bool> CreateAsync(string? title, string text, int? commentId, string userId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                userId ??= _httpContextAcccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new CommentAnswer
                {
                    Title = title,
                    Text = text,
                    CommentId = commentId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _commentAnswerDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CommentAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _commentAnswerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _commentAnswerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _commentAnswerDal.GetAllIncludeAsync(new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Comment, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "Comment Id was null");

                var result = await _commentAnswerDal.GetAllIncludeByIdAsync(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Blog, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _commentAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Blog, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public IEnumerable<CommentAnswer> GetAllIncludingCommentAnswerForCompanyByCommentId(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var result = _commentAnswerDal.GetAllIncludeById(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.Comment.Blog.Company, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public IEnumerable<CommentAnswer> GetAllIncludingCommentAnswersForExplorerByCommentId(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var result = _commentAnswerDal.GetAllIncludeById(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CommentId==commentId
                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Product, y => y.Comment.Blog, y => y.Reports, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _commentAnswerDal.GetAllIncludeAsync(new Expression<Func<CommentAnswer, bool>>[]
                {

                }, null, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Blog, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdminByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "Comment Id was null");

                var result = await _commentAnswerDal.GetAllIncludeByIdAsync(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {

                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Blog, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _commentAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Blog, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public IEnumerable<CommentAnswer> GetAllSiteMap()
        {
            try
            {
                var result = _commentAnswerDal.GetAllInclude(new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Comment);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public async Task<CommentAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _commentAnswerDal.GetIncludeAsync(i => i.Id == id, y => y.Comment, y => y.User, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _commentAnswerDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _commentAnswerDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _commentAnswerDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _commentAnswerDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
