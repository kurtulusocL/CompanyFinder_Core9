using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class AdDal : EntityRepositoryBase<Ad, ApplicationDbContext>, IAdDal
    {
        private readonly ApplicationDbContext _context;
        public AdDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int AdCounter()
        {
            try
            {
                return _context.Ads.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsActive = true;

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = true;
                }

                var hits = await _context.Set<Hit>().Where(b => b.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
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
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsActive = false;
                data.SuspendedDate = DateTime.Now.ToLocalTime();

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = true;
                    adTarget.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(b => b.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsDeleted = true;
                data.DeletedDate = DateTime.Now.ToLocalTime();

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = true;
                    adTarget.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(b => b.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
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
                var data = await _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsDeleted = false;

                var adTargets = await _context.Set<AdTarget>().Where(a => a.AdId == id).ToListAsync();
                foreach (var adTarget in adTargets)
                {
                    adTarget.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(b => b.AdId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public Ad SimilarHitRead(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                var hitRead = _context.Set<Ad>().Where(i => i.Id == id).FirstOrDefault();
                if (hitRead != null && hitRead.SimilarHit >= 0)
                {
                    hitRead.SimilarHit++;
                    _context.SaveChanges();
                    return hitRead;
                }
                hitRead.SimilarHit = 0;
                _context.SaveChanges();
                return hitRead;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Hit the entity.", ex);
            }
        }
    }
}
