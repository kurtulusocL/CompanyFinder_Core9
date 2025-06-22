using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ExceptionLoggerManager : IExceptionLoggerService
    {
        readonly IExceptionLoggerDal _exceptionLoggerDal;
        public ExceptionLoggerManager(IExceptionLoggerDal exceptionLoggerDal)
        {
            _exceptionLoggerDal = exceptionLoggerDal;
        }

        public async Task<bool> DeleteAsync(ExceptionLogger entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _exceptionLoggerDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _exceptionLoggerDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ExceptionLogger>> GetAllAsync()
        {
            try
            {
                var result = await _exceptionLoggerDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ExceptionLogger>();
            }
        }

        public async Task<IEnumerable<ExceptionLogger>> GetAllForAdmin()
        {
            try
            {
                var result = await _exceptionLoggerDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ExceptionLogger>();
            }
        }

        public async Task<ExceptionLogger> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _exceptionLoggerDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _exceptionLoggerDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _exceptionLoggerDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _exceptionLoggerDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _exceptionLoggerDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
