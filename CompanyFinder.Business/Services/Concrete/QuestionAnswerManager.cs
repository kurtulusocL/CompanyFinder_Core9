using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        readonly IQuestionAnswerDal _questionAnswerDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public QuestionAnswerManager(IQuestionAnswerDal questionAnswerDal, IHttpContextAccessor httpContextAccessor)
        {
            _questionAnswerDal = questionAnswerDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string? title, string answer, int? questionId, string userId)
        {
            try
            {
                if (questionId == null)
                    throw new ArgumentNullException(nameof(questionId), "questionId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new QuestionAnswer
                {
                    Title = title,
                    Answer = answer,
                    QuestionId = questionId,
                    UserId = userId
                };

                if (entity != null)
                {
                    var result = await _questionAnswerDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(QuestionAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _questionAnswerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _questionAnswerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _questionAnswerDal.GetAllIncludeAsync(new Expression<Func<QuestionAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingByQuestionIdAsync(int? questionId)
        {
            try
            {
                if (questionId == null)
                    throw new ArgumentNullException(nameof(questionId), "questionId was null");

                var result = await _questionAnswerDal.GetAllIncludeByIdAsync(questionId, "QuestionId", new Expression<Func<QuestionAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _questionAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<QuestionAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _questionAnswerDal.GetAllIncludeAsync(new Expression<Func<QuestionAnswer, bool>>[]
                {

                }, null, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdminByQuestionIdAsync(int? questionId)
        {
            try
            {
                if (questionId == null)
                    throw new ArgumentNullException(nameof(questionId), "questionId was null");

                var result = await _questionAnswerDal.GetAllIncludeByIdAsync(questionId, "QuestionId", new Expression<Func<QuestionAnswer, bool>>[]
                {
                    
                }, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<IEnumerable<QuestionAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _questionAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<QuestionAnswer, bool>>[]
                {
                    
                }, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public IEnumerable<QuestionAnswer> GetAllIncludingQuestionAnswerByQuestionId(int? questionId)
        {
            try
            {
                if (questionId == null)
                    throw new ArgumentNullException(nameof(questionId), "questionId was null");

                var result = _questionAnswerDal.GetAllIncludeById(questionId, "QuestionId", new Expression<Func<QuestionAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.QuestionId.HasValue, y=>y.Question.CompanyId.HasValue, y=>y.Question.Company.Name, y => y.UserId);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionAnswer>();
            }
        }

        public async Task<QuestionAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _questionAnswerDal.GetIncludeAsync(i => i.Id == id, y => y.Question, y => y.Question.Company, y => y.Question.Product, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public QuestionAnswer GetQuestionAnswerById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return _questionAnswerDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public int QuestionAnswerCounter()
        {
            var result = _questionAnswerDal.QuestionAnswerCounter();
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _questionAnswerDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _questionAnswerDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _questionAnswerDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _questionAnswerDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
