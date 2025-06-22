using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class CommentDal : EntityRepositoryBase<Comment, ApplicationDbContext>, ICommentDal
    {
        private readonly ApplicationDbContext _context;
        public CommentDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int CommentCounter()
        {
            try
            {
                return _context.Comments.Count();
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
                var comment = await _context.Set<Comment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (comment == null)
                {
                    return false;
                }
                comment.IsActive = true;

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.CommentId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(b => b.CommentId == id).ToListAsync();
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
                var comment = await _context.Set<Comment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (comment == null)
                {
                    return false;
                }
                comment.IsActive = false;
                comment.SuspendedDate = DateTime.Now.ToLocalTime();

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.CommentId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = false;
                    commentAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(b => b.CommentId == id).ToListAsync();
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
                var comment = await _context.Set<Comment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (comment == null)
                {
                    return false;
                }
                comment.IsDeleted = true;
                comment.DeletedDate = DateTime.Now.ToLocalTime();

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.CommentId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = true;
                    commentAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(b => b.CommentId == id).ToListAsync();
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
                var comment = await _context.Set<Comment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (comment == null)
                {
                    return false;
                }
                comment.IsDeleted = false;

                var commentAnswers = await _context.Set<CommentAnswer>().Where(a => a.CommentId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(b => b.CommentId == id).ToListAsync();
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
