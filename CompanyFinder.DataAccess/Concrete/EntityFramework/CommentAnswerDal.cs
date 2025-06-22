using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class CommentAnswerDal : EntityRepositoryBase<CommentAnswer, ApplicationDbContext>, ICommentAnswerDal
    {
        private readonly ApplicationDbContext _context;
        public CommentAnswerDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int CommentAnswerCounter()
        {
            try
            {
                return _context.CommentAnswers.Count();
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
                var commentAnswer = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentAnswer == null)
                {
                    return false;
                }
                commentAnswer.IsActive = true;

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
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

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var commentAnswer = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentAnswer == null)
                {
                    return false;
                }
                commentAnswer.IsActive = false;
                commentAnswer.SuspendedDate = DateTime.Now.ToLocalTime();

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
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
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var commentAnswer = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentAnswer == null)
                {
                    return false;
                }
                commentAnswer.IsDeleted = true;
                commentAnswer.DeletedDate = DateTime.Now.ToLocalTime();

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
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
                var commentAnswer = await _context.Set<CommentAnswer>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentAnswer == null)
                {
                    return false;
                }
                commentAnswer.IsDeleted = false;

                var reports = await _context.Set<Report>().Where(a => a.CommentAnswerId == id).ToListAsync();
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
