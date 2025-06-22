using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CancelMembershipManager : ICancelMembershipService
    {
        readonly ICancelMembershipDal _cancelMembershipDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CancelMembershipManager(ICancelMembershipDal cancelMembershipDal, IHttpContextAccessor httpContextAccessor)
        {
            _cancelMembershipDal = cancelMembershipDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string title, string? otherReason, DateTime expectedCancelDate, int cancelMembershipReasonId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }

                var entity = new CancelMembership
                {
                    Title = title,
                    AnotherReason = otherReason,
                    ExpectedCancelDate = expectedCancelDate,
                    CancelMembershipReasonId = cancelMembershipReasonId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _cancelMembershipDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CancelMembership entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _cancelMembershipDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _cancelMembershipDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, null, y => y.CancelMembershipReason, y => y.User);
                return result.OrderByDescending(i => i.ExpectedCancelDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingByReasonIdAsync(int id)
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeByIdAsync(id, "CancelMembershipReasonId", new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User, y => y.CancelMembershipReason);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _cancelMembershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User, y => y.CancelMembershipReason);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingCancelledAsync()
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.IsCancelled==true
                }, null, y => y.CancelMembershipReason, y => y.User);
                return result.OrderByDescending(i => i.CancelDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingNotCancelledAsync()
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false,
                i=>i.IsCancelled==false
                }, null, y => y.CancelMembershipReason, y => y.User);
                return result.OrderByDescending(i => i.ExpectedCancelDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingCancelledForAdminAsync()
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[] {
                i=>i.IsCancelled==false
                }, null, y => y.CancelMembershipReason, y => y.User);
                return result.OrderByDescending(i => i.CancelDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[] {

                }, null, y => y.CancelMembershipReason, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingForAdminByReasonIdAsync(int id)
        {
            try
            {
                var result = await _cancelMembershipDal.GetAllIncludeByIdAsync(id, "CancelMembershipReasonId", new Expression<Func<CancelMembership, bool>>[]
                {

                }, y => y.User, y => y.CancelMembershipReason);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _cancelMembershipDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CancelMembership, bool>>[]
                {
                    
                }, y => y.User, y => y.CancelMembershipReason);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public async Task<CancelMembership> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _cancelMembershipDal.GetIncludeAsync(i => i.Id == id, y => y.User, y => y.CancelMembershipReason);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetCancelledAsync(int id)
        {
            var result = await _cancelMembershipDal.SetCancelledAsync(id);
            return result;
        }
        public async Task<bool> SetNotCancelledAsync(int id)
        {
            var result = await _cancelMembershipDal.SetNotCancelledAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _cancelMembershipDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _cancelMembershipDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _cancelMembershipDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _cancelMembershipDal.SetNotDeletedAsync(id);
            return result;
        }

        public IEnumerable<CancelMembership> GetAllIncludingCancelMembershipsForAdminHeader()
        {
            try
            {
                return _cancelMembershipDal.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCancelled==false
                }, null, y => y.CancelMembershipReason, y => y.User).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }
    }
}
