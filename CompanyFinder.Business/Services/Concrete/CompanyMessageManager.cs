using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyMessageManager : ICompanyMessageService
    {
        readonly ICompanyMessageDal _companyMessageDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyMessageManager(ICompanyMessageDal companyMessageDal, IHttpContextAccessor httpContextAccessor)
        {
            _companyMessageDal = companyMessageDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string? title, string subject, string desc, int? companyId, string userId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "User Id was null");
                }
                var entity = new CompanyMessage
                {
                    Title = title,
                    Subject = subject,
                    Desc = desc,
                    CompanyId = companyId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _companyMessageDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyMessage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyMessageDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyMessageDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyMessage>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyMessageDal.GetAllIncludeAsync(new Expression<Func<CompanyMessage, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, null, y => y.User, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyMessage>();
            }
        }

        public async Task<IEnumerable<CompanyMessage>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyMessageDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyMessage, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, y => y.Company, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyMessage>();
            }
        }

        public async Task<IEnumerable<CompanyMessage>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "companyId was null");

                var result = await _companyMessageDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyMessage, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, y => y.Company, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyMessage>();
            }
        }

        public async Task<IEnumerable<CompanyMessage>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _companyMessageDal.GetAllIncludeAsync(new Expression<Func<CompanyMessage, bool>>[] {
                
                }, null, y => y.User, y => y.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyMessage>();
            }
        }

        public async Task<CompanyMessage> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companyMessageDal.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyMessageDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyMessageDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyMessageDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyMessageDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string? title, string subject, string desc, int? companyId, string userId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "User Id was null");
                }
                var entity = new CompanyMessage
                {
                    Title = title,
                    Subject = subject,
                    Desc = desc,
                    CompanyId = companyId,
                    UserId = userId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _companyMessageDal.UpdateAsync(entity);
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
