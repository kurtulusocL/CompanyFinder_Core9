using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CancelMembershipReasonManager : ICancelMembershipReasonService
    {
        readonly ICancelMembershipReasonDal _cancelMembershipReasonDal;
        public CancelMembershipReasonManager(ICancelMembershipReasonDal cancelMembershipReasonDal)
        {
            _cancelMembershipReasonDal = cancelMembershipReasonDal;
        }

        public async Task<bool> CreateAsync(CancelMembershipReason entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _cancelMembershipReasonDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CancelMembershipReason entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _cancelMembershipReasonDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _cancelMembershipReasonDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CancelMembershipReason>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _cancelMembershipReasonDal.GetAllIncludeAsync(new Expression<Func<CancelMembershipReason, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, i => i.CancelMemberships);
                return result.OrderByDescending(i => i.CancelMemberships.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembershipReason>();
            }
        }

        public async Task<IEnumerable<CancelMembershipReason>> GetAllIncludingForAddAsync()
        {
            try
            {
                var result = await _cancelMembershipReasonDal.GetAllIncludeAsync(new Expression<Func<CancelMembershipReason, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, i => i.CancelMemberships);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembershipReason>();
            }
        }

        public async Task<IEnumerable<CancelMembershipReason>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _cancelMembershipReasonDal.GetAllIncludeAsync(new Expression<Func<CancelMembershipReason, bool>>[]
                {

                }, null, i => i.CancelMemberships);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembershipReason>();
            }
        }

        public async Task<CancelMembershipReason> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _cancelMembershipReasonDal.GetIncludeAsync(i => i.Id == id, i => i.CancelMemberships);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _cancelMembershipReasonDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _cancelMembershipReasonDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _cancelMembershipReasonDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _cancelMembershipReasonDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CancelMembershipReason entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _cancelMembershipReasonDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
