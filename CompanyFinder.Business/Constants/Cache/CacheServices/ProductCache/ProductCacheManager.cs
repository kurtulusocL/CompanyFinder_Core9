using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.ProductCache
{
    public class ProductCacheManager : IProductCacheService
    {
        readonly IMemoryCache _memoryCache;
        readonly IProductDal _productDal;
        public ProductCacheManager(IMemoryCache memoryCache, IProductDal productDal)
        {
            _memoryCache = memoryCache;
            _productDal = productDal;
        }

        public async Task UpdateProductCache(int? companyId, int? categoryId, int? subcategoryId, string userId, int? id)
        {
            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllProducts, out List<Product> allProducts))
            {
                var updatedProducts = await _productDal.GetAllAsync();
                _memoryCache.Set(ProductCacheKeys.GetAllProducts, updatedProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllQuestionableProducts, out List<Product> allQuestionableProducts))
            {
                var updatedQuestionableProducts = await _productDal.GetAllAsync(i=>i.IsQuestionable);
                _memoryCache.Set(ProductCacheKeys.GetAllQuestionableProducts, updatedQuestionableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllNotQuestionableProducts, out List<Product> allNotQuestionableProducts))
            {
                var updatedNotQuestionableProducts = await _productDal.GetAllAsync(i => !i.IsQuestionable);
                _memoryCache.Set(ProductCacheKeys.GetAllNotQuestionableProducts, updatedNotQuestionableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllAvailableProducts, out List<Product> allAvailableProducts))
            {
                var updatedAvailableProducts = await _productDal.GetAllAsync(i => i.IsAvailable);
                _memoryCache.Set(ProductCacheKeys.GetAllAvailableProducts, updatedAvailableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllNotAvailableProducts, out List<Product> allNotAvailableProducts))
            {
                var updatedNotAvailableProducts = await _productDal.GetAllAsync(i => !i.IsAvailable);
                _memoryCache.Set(ProductCacheKeys.GetAllNotAvailableProducts, updatedNotAvailableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllCommentableProducts, out List<Product> allCommentableProducts))
            {
                var updatedCommentableProducts = await _productDal.GetAllAsync(i => i.IsCommentable);
                _memoryCache.Set(ProductCacheKeys.GetAllCommentableProducts, updatedCommentableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllNotCommentableProducts, out List<Product> allNotCommentableProducts))
            {
                var updatedNotCommentableProducts = await _productDal.GetAllAsync(i => !i.IsCommentable);
                _memoryCache.Set(ProductCacheKeys.GetAllNotCommentableProducts, updatedNotCommentableProducts, TimeSpan.FromMinutes(25));
            }

            if (_memoryCache.TryGetValue(ProductCacheKeys.GetAllProductsForAdmin, out List<Product> allProductsForAdmin))
            {
                var updatedProductsForAdmin = await _productDal.GetAllAsync();
                _memoryCache.Set(ProductCacheKeys.GetAllProductsForAdmin, updatedProductsForAdmin, TimeSpan.FromMinutes(25));
            }

            if (companyId.HasValue)
            {
                string productCompanyCacheKey = ProductCacheKeys.GetAllIncludingByCompanyIdAsync(companyId.Value);
                if (_memoryCache.TryGetValue(productCompanyCacheKey, out List<Product> productCompanies))
                {
                    var updatedProductCompanies = await _productDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(productCompanyCacheKey, updatedProductCompanies, TimeSpan.FromMinutes(25));
                }

                string productCompanyForAdminCacheKey = ProductCacheKeys.GetAllIncludingByCompanyIdForAdminAsync(companyId.Value);
                if (_memoryCache.TryGetValue(productCompanyForAdminCacheKey, out List<Product> productCompaniesForAdmin))
                {
                    var updatedProductCompaniesForAdmin = await _productDal.GetAllAsync(i => i.CompanyId == companyId.Value);
                    _memoryCache.Set(productCompanyForAdminCacheKey, updatedProductCompaniesForAdmin, TimeSpan.FromMinutes(25));
                }
            }

            if (categoryId.HasValue)
            {
                string productCategoryCacheKey = ProductCacheKeys.GetAllIncludingByCategoryIdAsync(categoryId.Value);
                if (_memoryCache.TryGetValue(productCategoryCacheKey, out List<Product> productCategories))
                {
                    var updatedProductCategories = await _productDal.GetAllAsync(i => i.ProductCategoryId == categoryId.Value);
                    _memoryCache.Set(productCategoryCacheKey, updatedProductCategories, TimeSpan.FromMinutes(25));
                }
            }

            if (subcategoryId.HasValue)
            {
                string productSubcategoryCacheKey = ProductCacheKeys.GetAllIncludingBySubcategoryIdAsync(subcategoryId.Value);
                if (_memoryCache.TryGetValue(productSubcategoryCacheKey, out List<Product> productSubcategories))
                {
                    var updatedProductSubcategories = await _productDal.GetAllAsync(i => i.ProductSubcategoryId == subcategoryId.Value);
                    _memoryCache.Set(productSubcategoryCacheKey, updatedProductSubcategories, TimeSpan.FromMinutes(25));
                }
            }

            if (!string.IsNullOrEmpty(userId))
            {
                string userCacheKey = ProductCacheKeys.GetAllIncludingByUserIdAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKey, out List<Product> userProducts))
                {
                    var updatedUserProducts = await _productDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKey, updatedUserProducts, TimeSpan.FromMinutes(25));
                }

                string userCacheKeyForAdmin = ProductCacheKeys.GetAllIncludingByUserIdForAdminAsync(userId);
                if (_memoryCache.TryGetValue(userCacheKeyForAdmin, out List<Product> userProductsForAdmin))
                {
                    var updatedUserProductsForAdmin = await _productDal.GetAllAsync(a => a.UserId == userId);
                    _memoryCache.Set(userCacheKeyForAdmin, updatedUserProductsForAdmin, TimeSpan.FromMinutes(25));
                }
            }

            if (id.HasValue)
            {
                string productCacheKey = ProductCacheKeys.GetByIdAsync(id.Value);
                if (_memoryCache.TryGetValue(productCacheKey, out Product singleProduct))
                {
                    var updatedProduct = await _productDal.GetAsync(a => a.Id == id.Value);
                    if (updatedProduct != null)
                    {
                        _memoryCache.Set(productCacheKey, updatedProduct, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}