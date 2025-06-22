using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ProductCategoryManager : IProductCategoryService
    {
        readonly IProductCategoryDal _productCategoryDal;
        public ProductCategoryManager(IProductCategoryDal productCategoryDal)
        {
            _productCategoryDal = productCategoryDal;
        }

        public async Task<bool> CreateAsync(ProductCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _productCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ProductCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _productCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _productCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _productCategoryDal.GetAllIncludeAsync(new Expression<Func<ProductCategory, bool>>[] {
                i=>i.IsActive==true,
                x=>x.IsDeleted==false
                }, null, x => x.Products, x => x.ProductSubcategories);
                return result.OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllIncludingForAddCompanyPartnershipAsync()
        {
            try
            {
                var result = await _productCategoryDal.GetAllIncludeAsync(new Expression<Func<ProductCategory, bool>>[] {
                i=>i.IsActive==true,
                x=>x.IsDeleted==false
                }, null, x => x.Products, x => x.ProductSubcategories, x => x.CompanyPartnerships);
                return result.OrderByDescending(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllIncludingForAddProductAsync()
        {
            try
            {
                var result = await _productCategoryDal.GetAllIncludeAsync(new Expression<Func<ProductCategory, bool>>[] {
                i=>i.IsActive==true,
                x=>x.IsDeleted==false
                }, null, x => x.Products, x => x.ProductSubcategories);
                return result.OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllIncludingForAddSubcategoryAsync()
        {
            try
            {
                var result = await _productCategoryDal.GetAllIncludeAsync(new Expression<Func<ProductCategory, bool>>[] {
                i=>i.IsActive==true,
                x=>x.IsDeleted==false
                }, null, x => x.Products, x => x.ProductSubcategories);
                return result.OrderByDescending(i => i.ProductSubcategories.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _productCategoryDal.GetAllIncludeAsync(new Expression<Func<ProductCategory, bool>>[] {

                }, null, x => x.Products, x => x.ProductSubcategories);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public IEnumerable<ProductCategory> GetAllIncludingProductCategoriesForProductAdvancedSearch()
        {
            try
            {
                return _productCategoryDal.GetAllInclude(new Expression<Func<ProductCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                }, null).OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public IEnumerable<ProductCategory> GetAllIncludingProductCategoriesForProductExplorer()
        {
            try
            {
                return _productCategoryDal.GetAllInclude(new Expression<Func<ProductCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                }, null, y => y.Products).OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public IEnumerable<ProductCategory> GetAllIncludingProductCategoryForCompanyPartnership()
        {
            try
            {
                return _productCategoryDal.GetAllInclude(new Expression<Func<ProductCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyPartnerships.Count()>0
                }, null, y => y.CompanyPartnerships).OrderByDescending(i => i.CompanyPartnerships.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public IEnumerable<ProductCategory> GetAllProductCategoryForAdminHome()
        {
            try
            {
                return _productCategoryDal.GetAllInclude(new Expression<Func<ProductCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                }, null, y => y.Products, y => y.ProductSubcategories).OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public IEnumerable<ProductCategory> GetAllSiteMap()
        {
            try
            {
                var result = _productCategoryDal.GetAllInclude(new Expression<Func<ProductCategory, bool>>[] {
                i=>i.IsActive==true,
                x=>x.IsDeleted==false
                }, null);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategory>();
            }
        }

        public async Task<ProductCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _productCategoryDal.GetIncludeAsync(i => i.Id == id, x => x.Products, x => x.ProductSubcategories, x => x.CompanyPartnerships);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _productCategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _productCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _productCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _productCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(ProductCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _productCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
