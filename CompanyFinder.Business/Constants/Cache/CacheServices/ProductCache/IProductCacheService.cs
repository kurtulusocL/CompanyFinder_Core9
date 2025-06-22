using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.ProductCache
{
    public interface IProductCacheService
    {
        Task UpdateProductCache(int? companyId, int? categoryId, int? subcategoryId, string userId, int? id);
    }
}
