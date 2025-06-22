using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class CompanyFormAnswerDal : EntityRepositoryBase<CompanyFormAnswer, ApplicationDbContext>, ICompanyFormAnswerDal
    {
        private readonly ApplicationDbContext _context;
        public CompanyFormAnswerDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var companyFormAnswer = await _context.Set<CompanyFormAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyFormAnswer == null)
                {
                    return false;
                }
                companyFormAnswer.IsActive = true;

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting DeActive the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var companyFormAnswer = await _context.Set<CompanyFormAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyFormAnswer == null)
                {
                    return false;
                }
                companyFormAnswer.IsActive = false;
                companyFormAnswer.SuspendedDate = DateTime.Now.ToLocalTime();

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
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
                var companyFormAnswer = await _context.Set<CompanyFormAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyFormAnswer == null)
                {
                    return false;
                }
                companyFormAnswer.IsDeleted = true;
                companyFormAnswer.DeletedDate = DateTime.Now.ToLocalTime();

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.Now.ToLocalTime();
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
                var companyFormAnswer = await _context.Set<CompanyFormAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyFormAnswer == null)
                {
                    return false;
                }
                companyFormAnswer.IsDeleted = false;

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormAnswerId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotDeleted the entity.", ex);
            }
        }
    }
}
