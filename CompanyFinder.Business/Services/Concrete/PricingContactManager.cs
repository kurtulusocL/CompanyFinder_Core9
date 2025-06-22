using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class PricingContactManager : IPricingContactService
    {
        readonly IPricingContactDal _pricingContactDal;
        public PricingContactManager(IPricingContactDal pricingContactDal)
        {
            _pricingContactDal = pricingContactDal;
        }

        public async Task<bool> CreateAsync(string nameSurname, string companyName, string? email, string phoneNumber, string desc, int? pricingId)
        {
            try
            {
                if (pricingId == null)
                    throw new ArgumentNullException(nameof(pricingId), "pricingId was null");

                var model = new PricingContact
                {
                    NameSurname = nameSurname,
                    CompanyName = companyName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Desc = desc,
                    PricingId = pricingId
                };
                if (model != null)
                {
                    var result = await _pricingContactDal.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(PricingContact entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _pricingContactDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _pricingContactDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<PricingContact>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _pricingContactDal.GetAllIncludeAsync(new Expression<Func<PricingContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Pricing);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<PricingContact>();
            }
        }

        public async Task<IEnumerable<PricingContact>> GetAllIncludingByPricingId(int? pricingId)
        {
            try
            {
                if (pricingId == null)
                    throw new ArgumentNullException(nameof(pricingId), "pricingId was null");

                var result = await _pricingContactDal.GetAllIncludeByIdAsync(pricingId, "PricingId", new Expression<Func<PricingContact, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Pricing);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<PricingContact>();
            }
        }

        public async Task<IEnumerable<PricingContact>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _pricingContactDal.GetAllIncludeAsync(new Expression<Func<PricingContact, bool>>[]
                {

                }, null, y => y.Pricing);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<PricingContact>();
            }
        }

        public async Task<PricingContact> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _pricingContactDal.GetIncludeAsync(i => i.Id == id, y => y.Pricing);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _pricingContactDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _pricingContactDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _pricingContactDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _pricingContactDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
