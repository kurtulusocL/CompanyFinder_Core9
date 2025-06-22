using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class BlackListManager : IBlackListService
    {
        readonly IBlackListDal _blackListDal;
        public BlackListManager(IBlackListDal blackListDal)
        {
            _blackListDal = blackListDal;
        }

        public async Task<bool> CreateAsync(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var model = new BlackList
                {
                    MacAddress = macAddress,
                    IpAddress = ipAddress,
                    LocalIpAddress = localIpAddress,
                    RemoteIpAddress = remoteIpAddress,
                    IpAddressVPN = ipAddressVPN,
                    ExpirationDate = expirationDate,
                    AuditId = auditId
                };
                if (model != null)
                {
                    var result = await _blackListDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(BlackList entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _blackListDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blackListDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<BlackList>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _blackListDal.GetAllIncludeAsync(new Expression<Func<BlackList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Audit);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlackList>();
            }
        }

        public async Task<IEnumerable<BlackList>> GetAllIncludingByAuditIdAsync(int? auditId)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var result = await _blackListDal.GetAllIncludeByIdAsync(auditId, "AuditId", new Expression<Func<BlackList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.AuditId==auditId
                }, y => y.Audit);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlackList>();
            }
        }

        public async Task<IEnumerable<BlackList>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _blackListDal.GetAllIncludeAsync(new Expression<Func<BlackList, bool>>[]
                {

                }, null, y => y.Audit);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlackList>();
            }
        }

        public async Task<IEnumerable<BlackList>> GetAllIncludingForAdminByAuditIdAsync(int? auditId)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var result = await _blackListDal.GetAllIncludeByIdAsync(auditId, "AuditId", new Expression<Func<BlackList, bool>>[]
                {

                    i=>i.AuditId==auditId
                }, y => y.Audit);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlackList>();
            }
        }

        public async Task<BlackList> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blackListDal.GetIncludeAsync(i => i.Id == id, y => y.Audit);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blackListDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blackListDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blackListDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blackListDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string macAddress, string ipAddress, string localIpAddress, string remoteIpAddress, string ipAddressVPN, DateTime expirationDate, int? auditId, int id)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var model = new BlackList
                {
                    MacAddress = macAddress,
                    IpAddress = ipAddress,
                    LocalIpAddress = localIpAddress,
                    RemoteIpAddress = remoteIpAddress,
                    IpAddressVPN = ipAddressVPN,
                    ExpirationDate = expirationDate,
                    AuditId = auditId,
                    Id = id
                };
                if (model != null)
                {
                    model.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _blackListDal.UpdateAsync(model);
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
