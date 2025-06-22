using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ProductSubcategoryManager : IProductSubcategoryService
    {
        readonly IProductSubcategoryDal _productSubcategoryDal;
        public ProductSubcategoryManager(IProductSubcategoryDal productSubcategoryDal)
        {
            _productSubcategoryDal = productSubcategoryDal;
        }

        public async Task<bool> CreateAsync(ProductSubcategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _productSubcategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ProductSubcategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _productSubcategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _productSubcategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _productSubcategoryDal.GetAllIncludeAsync(new Expression<Func<ProductSubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.Products, y => y.ProductCategory);
                return result.OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllIncludingByCategoryIdAsync(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                    throw new ArgumentNullException(nameof(categoryId), "CategoryId was null");

                var result = await _productSubcategoryDal.GetAllIncludeByIdAsync(categoryId, "ProductCategoryId",
                     new Expression<Func<ProductSubcategory, bool>>[]
                     {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                     },
                     i => i.ProductCategory,
                     i => i.Products
                 );
                return result.OrderByDescending(i=>i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllIncludingForAddProductByCategoryIdAsync(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                    throw new ArgumentNullException(nameof(categoryId), "CategoryId was null");

                var result = await _productSubcategoryDal.GetAllIncludeByIdAsync(categoryId, "ProductCategoryId",
                    new Expression<Func<ProductSubcategory, bool>>[]
                    {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                    },
                    i => i.ProductCategory,
                    i => i.Products
                );
                return result.OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public async Task<IEnumerable<ProductSubcategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _productSubcategoryDal.GetAllIncludeAsync(new Expression<Func<ProductSubcategory, bool>>[]
                {

                }, null, y => y.Products, y => y.ProductCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public IEnumerable<ProductSubcategory> GetAllIncludingProductSubcategoriesForProductAdvancedSearch()
        {
            try
            {
                try
                {
                    return _productSubcategoryDal.GetAllInclude(new Expression<Func<ProductSubcategory, bool>>[]
                    {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                    }, null).OrderBy(i => i.Name).ToList();
                }
                catch (Exception)
                {
                    return new List<ProductSubcategory>();
                }
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public IEnumerable<ProductSubcategory> GetAllIncludingProductSubcategoriesForProductExplorer()
        {
            try
            {
                return _productSubcategoryDal.GetAllInclude(new Expression<Func<ProductSubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                }, null, y => y.Products).OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public IEnumerable<ProductSubcategory> GetAllProductSubcategoriesForAdminHome()
        {
            try
            {
                return _productSubcategoryDal.GetAllInclude(new Expression<Func<ProductSubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Products.Count()>0
                }, null, y => y.Products, y => y.ProductCategory).OrderByDescending(i => i.Products.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public IEnumerable<ProductSubcategory> GetAllSiteMap()
        {
            try
            {
                var result = _productSubcategoryDal.GetAllInclude(new Expression<Func<ProductSubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.Products, y => y.ProductCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProductSubcategory>();
            }
        }

        public async Task<ProductSubcategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _productSubcategoryDal.GetIncludeAsync(i => i.Id == id, y => y.Products, y => y.ProductCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _productSubcategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _productSubcategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _productSubcategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _productSubcategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(ProductSubcategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _productSubcategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
