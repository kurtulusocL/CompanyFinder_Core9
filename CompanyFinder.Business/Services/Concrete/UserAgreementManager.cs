using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class UserAgreementManager : IUserAgreementService
    {
        readonly IUserAgreementDal _userAgreementDal;
        public UserAgreementManager(IUserAgreementDal userAgreementDal)
        {
            _userAgreementDal = userAgreementDal;
        }

        public async Task<bool> CreateAsync(UserAgreement entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _userAgreementDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserAgreement entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _userAgreementDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userAgreementDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserAgreement>> GetAllAsync()
        {
            try
            {
                var result = await _userAgreementDal.GetAllAsync(i => i.IsDeleted == false && i.IsActive == true);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserAgreement>();
            }
        }

        public async Task<IEnumerable<UserAgreement>> GetAllForAdmin()
        {
            try
            {
                var result = await _userAgreementDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserAgreement>();
            }
        }

        public async Task<UserAgreement> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _userAgreementDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userAgreementDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userAgreementDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userAgreementDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userAgreementDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(UserAgreement entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _userAgreementDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
