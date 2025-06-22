using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.QuestionAnswerCache
{
    public interface IQuestionAnswerCacheService
    {
        Task ClearQuestionAnswerCache(int? questionId, string userId, int? id);
    }
}
