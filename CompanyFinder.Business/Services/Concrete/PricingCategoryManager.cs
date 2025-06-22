using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.Business.Services.Concrete
{
    public class PricingCategoryManager : IPricingCategoryService
    {
        readonly IPricingCategoryDal _pricingCategoryDal;
        public PricingCategoryManager(IPricingCategoryDal pricingCategoryDal)
        {
            _pricingCategoryDal = pricingCategoryDal;
        }

        public async Task<bool> CreateAsync(PricingCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _pricingCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(PricingCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _pricingCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _pricingCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<PricingCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _pricingCategoryDal.GetIncludeAsync(i => i.Id == id, y => y.Pricings);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<PricingCategory>> GettAllIncludingAsync()
        {
            try
            {
                var result = await _pricingCategoryDal.GetAllIncludeAsync(new Expression<Func<PricingCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Pricings);
                return result.OrderByDescending(i => i.Pricings.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<PricingCategory>();
            }
        }

        public async Task<IEnumerable<PricingCategory>> GettAllIncludingForAddPricingAsync()
        {
            try
            {
                var result = await _pricingCategoryDal.GetAllIncludeAsync(new Expression<Func<PricingCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Pricings);
                return result.OrderByDescending(i => i.Pricings.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<PricingCategory>();
            }
        }

        public async Task<IEnumerable<PricingCategory>> GettAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _pricingCategoryDal.GetAllIncludeAsync(new Expression<Func<PricingCategory, bool>>[]
                {
                    
                }, null, y => y.Pricings);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<PricingCategory>();
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _pricingCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _pricingCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _pricingCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _pricingCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(PricingCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _pricingCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }
    }
}
