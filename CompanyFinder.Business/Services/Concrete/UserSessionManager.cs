using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Concrete
{
    public class UserSessionManager : IUserSessionService
    {
        readonly IUserSessionDal _userSessionDal;
        public UserSessionManager(IUserSessionDal userSessionDal)
        {
            _userSessionDal = userSessionDal;
        }

        public async Task<bool> DeleteAsync(UserSession entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _userSessionDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userSessionDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _userSessionDal.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingByLoginDateByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _userSessionDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.LoginDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _userSessionDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingCurrentlyLoginAsync()
        {
            try
            {
                var result = await _userSessionDal.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsOnline==true
                }, null, y => y.User);
                return result.OrderByDescending(i => i.LoginDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _userSessionDal.GetAllIncludeAsync(new Expression<Func<UserSession, bool>>[]
                {

                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<IEnumerable<UserSession>> GetAllIncludingUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _userSessionDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<UserSession, bool>>[]
                {

                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public IEnumerable<UserSession> GetAllUserSessionForAdminHome()
        {
            try
            {
                var result = _userSessionDal.GetAllInclude(new Expression<Func<UserSession, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).Take(150).ToList();
            }
            catch (Exception)
            {
                return new List<UserSession>();
            }
        }

        public async Task<UserSession> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _userSessionDal.GetIncludeAsync(i => i.Id == id, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userSessionDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userSessionDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userSessionDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userSessionDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
