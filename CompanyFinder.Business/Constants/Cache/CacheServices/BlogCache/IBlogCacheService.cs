using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.BlogCache
{
    public interface IBlogCacheService
    {
        Task BlogUpdateCache(int? categoryId, int? companyId, string userId, int? id);
    }
}
