using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities.AppUser;

namespace CompanyFinder.Business.Services.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<bool> DeleteAsync(User entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _userDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<User>> GetAllAdminForAddTask()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                 {
                        i=>i.IsActive==true,
                        i=>i.IsDeleted==false,
                        i=>i.Title!= "Company"
                 }, null, y => y.ToDos);
                return result.OrderByDescending(i => i.ToDos.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingAdminUserAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Title!="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y=>y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingCompanyUserAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Title=="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingDeletedAdminAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==true,
                    i=>i.Title!="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingDeletedCompanyAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==true,
                    i=>i.Title=="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {

                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingSuspendedAdminAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==false,
                    i=>i.IsDeleted==false,
                    i=>i.Title!="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<IEnumerable<User>> GetAllIncludingSuspendedCompanyAsync()
        {
            try
            {
                var result = await _userDal.GetAllIncludeAsync(new Expression<Func<User, bool>>[]
                {
                    i=>i.IsActive==false,
                    i=>i.IsDeleted==false,
                    i=>i.Title=="Company"
                }, null, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _userDal.GetIncludeAsync(i => i.Id == id, y => y.Appointments, y => y.Blogs, y => y.CancelMemberships, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.CompanyMessages, y => y.Hits, y => y.Likes, y => y.ProfileImages, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents, y => y.ToDos, y => y.UserSessions, y => y.CompanyPartnerships, y => y.CompanyPartnerships, y => y.CompanyFormAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<User> GetCompanyUserForProfileByUserIdAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _userDal.GetIncludeAsync(i => i.IsActive == true && i.IsDeleted == false && i.Id == id, y => y.Appointments, y => y.Blogs, y => y.Comments, y => y.CommentAnswers, y => y.Companies, y => y.Hits, y => y.Likes, y => y.Products, y => y.Questions, y => y.QuestionAnswers, y => y.Reports, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public User GetUserCompanyHeaderByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _userDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.Id == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public User GetUserForAdminHomeById(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return _userDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.Id == id, y => y.ProfileImages, y => y.ToDos);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            var result = await _userDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(string id)
        {
            var result = await _userDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(string id)
        {
            var result = await _userDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(string id)
        {
            var result = await _userDal.SetNotDeletedAsync(id);
            return result;
        }

        public int UserCounter()
        {
            var result = _userDal.UserCounter();
            return result;
        }
    }
}
