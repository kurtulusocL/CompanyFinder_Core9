using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class PricingManager : IPricingService
    {
        readonly IPricingDal _pricingDal;
        public PricingManager(IPricingDal pricingDal)
        {
            _pricingDal = pricingDal;
        }

        public async Task<bool> CreateAsync(Pricing entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _pricingDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Pricing entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _pricingDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _pricingDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<Pricing> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _pricingDal.GetIncludeAsync(i => i.Id == id, y => y.PricingCategory, y=>y.PricingContacts);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Pricing>> GettAllIncludingAsync()
        {
            try
            {
                var result = await _pricingDal.GetAllIncludeAsync(new Expression<Func<Pricing, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.PricingCategory, y => y.PricingContacts);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Pricing>();
            }
        }

        public async Task<IEnumerable<Pricing>> GettAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = await _pricingDal.GetAllIncludeByIdAsync(categoryId, "PricingCategoryId", new Expression<Func<Pricing, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.PricingCategory, y => y.PricingContacts);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Pricing>();
            }
        }

        public async Task<IEnumerable<Pricing>> GettAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _pricingDal.GetAllIncludeAsync(new Expression<Func<Pricing, bool>>[]
                {

                }, null, y => y.PricingCategory, y => y.PricingContacts);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Pricing>();
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _pricingDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _pricingDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _pricingDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _pricingDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Pricing entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _pricingDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }
    }
}
