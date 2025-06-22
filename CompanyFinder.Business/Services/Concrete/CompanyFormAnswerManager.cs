using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyFormAnswerManager : ICompanyFormAnswerService
    {
        readonly ICompanyFormAnswerDal _companyFormAnswerDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyFormAnswerManager(ICompanyFormAnswerDal companyFormAnswerDal, IHttpContextAccessor httpContextAccessor)
        {
            _companyFormAnswerDal = companyFormAnswerDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string? title, string desc, int? companyFormId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User Id was null.");
                }

                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var entity = new CompanyFormAnswer
                {
                    Title = title,
                    Desc = desc,
                    CompanyFormId = companyFormId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _companyFormAnswerDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyFormAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyFormAnswerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyFormAnswerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyFormAnswerDal.GetAllIncludeAsync(new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyForm, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingByCompanyFormIdAsync(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = await _companyFormAnswerDal.GetAllIncludeByIdAsync(companyFormId, "CompanyFormId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyForm, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public IEnumerable<CompanyFormAnswer> GetAllIncludingCompanyFormAnswersForExplorer(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = _companyFormAnswerDal.GetAllIncludeById(companyFormId, "CompanyFormId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CompanyFormId==companyFormId
                }, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingCompanyUserFormAnswerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyFormAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public IEnumerable<CompanyFormAnswer> GetAllIncludingCompanyUserYourFormAnswerByCompanyFormId(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyId was null");

                var result = _companyFormAnswerDal.GetAllIncludeById(companyFormId, "CompanyFormId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingCompanyUserYourFormAnswerByCompanyFormIdAsync(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = await _companyFormAnswerDal.GetAllIncludeByIdAsync(companyFormId, "CompanyFormId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyForm, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _companyFormAnswerDal.GetAllIncludeAsync(new Expression<Func<CompanyFormAnswer, bool>>[]
                {

                }, null, y => y.CompanyForm, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyFormAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {

                }, y => y.CompanyForm, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<IEnumerable<CompanyFormAnswer>> GetAllIncludingUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyFormAnswerDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<CompanyFormAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyForm, y => y.User, y => y.Reports);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyFormAnswer>();
            }
        }

        public async Task<CompanyFormAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyFormAnswerDal.GetIncludeAsync(i => i.Id == id, y => y.CompanyForm, y => y.User, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyFormAnswerDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyFormAnswerDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyFormAnswerDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyFormAnswerDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
