using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CustomerStatusManager : ICustomerStatusService
    {
        readonly ICustomerStatusDal _customerStatusDal;
        public CustomerStatusManager(ICustomerStatusDal customerStatusDal)
        {
            _customerStatusDal = customerStatusDal;
        }

        public async Task<bool> CreateAsync(CustomerStatus entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _customerStatusDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CustomerStatus entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _customerStatusDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _customerStatusDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CustomerStatus>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _customerStatusDal.GetAllIncludeAsync(new Expression<Func<CustomerStatus, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Customers);
                return result.OrderByDescending(i => i.Customers.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CustomerStatus>();
            }
        }

        public async Task<IEnumerable<CustomerStatus>> GetAllIncludingForAddCustomerAsync()
        {
            try
            {
                var result = await _customerStatusDal.GetAllIncludeAsync(new Expression<Func<CustomerStatus, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Customers);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CustomerStatus>();
            }
        }

        public async Task<IEnumerable<CustomerStatus>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _customerStatusDal.GetAllIncludeAsync(new Expression<Func<CustomerStatus, bool>>[]
                {
                   
                }, null, y => y.Customers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CustomerStatus>();
            }
        }

        public async Task<CustomerStatus> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _customerStatusDal.GetIncludeAsync(i => i.Id == id, y => y.Customers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _customerStatusDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _customerStatusDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _customerStatusDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _customerStatusDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CustomerStatus entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _customerStatusDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
