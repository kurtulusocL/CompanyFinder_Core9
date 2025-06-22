using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class ReportAnswerDal : EntityRepositoryBase<ReportAnswer, ApplicationDbContext>, IReportAnswerDal
    {
        private readonly ApplicationDbContext _context;
        public ReportAnswerDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int ReportAnswerCounter()
        {
            try
            {
                return _context.ReportAnswers.Count();
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
                var active = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Active the entity.", ex);
            }
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var active = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (active != null)
                {
                    active.IsActive = false;
                    active.SuspendedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var deleted = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = true;
                    deleted.DeletedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
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
                var deleted = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (deleted != null)
                {
                    deleted.IsDeleted = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Not Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotSatisfieldAsync(int id)
        {
            try
            {
                var satisfield = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (satisfield != null)
                {
                    satisfield.IsSatisfield = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotSatisfield the entity.", ex);
            }
        }

        public async Task<bool> SetSatisfieldAsync(int id)
        {
            try
            {
                var satisfield = await _context.Set<ReportAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (satisfield != null)
                {
                    satisfield.IsSatisfield = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Satisfield the entity.", ex);
            }
        }
    }
}
