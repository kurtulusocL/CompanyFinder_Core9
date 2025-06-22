using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AdCompanyManager : IAdCompanyService
    {
        readonly IAdCompanyDal _adCompanyDal;
        public AdCompanyManager(IAdCompanyDal adCompanyDal)
        {
            _adCompanyDal = adCompanyDal;
        }

        public async Task<bool> CreateAsync(AdCompany entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");
                var result = await _adCompanyDal.AddAsync(entity);
                if (result)
                {
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AdCompany entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _adCompanyDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _adCompanyDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<AdCompany>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _adCompanyDal.GetAllIncludeAsync(new Expression<Func<AdCompany, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ads);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdCompany>();
            }
        }

        public async Task<IEnumerable<AdCompany>> GetAllIncludingForAddAdAsync()
        {
            try
            {
                var result = await _adCompanyDal.GetAllIncludeAsync(new Expression<Func<AdCompany, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Ads);
                return result.OrderByDescending(i => i.Ads.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<AdCompany>();
            }
        }

        public async Task<IEnumerable<AdCompany>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _adCompanyDal.GetAllIncludeAsync(new Expression<Func<AdCompany, bool>>[]
                {

                }, null, y => y.Ads);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdCompany>();
            }
        }

        public async Task<AdCompany> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _adCompanyDal.GetIncludeAsync(i => i.Id == id, y => y.Ads);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _adCompanyDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _adCompanyDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _adCompanyDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _adCompanyDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(AdCompany entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _adCompanyDal.UpdateAsync(entity);
                if (result)
                {
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
