using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class AdCompanyDal : EntityRepositoryBase<AdCompany, ApplicationDbContext>, IAdCompanyDal
    {
        private readonly ApplicationDbContext _context;
        public AdCompanyDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<AdCompany>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return true;
                }
                data.IsActive = false;

                var ads = await _context.Set<Ad>().Where(a => a.AdCompanyId == id).ToListAsync();
                foreach (var ad in ads)
                {
                    ad.IsActive = true;
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
                var data = await _context.Set<AdCompany>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsActive = false;
                data.SuspendedDate = DateTime.Now.ToLocalTime();

                var ads = await _context.Set<Ad>().Where(a => a.AdCompanyId == id).ToListAsync();
                foreach (var ad in ads)
                {
                    ad.IsActive = false;
                    ad.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var data = await _context.Set<AdCompany>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = true;
                data.DeletedDate = DateTime.Now.ToLocalTime();

                var ads = await _context.Set<Ad>().Where(a => a.AdCompanyId == id).ToListAsync();
                foreach (var ad in ads)
                {
                    ad.IsDeleted = true;
                    ad.DeletedDate = DateTime.Now.ToLocalTime();
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
                var data = await _context.Set<AdCompany>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }
                data.IsDeleted = false;

                var ads = await _context.Set<Ad>().Where(a => a.AdCompanyId == id).ToListAsync();
                foreach (var ad in ads)
                {
                    ad.IsDeleted = false;
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
