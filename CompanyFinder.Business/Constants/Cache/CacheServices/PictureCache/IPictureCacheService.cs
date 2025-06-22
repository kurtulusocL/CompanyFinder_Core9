using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.PictureCache
{
    public interface IPictureCacheService
    {
        Task ClarAndUpdateCache();
        Task ClearBlogCache(int? blogId, int? id);
        Task ClearCompanyCache(int? companyId, int? id);
        Task ClearProductCache(int? productId, int? id);
    }
}
