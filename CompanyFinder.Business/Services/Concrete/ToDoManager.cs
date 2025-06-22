using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ToDoManager : IToDoService
    {
        readonly IToDoDal _toDoDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public ToDoManager(IToDoDal toDoDal, IHttpContextAccessor httpContextAccessor)
        {
            _toDoDal = toDoDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new ToDo
                {
                    Title = title,
                    Subtitle = subtitle,
                    Desc = desc,
                    StartDate = startDate,
                    EndDate = endDate,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _toDoDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ToDo entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _toDoDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _toDoDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _toDoDal.GetAllIncludeAsync(new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _toDoDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingFinishedAsync()
        {
            try
            {
                var result = await _toDoDal.GetAllIncludeAsync(new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFinished==true
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingFinishedTaskByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _toDoDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFinished==true
                }, y => y.User);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _toDoDal.GetAllIncludeAsync(new Expression<Func<ToDo, bool>>[]
                {

                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingOpenTaskAsync()
        {
            try
            {
                var result = await _toDoDal.GetAllIncludeAsync(new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFinished==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAllIncludingOpenTaskByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _toDoDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFinished==false
                }, y => y.User);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }

        public async Task<ToDo> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return  await _toDoDal.GetIncludeAsync(i => i.Id == id, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public ToDo HitRead(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id was null");

            return _toDoDal.HitRead(id);
        }
        public async Task<bool> SetFinishedAsync(int id)
        {
            var result = await _toDoDal.SetFinishedAsync(id);
            return result;
        }
        public async Task<bool> SetNotFinishedAsync(int id)
        {
            var result = await _toDoDal.SetNotFinishedAsync(id);
            return result;
        }
        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _toDoDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _toDoDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _toDoDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _toDoDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId, int id)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new ToDo
                {
                    Title = title,
                    Subtitle = subtitle,
                    Desc = desc,
                    StartDate = startDate,
                    EndDate = endDate,
                    UserId = userId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _toDoDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public IEnumerable<ToDo> GetAllIncludingTaksForAdminProfileByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = _toDoDal.GetAllIncludeById(userId, "UserId", new Expression<Func<ToDo, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ToDo>();
            }
        }
    }
}
