using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class SectorNewsDal : EntityRepositoryBase<SectorNews, ApplicationDbContext>, ISectorNewsDal
    {
        private readonly ApplicationDbContext _context;
        public SectorNewsDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var sectorNews = await _context.Set<SectorNews>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (sectorNews == null)
                {
                    return false;
                }
                sectorNews.IsActive = true;

                var hits = await _context.Set<Hit>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
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
                var sectorNews = await _context.Set<SectorNews>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (sectorNews == null)
                {
                    return false;
                }
                sectorNews.IsActive = false;
                sectorNews.SuspendedDate = DateTime.Now.ToLocalTime();

                var hits = await _context.Set<Hit>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var sectorNews = await _context.Set<SectorNews>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (sectorNews == null)
                {
                    return false;
                }
                sectorNews.IsDeleted = true;
                sectorNews.DeletedDate = DateTime.Now.ToLocalTime();

                var hits = await _context.Set<Hit>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
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
                var sectorNews = await _context.Set<SectorNews>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (sectorNews == null)
                {
                    return false;
                }
                sectorNews.IsDeleted = false;

                var hits = await _context.Set<Hit>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(a => a.SectorNewsId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
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
