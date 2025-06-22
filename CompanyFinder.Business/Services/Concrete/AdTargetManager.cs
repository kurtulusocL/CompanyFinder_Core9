using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AdTargetManager : IAdTargetService
    {
        readonly IAdTargetDal _adTargetDal;
        public AdTargetManager(IAdTargetDal adTargetDal)
        {
            _adTargetDal = adTargetDal;
        }

        public async Task<bool> CreateAsync(int? cityId, int? countryId, int? adId)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "Ad Id was null");

                var model = new AdTarget
                {
                    CityId = cityId,
                    CountryId = countryId,
                    AdId = adId
                };
                if (model != null)
                {
                    var result = await _adTargetDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AdTarget entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _adTargetDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _adTargetDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _adTargetDal.GetAllIncludeAsync(new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.Ad, y => y.City, y => y.Country);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllIncludingByAdIdAsync(int? adId)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "Ad Id was null");

                var result = await _adTargetDal.GetAllIncludeByIdAsync(adId, "AdId", new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, y => y.Ad, y => y.City, y => y.Country);
                return result.OrderByDescending(i => i.Ad.ExpiryDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllIncludingByCityIdAsync(int? cityId)
        {
            try
            {
                if (cityId == null)
                    throw new ArgumentNullException(nameof(cityId), "Ad Id was null");

                var result = await _adTargetDal.GetAllIncludeByIdAsync(cityId, "CityId", new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, y => y.Ad, y => y.City, y => y.Country);
                return result.OrderByDescending(i => i.Ad.ExpiryDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllIncludingByCountryIdAsync(int? countryId)
        {
            try
            {
                if (countryId == null)
                    throw new ArgumentNullException(nameof(countryId), "Ad Id was null");

                var result = await _adTargetDal.GetAllIncludeByIdAsync(countryId, "CountryId", new Expression<Func<AdTarget, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, y => y.Ad, y => y.City, y => y.Country);
                return result.OrderByDescending(i => i.Ad.ExpiryDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public async Task<IEnumerable<AdTarget>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _adTargetDal.GetAllIncludeAsync(new Expression<Func<AdTarget, bool>>[]
                {
                    
                }, null, y => y.Ad, y => y.City, y => y.Country);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AdTarget>();
            }
        }

        public async Task<AdTarget> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _adTargetDal.GetIncludeAsync(i => i.Id == id, y => y.Ad, y => y.City, y => y.Country);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _adTargetDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _adTargetDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _adTargetDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _adTargetDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(int? cityId, int? countryId, int? adId, int id)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "Ad Id was null");

                var model = new AdTarget
                {
                    CityId = cityId,
                    CountryId = countryId,
                    AdId = adId,
                    Id = id,
                    UpdatedDate=DateTime.Now.ToLocalTime()
                };
                if (model != null)
                {
                    var result = await _adTargetDal.UpdateAsync(model);
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
