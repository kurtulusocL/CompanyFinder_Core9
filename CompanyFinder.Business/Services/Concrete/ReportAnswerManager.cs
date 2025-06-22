using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ReportAnswerManager : IReportAnswerService
    {
        readonly IReportAnswerDal _reportAnswerDal;
        public ReportAnswerManager(IReportAnswerDal reportAnswerDal)
        {
            _reportAnswerDal = reportAnswerDal;
        }

        public async Task<bool> CreateAsync(string title, string desc, int? reportId)
        {
            try
            {
                if (reportId == null)
                    throw new ArgumentNullException(nameof(reportId), "reportId was null");

                var entity = new ReportAnswer
                {
                    Title = title,
                    Desc = desc,
                    ReportId = reportId
                };
                if (entity != null)
                {
                    var result = await _reportAnswerDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ReportAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _reportAnswerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _reportAnswerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _reportAnswerDal.GetAllIncludeAsync(new Expression<Func<ReportAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingByReportIdAsync(int? reportId)
        {
            try
            {
                if (reportId == null)
                    throw new ArgumentNullException(nameof(reportId), "reportId was null");

                var result = await _reportAnswerDal.GetAllIncludeByIdAsync(reportId, "ReportId", new Expression<Func<ReportAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _reportAnswerDal.GetAllIncludeAsync(new Expression<Func<ReportAnswer, bool>>[]
                {

                }, null, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingForAdminByReportIdAsync(int? reportId)
        {
            try
            {
                if (reportId == null)
                    throw new ArgumentNullException(nameof(reportId), "reportId was null");

                var result = await _reportAnswerDal.GetAllIncludeByIdAsync(reportId, "ReportId", new Expression<Func<ReportAnswer, bool>>[]
                {

                }, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingNotSatisfieldsAsync()
        {
            try
            {
                var result = await _reportAnswerDal.GetAllIncludeAsync(new Expression<Func<ReportAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSatisfield==false
                }, null, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public IEnumerable<ReportAnswer> GetAllIncludingReportAnswerForCompanyUserByReportId(int? reportId)
        {
            try
            {
                if (reportId == null)
                    throw new ArgumentNullException(nameof(reportId), "reportId was null");

                var result = _reportAnswerDal.GetAllIncludeById(reportId, "", new Expression<Func<ReportAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<IEnumerable<ReportAnswer>> GetAllIncludingSatisfieldsAsync()
        {
            try
            {
                var result = await _reportAnswerDal.GetAllIncludeAsync(new Expression<Func<ReportAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsSatisfield==true
                }, null, y => y.Report);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportAnswer>();
            }
        }

        public async Task<ReportAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _reportAnswerDal.GetIncludeAsync(i => i.Id == id, y => y.Report);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public int ReportAnswerCounter()
        {
            var result=_reportAnswerDal.ReportAnswerCounter();
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _reportAnswerDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _reportAnswerDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _reportAnswerDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _reportAnswerDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotSatisfieldAsync(int id)
        {
            var result = await _reportAnswerDal.SetNotSatisfieldAsync(id);
            return result;
        }

        public async Task<bool> SetSatisfieldAsync(int id)
        {
            var result = await _reportAnswerDal.SetSatisfieldAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string desc, int? reportId, int id)
        {
            try
            {
                if (reportId == null)
                    throw new ArgumentNullException(nameof(reportId), "reportId was null");

                var entity = new ReportAnswer
                {
                    Title = title,
                    Desc = desc,
                    ReportId = reportId,
                    Id = id
                };
                if (entity != null)
                {
                    entity.UpdatedDate = DateTime.Now.ToLocalTime();
                    var result = await _reportAnswerDal.UpdateAsync(entity);
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
