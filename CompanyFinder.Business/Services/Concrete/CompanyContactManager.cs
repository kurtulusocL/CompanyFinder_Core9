using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyContactManager : ICompanyContactService
    {
        readonly ICompanyContactDal _companyContactDal;
        public CompanyContactManager(ICompanyContactDal companyContactDal)
        {
            _companyContactDal = companyContactDal;
        }
        public async Task<bool> CreateAsync(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyContact
                {
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    WhatsappNo = whatsappNo,
                    Address = address,
                    IsHide = isHide,
                    CompanyId = companyId
                };
                if (entity != null)
                {
                    var result = await _companyContactDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyContact entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyContactDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyContactDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyContactDal.GetAllIncludeAsync(new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyContact>();
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyContactDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyContact>();
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllIncludingContactHideAsync()
        {
            try
            {
                var result = await _companyContactDal.GetAllIncludeAsync(new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsHide==true
                }, null, y => y.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyContact>();
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _companyContactDal.GetAllIncludeAsync(new Expression<Func<CompanyContact, bool>>[]
                {

                }, null, y => y.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyContact>();
            }
        }

        public async Task<IEnumerable<CompanyContact>> GetAllIncludingForAdminByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyContactDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Company.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyContact>();
            }
        }

        public async Task<CompanyContact> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companyContactDal.GetIncludeAsync(i => i.Id == id, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CompanyContact GetCompanyContactByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyContactDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CompanyContact GetCompanyContactForcCompanyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyContactDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.CompanyId == companyId, y => y.Company, y => y.Company.Country, y => y.Company.City);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyContactDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyContactDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyContactDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetHideAsync(int id)
        {
            var result = await _companyContactDal.SetHideAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyContactDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotHideAsync(int id)
        {
            var result = await _companyContactDal.SetNotHideAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyContact
                {
                    EmailAddress = emailAddress,
                    PhoneNumber = phoneNumber,
                    WhatsappNo = whatsappNo,
                    Address = address,
                    IsHide = isHide,
                    CompanyId = companyId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _companyContactDal.UpdateAsync(entity);
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
