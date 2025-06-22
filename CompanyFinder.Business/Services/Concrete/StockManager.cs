using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class StockManager : IStockService
    {
        readonly IStockDal _stockDal;
        public StockManager(IStockDal stockDal)
        {
            _stockDal = stockDal;
        }

        public async Task<bool> CreateAsync(int quantity, string? warehouse, int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);
                var model = new Stock
                {
                    Code = code,
                    Quantity = quantity,
                    Warehouse = warehouse,
                    ProductId = productId
                };
                if (model != null)
                {
                    var result = await _stockDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Stock entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _stockDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _stockDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Stock>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _stockDal.GetAllIncludeAsync(new Expression<Func<Stock, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Product, y => y.Product.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Stock>();
            }
        }

        public async Task<IEnumerable<Stock>> GetAllIncludingByCompanyIdIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _stockDal.GetAllIncludingByPropertyPathAsync(companyId, "Product.CompanyId", new Expression<Func<Stock, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Product.CompanyId==companyId
                }, y => y.Product, y => y.Product.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Stock>();
            }
        }

        public async Task<IEnumerable<Stock>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = await _stockDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Stock, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Product, y => y.Product.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Stock>();
            }
        }

        public async Task<IEnumerable<Stock>> GetAllIncludingByQuantityAsync()
        {
            try
            {
                var result = await _stockDal.GetAllIncludeAsync(new Expression<Func<Stock, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Product, y => y.Product.Company);
                return result.OrderByDescending(i => i.Quantity).ToList();
            }
            catch (Exception)
            {
                return new List<Stock>();
            }
        }

        public async Task<IEnumerable<Stock>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _stockDal.GetAllIncludeAsync(new Expression<Func<Stock, bool>>[]
                {

                }, null, y => y.Product, y => y.Product.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Stock>();
            }
        }

        public async Task<Stock> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _stockDal.GetIncludeAsync(i => i.Id == id, y => y.Product, y => y.Product.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Stock GetProductForCompanyUserSrockByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                return _stockDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.ProductId == productId, y => y.Product);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<Stock> GetProductForCompanyUserStockByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "id was null");

                return await _stockDal.GetIncludeAsync(i => i.IsActive == true && i.IsDeleted == false && i.ProductId == productId, y => y.Product);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _stockDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _stockDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _stockDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _stockDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(int quantity, string? warehouse, int? productId, int id)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var model = new Stock
                {
                    Quantity = quantity,
                    Warehouse = warehouse,
                    ProductId = productId,
                    Id = id,
                    UpdatedDate = DateTime.Now.ToLocalTime()
                };
                if (model != null)
                {
                    var result = await _stockDal.UpdateAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
