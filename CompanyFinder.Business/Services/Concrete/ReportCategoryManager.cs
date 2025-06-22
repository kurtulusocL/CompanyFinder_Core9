using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ReportCategoryManager : IReportCategoryService
    {
        readonly IReportCategoryDal _reportCategoryDal;
        public ReportCategoryManager(IReportCategoryDal reportCategoryDal)
        {
            _reportCategoryDal = reportCategoryDal;
        }

        public async Task<bool> CreateAsync(ReportCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _reportCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ReportCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _reportCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _reportCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ReportCategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _reportCategoryDal.GetAllIncludeAsync(new Expression<Func<ReportCategory, bool>>[]
                    {
                         i => i.IsActive == true,
                         i => i.IsDeleted == false,
                    }, null,
                   i => i.Reports);
                return result.OrderByDescending(i => i.Reports.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ReportCategory>();
            }
        }

        public async Task<IEnumerable<ReportCategory>> GetAllIncludingForAddReportAsync()
        {
            try
            {
                var result = await _reportCategoryDal.GetAllIncludeAsync(new Expression<Func<ReportCategory, bool>>[]
                    {
                         i => i.IsActive == true,
                         i => i.IsDeleted == false,
                    }, null,
                   i => i.Reports);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<ReportCategory>();
            }
        }

        public async Task<IEnumerable<ReportCategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _reportCategoryDal.GetAllIncludeAsync(new Expression<Func<ReportCategory, bool>>[]
                    {
                         
                    }, null,
                   i => i.Reports);
                return result.OrderByDescending(i => i.Reports.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<ReportCategory>();
            }
        }

        public async Task<ReportCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id is Null");

                return await _reportCategoryDal.GetIncludeAsync(i => i.Id == id, x => x.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _reportCategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _reportCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _reportCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _reportCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(ReportCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _reportCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
