using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        readonly IUserRoleDal _userRoleDal;
        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public async Task<bool> CreateAsync(UserRole entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _userRoleDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserRole entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _userRoleDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userRoleDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            try
            {
                var result = await _userRoleDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserRole>();
            }
        }

        public async Task<IEnumerable<UserRole>> GetAllForAdmin()
        {
            try
            {
                var result = await _userRoleDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserRole>();
            }
        }

        public async Task<UserRole> GetByIdAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _userRoleDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            var result = await _userRoleDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            var result = await _userRoleDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            var result = await _userRoleDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            var result = await _userRoleDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(UserRole entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _userRoleDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
