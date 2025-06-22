using System.Linq.Expressions;
using CompanyFinder.Business.Constants.Cache.CacheKeys;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyFinder.Business.Constants.Cache.CacheServices.UserCache
{
    public class UserCacheManager : IUserCacheService
    {
        IMemoryCache _memoryCache;
        IUserDal _userDal;
        public UserCacheManager(IMemoryCache memoryCache, IUserDal userDal)
        {
            _memoryCache = memoryCache;
            _userDal = userDal;
        }
        public async Task UserDeleteCache(string id)
        {
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingAdminUser, out List<User> allAdminUsers))
            {
                var updatedAdminUsers = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                     i=>i.Title!="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions);
                _memoryCache.Set(UserCacheKeys.GetAllIncludingAdminUser, updatedAdminUsers, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncluding, out List<User> allUsers))
            {
                var updatedUsers = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions);
                _memoryCache.Set(UserCacheKeys.GetAllIncludingAdminUser, updatedUsers, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingCompanyUser, out List<User> allCompanyUsers))
            {
                var updatedCompanyUsers = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingCompanyUser, updatedCompanyUsers, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingDeletedAdmin, out List<User> allDeletedAdmin))
            {
                var updatedDeletedAdmins = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingDeletedAdmin, updatedDeletedAdmins, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingDeletedCompany, out List<User> allDeletedCompanies))
            {
                var updatedDeletedCompanies = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingDeletedCompany, updatedDeletedCompanies, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingForAdmin, out List<User> allUsersForAdmin))
            {
                var updatedUsersForAdmin = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingForAdmin, updatedUsersForAdmin, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingSuspendedAdmin, out List<User> allSuspendedAdmins))
            {
                var updatedSuspendedAdmins = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingSuspendedAdmin, updatedSuspendedAdmins, TimeSpan.FromMinutes(25));
            }
            if (_memoryCache.TryGetValue(UserCacheKeys.GetAllIncludingSuspendedCompany, out List<User> allSuspendedCompanies))
            {
                var updatedSuspendedCompanies = await _userDal.GetAllAsync();
                _memoryCache.Set(UserCacheKeys.GetAllIncludingSuspendedCompany, updatedSuspendedCompanies, TimeSpan.FromMinutes(25));
            }
            if (id != null)
            {
                string userCacheKey = UserCacheKeys.GetByIdAsync(id);
                if (_memoryCache.TryGetValue(userCacheKey, out User singleUser))
                {
                    var updatedUser = await _userDal.GetAsync(a => a.Id == id);
                    if (updatedUser != null)
                    {
                        _memoryCache.Set(userCacheKey, updatedUser, TimeSpan.FromMinutes(25));
                    }
                }
            }
        }
    }
}
