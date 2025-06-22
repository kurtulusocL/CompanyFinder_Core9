using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AuditManager : IAuditService
    {
        readonly IAuditDal _auditDal;
        public AuditManager(IAuditDal auditDal)
        {
            _auditDal = auditDal;
        }

        public async Task<bool> DeleteAsync(Audit entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _auditDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _auditDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Audit>> GetAllAsync()
        {
            try
            {
                var result = await _auditDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Audit>();
            }
        }

        public async Task<IEnumerable<Audit>> GetAllForAdmin()
        {
            try
            {
                var result = await _auditDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Audit>();
            }
        }

        public async Task<IEnumerable<Audit>> GetAllUserAuditAsync()
        {
            try
            {
                var result = await _auditDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false && i.UserName != "Anonymous");
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Audit>();
            }
        }

        public async Task<IEnumerable<Audit>> GetAllVisitorAuditAsync()
        {
            try
            {
                var result = await _auditDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false && i.UserName == "Anonymous");
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Audit>();
            }
        }

        public async Task<Audit> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _auditDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _auditDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _auditDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _auditDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _auditDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
