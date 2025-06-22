using System.ComponentModel.Design;
using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.QuestionCache
{
    public class QuestionCacheManager : IQuestionCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IQuestionDal _questionDal;
        public QuestionCacheManager(IMemoryCache memoryCache, IQuestionDal questionDal)
        {
            _memoryCache = memoryCache;
            _questionDal = questionDal;
        }

        public async Task ClearUpdateCache()
        {
            if (_memoryCache.TryGetValue(QuestionCacheKeys.GetAllQuestions, out List<Question> allQuestions))
            {
                var updatedQuestions = await _questionDal.GetAllAsync();
                _memoryCache.Set(QuestionCacheKeys.GetAllQuestions, updatedQuestions, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(QuestionCacheKeys.GetAllQuestionForAdmin, out List<Question> allQuestionsForAdmin))
            {
                var updatedQuestionsForAdmin = await _questionDal.GetAllAsync();
                _memoryCache.Set(QuestionCacheKeys.GetAllQuestionForAdmin, updatedQuestionsForAdmin, TimeSpan.FromMinutes(25));
            }
        }

        public async Task CompanyCacheClear(int? companyId, string userId, int? id)
        {
            if (companyId.HasValue)
            {
                string questionCompanyCacheKey = QuestionCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(questionCompanyCacheKey, out List<Question> companyQuestions))
                {
                    var updatedCompanyQuestions = await _questionDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(questionCompanyCacheKey, updatedCompanyQuestions, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = QuestionCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Question> userQuestions))
                {
                    var updatedUserQuestions = await _questionDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserQuestions, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = QuestionCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Question> userAdminQuestions))
                {
                    var updatedUserAdminQuestions = await _questionDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminQuestions, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string questionCacheKey = QuestionCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(questionCacheKey, out Question singleQuestion))
                {
                    var updatedQuestion = await _questionDal.GetAsync(a => a.Id == id.Value);
                    if (updatedQuestion != null)
                    {
                        _memoryCache.Set(questionCacheKey, updatedQuestion, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }

        public async Task ProductCacheClear(int? productId, string userId, int? id)
        {
            if (productId.HasValue)
            {
                string questionProductCacheKey = QuestionCacheKeys.GetAllIncludingByCompanyIdAsync(productId.Value);
                if (_memoryCache.TryGetValue(questionProductCacheKey, out List<Question> procuctQuestions))
                {
                    var updatedProductQuestions = await _questionDal.GetAllAsync(i => i.ProductId == productId.Value);
                    _memoryCache.Set(questionProductCacheKey, updatedProductQuestions, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = QuestionCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Question> userQuestions))
                {
                    var updatedUserQuestions = await _questionDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserQuestions, TimeSpan.FromMinutes(25));
                }

                string userAdminCacheKey = QuestionCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userAdminCacheKey, out List<Question> userAdminQuestions))
                {
                    var updatedUserAdminQuestions = await _questionDal.GetAllAsync(i => i.UserId == userId);
                    _memoryCache.Set(userAdminCacheKey, updatedUserAdminQuestions, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string questionCacheKey = QuestionCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(questionCacheKey, out Question singleQuestion))
                {
                    var updatedQuestion = await _questionDal.GetAsync(a => a.Id == id.Value);
                    if (updatedQuestion != null)
                    {
                        _memoryCache.Set(questionCacheKey, updatedQuestion, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
