using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class PricingDal : EntityRepositoryBase<Pricing, ApplicationDbContext>, IPricingDal
    {
        private readonly ApplicationDbContext _context;
        public PricingDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var pricing = await _context.Set<Pricing>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (pricing == null)
                {
                    return false;
                }
                pricing.IsActive = true;

                var pricingContacts = await _context.Set<PricingContact>().Where(a => a.PricingId == id).ToListAsync();
                foreach (var pricingContact in pricingContacts)
                {
                    pricingContact.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var pricing = await _context.Set<Pricing>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (pricing == null)
                {
                    return false;
                }
                pricing.IsActive = false;
                pricing.SuspendedDate = DateTime.Now.ToLocalTime();

                var pricingContacts = await _context.Set<PricingContact>().Where(a => a.PricingId == id).ToListAsync();
                foreach (var pricingContact in pricingContacts)
                {
                    pricingContact.IsActive = false;
                    pricingContact.SuspendedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var pricing = await _context.Set<Pricing>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (pricing == null)
                {
                    return false;
                }
                pricing.IsDeleted = true;
                pricing.DeletedDate = DateTime.Now.ToLocalTime();
                
                var pricingContacts = await _context.Set<PricingContact>().Where(a => a.PricingId == id).ToListAsync();
                foreach (var pricingContact in pricingContacts)
                {
                    pricingContact.IsDeleted = true;
                    pricingContact.DeletedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var pricing = await _context.Set<Pricing>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (pricing == null)
                {
                    return false;
                }
                pricing.IsDeleted = false;

                var pricingContacts = await _context.Set<PricingContact>().Where(a => a.PricingId == id).ToListAsync();
                foreach (var pricingContact in pricingContacts)
                {
                    pricingContact.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }
    }
}
