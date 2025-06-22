using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class CompanyFormDal : EntityRepositoryBase<CompanyForm, ApplicationDbContext>, ICompanyFormDal
    {
        private readonly ApplicationDbContext _context;
        public CompanyFormDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var companyForm = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyForm == null)
                {
                    return false;
                }
                companyForm.IsActive = true;

                var companyFormAnswers = await _context.Set<CompanyFormAnswer>().Where(a => a.CompanyFormId == id).ToListAsync();
                foreach (var companyFormAnswer in companyFormAnswers)
                {
                    companyFormAnswer.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyFormId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyFormId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetAnswerable(int id)
        {
            try
            {
                var answerable = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (answerable != null)
                {
                    answerable.IsAnswerable = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Answerable the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var companyForm = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyForm == null)
                {
                    return false;
                }
                companyForm.IsActive = false;
                companyForm.SuspendedDate = DateTime.Now;

                var companyFormAnswers = await _context.Set<CompanyFormAnswer>().Where(a => a.CompanyFormId == id).ToListAsync();
                foreach (var companyFormAnswer in companyFormAnswers)
                {
                    companyFormAnswer.IsActive = false;
                    companyForm.SuspendedDate = DateTime.Now;
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyFormId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    companyForm.SuspendedDate = DateTime.Now;
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyFormId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    companyForm.SuspendedDate = DateTime.Now;
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    companyForm.SuspendedDate = DateTime.Now;
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
                var companyForm = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyForm == null)
                {
                    return false;
                }
                companyForm.IsDeleted = true;
                companyForm.DeletedDate = DateTime.Now.ToLocalTime();

                var companyFormAnswers = await _context.Set<CompanyFormAnswer>().Where(a => a.CompanyFormId == id).ToListAsync();
                foreach (var companyFormAnswer in companyFormAnswers)
                {
                    companyFormAnswer.IsDeleted = true;
                    companyFormAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyFormId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyFormId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
                }
               
                var reports = await _context.Set<Report>().Where(r => r.CompanyFormId == id).ToListAsync();
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

        public async Task<bool> SetNotAnswerable(int id)
        {
            try
            {
                var answerable = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (answerable != null)
                {
                    answerable.IsAnswerable = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Answerable the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var companyForm = await _context.Set<CompanyForm>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (companyForm == null)
                {
                    return false;
                }
                companyForm.IsDeleted = false;

                var companyFormAnswers = await _context.Set<CompanyFormAnswer>().Where(a => a.CompanyFormId == id).ToListAsync();
                foreach (var companyFormAnswer in companyFormAnswers)
                {
                    companyFormAnswer.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyFormId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyFormId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyFormId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
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
