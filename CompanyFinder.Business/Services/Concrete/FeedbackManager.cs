using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class FeedbackManager : IFeedbackService
    {
        readonly IFeedbackDal _feedbackDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public FeedbackManager(IFeedbackDal feedbackDal, IHttpContextAccessor httpContextAccessor)
        {
            _feedbackDal = feedbackDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string title, string subject, string desc, string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                Feedback model = new Feedback
                {
                    Title = title,
                    Subject = subject,
                    Desc = desc,
                    UserId = userId
                };
                if (model != null)
                {
                    var result = await _feedbackDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Feedback entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _feedbackDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _feedbackDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            try
            {
                var result = await _feedbackDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Feedback>();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllByUserIdAsync(string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");

                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _feedbackDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Feedback, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.UserId==userId
                });
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Feedback>();
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllForAdminAsync()
        {
            try
            {
                var result = await _feedbackDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Feedback>();
            }
        }

        public async Task<Feedback> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _feedbackDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _feedbackDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _feedbackDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _feedbackDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _feedbackDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
