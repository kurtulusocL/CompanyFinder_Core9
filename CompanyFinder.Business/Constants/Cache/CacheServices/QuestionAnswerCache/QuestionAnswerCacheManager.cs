using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.QuestionAnswerCache
{
    public class QuestionAnswerCacheManager : IQuestionAnswerCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IQuestionAnswerDal _questionAnswerDal;
        public QuestionAnswerCacheManager(IMemoryCache memoryCache, IQuestionAnswerDal questionAnswerDal)
        {
            _memoryCache = memoryCache;
            _questionAnswerDal = questionAnswerDal;
        }

        public async Task ClearQuestionAnswerCache(int? questionId, string userId, int? id)
        {
            if (_memoryCache.TryGetValue(QuestionAnswerCacheKeys.GetAllQuestionAnswers, out List<QuestionAnswer> allQuestionAnswers))
            {
                var updatedQuestionAnswers = await _questionAnswerDal.GetAllAsync();
                _memoryCache.Set(QuestionAnswerCacheKeys.GetAllQuestionAnswers, updatedQuestionAnswers, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(QuestionAnswerCacheKeys.GetAllQuestionAnswerForAdmin, out List<QuestionAnswer> allQuestionAnswersForAdmin))
            {
                var updatedQuestionAnswersForAdmin = await _questionAnswerDal.GetAllAsync();
                _memoryCache.Set(QuestionAnswerCacheKeys.GetAllQuestionAnswerForAdmin, updatedQuestionAnswersForAdmin, TimeSpan.FromMinutes(25));
            }

            if (questionId.HasValue)
            {
                string questionAnswerCacheKey = QuestionAnswerCacheKeys.GetAllIncludingByQuestionIdAsync(questionId.Value);
                if (_memoryCache.TryGetValue(questionAnswerCacheKey, out List<QuestionAnswer> questionAnswers))
                {
                    var updatedQuestionAnswers = await _questionAnswerDal.GetAllAsync(i => i.QuestionId == questionId.Value);
                    _memoryCache.Set(questionAnswerCacheKey, updatedQuestionAnswers, TimeSpan.FromMinutes(25));
                }

                string questionAnswerForAdminCacheKey = QuestionAnswerCacheKeys.GetAllIncludingForAdminByQuestionIdAsync(questionId.Value);
                if (_memoryCache.TryGetValue(questionAnswerForAdminCacheKey, out List<QuestionAnswer> questionAnswersForAdmin))
                {
                    var updatedQuestionAnswersForAdmin = await _questionAnswerDal.GetAllAsync(i => i.QuestionId == questionId.Value);
                    _memoryCache.Set(questionAnswerForAdminCacheKey, updatedQuestionAnswersForAdmin, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = QuestionAnswerCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Blog> userQuestionAnswers))
                {
                    var updatedUserQuestionAnswers = await _questionAnswerDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserQuestionAnswers, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = QuestionAnswerCacheKeys.GetAllIncludingForAdminByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Blog> userAdminQuestionAnswers))
                {
                    var updatedUserAdminQuestinAnswers = await _questionAnswerDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminQuestinAnswers, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string questionAnswerCacheKey = QuestionAnswerCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(questionAnswerCacheKey, out QuestionAnswer singleQuestionAnswer))
                {
                    var updatedQuestionAnswer = await _questionAnswerDal.GetAsync(a => a.Id == id.Value);
                    if (updatedQuestionAnswer != null)
                    {
                        _memoryCache.Set(questionAnswerCacheKey, updatedQuestionAnswer, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
