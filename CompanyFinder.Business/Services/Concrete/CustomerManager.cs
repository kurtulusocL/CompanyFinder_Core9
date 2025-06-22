using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CustomerManager : ICustomerService
    {
        readonly ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public async Task<bool> CreateAsync(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId)
        {
            try
            {
                if(companyId==null)
                    throw new ArgumentNullException(nameof(companyId),"companyId was null");

                Customer model = new Customer
                {
                    NameSurname = nameSurname,
                    CustomerCompany = customerCompany,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Location = location,
                    Desc = desc,
                    CompanyId = companyId,
                    CustomerStatusId = customerStatusId
                };
                if (model != null)
                {
                    var result = await _customerDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Customer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _customerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _customerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _customerDal.GetAllIncludeAsync(new Expression<Func<Customer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _customerDal.GetAllIncludeByIdAsync(companyId,"CompanyId", new Expression<Func<Customer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingByStatusIdAsync(int customerStatusId)
        {
            try
            {
                var result = await _customerDal.GetAllIncludeByIdAsync(customerStatusId, "CustomerStatusId", new Expression<Func<Customer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _customerDal.GetAllIncludeAsync(new Expression<Func<Customer, bool>>[]
                {
                    
                }, null, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingForCompanyUserByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _customerDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Customer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllIncludingForCompanyUserByStatusIdAsync(int customerStatusId)
        {
            try
            {
                var result = await _customerDal.GetAllIncludeByIdAsync(customerStatusId, "CustomerStatusId", new Expression<Func<Customer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.CustomerStatus);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public async Task<Customer> GetByIdAsync(int? id)
        {
            try
            {
                if(id==null)
                    throw new ArgumentNullException(nameof(id),"id was null");

                return await _customerDal.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.CustomerStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _customerDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _customerDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _customerDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _customerDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                Customer model = new Customer
                {
                    NameSurname = nameSurname,
                    CustomerCompany = customerCompany,
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    Location = location,
                    Desc = desc,
                    CompanyId = companyId,
                    CustomerStatusId = customerStatusId,
                    Id = id
                };
                if (model != null)
                {
                    model.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _customerDal.UpdateAsync(model);
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
