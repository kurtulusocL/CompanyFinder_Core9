using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class ReportDal : EntityRepositoryBase<Report, ApplicationDbContext>, IReportDal
    {
        private readonly ApplicationDbContext _context;
        public ReportDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Report HitRead(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                var hitRead = _context.Set<Report>().Where(i => i.Id == id).FirstOrDefault();
                if (hitRead != null && hitRead.Hit >= 0)
                {
                    hitRead.Hit++;
                    _context.SaveChanges();
                    return hitRead;
                }
                hitRead.Hit = 0;
                _context.SaveChanges();
                return hitRead;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Hit the entity.", ex);
            }
        }

        public int ReportCounter()
        {
            try
            {
                return _context.Reports.Count();
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
                var report = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (report == null)
                {
                    return false;
                }
                report.IsActive = true;

                var reportAnswers = await _context.Set<ReportAnswer>().Where(a => a.ReportId == id).ToListAsync();
                foreach (var reportAnswer in reportAnswers)
                {
                    reportAnswer.IsActive = true;
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
                var report = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (report == null)
                {
                    return false;
                }
                report.IsActive = false;
                report.SuspendedDate = DateTime.Now.ToLocalTime();

                var reportAnswers = await _context.Set<ReportAnswer>().Where(a => a.ReportId == id).ToListAsync();
                foreach (var reportAnswer in reportAnswers)
                {
                    reportAnswer.IsActive = false;
                    reportAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var report = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (report == null)
                {
                    return false;
                }
                report.IsDeleted = true;
                report.DeletedDate = DateTime.Now.ToLocalTime();
                
                var reportAnswers = await _context.Set<ReportAnswer>().Where(a => a.ReportId == id).ToListAsync();
                foreach (var reportAnswer in reportAnswers)
                {
                    reportAnswer.IsDeleted = true;
                    reportAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetFixedAsync(int id)
        {
            try
            {
                var isFixed = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isFixed != null)
                {
                    isFixed.IsFixed = true;
                    isFixed.FixedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Fixed Report the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var report = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (report == null)
                {
                    return false;
                }
                report.IsDeleted = false;

                var reportAnswers = await _context.Set<ReportAnswer>().Where(a => a.ReportId == id).ToListAsync();
                foreach (var reportAnswer in reportAnswers)
                {
                    reportAnswer.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotFixedAsync(int id)
        {
            try
            {
                var isFixed = await _context.Set<Report>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (isFixed != null)
                {
                    isFixed.IsFixed = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotFixed Report the entity.", ex);
            }
        }
    }
}
