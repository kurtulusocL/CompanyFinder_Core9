using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class BlogDal : EntityRepositoryBase<Blog, ApplicationDbContext>, IBlogDal
    {
        private readonly ApplicationDbContext _context;
        public BlogDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int BlogCounter()
        {
            try
            {
                return _context.Blogs.Count();
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
                var blog = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (blog == null)
                {
                    return false;
                }
                blog.IsActive = true;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(h => h.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(l => l.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(r => r.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
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
                var blog = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (blog == null)
                {
                    return false;
                }
                blog.IsActive = false;
                blog.SuspendedDate = DateTime.Now.ToLocalTime();

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = false;
                    picture.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var blog = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (blog == null)
                {
                    return false;
                }
                blog.IsDeleted = true;
                blog.DeletedDate = DateTime.Now.ToLocalTime();
                
                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = true;
                    picture.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.Now.ToLocalTime();
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
                var blog = await _context.Set<Blog>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (blog == null)
                {
                    return false;
                }
                blog.IsDeleted = false;

                var comments = await _context.Set<Comment>().Where(a => a.BlogId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(h => h.BlogId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(l => l.BlogId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.BlogId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(r => r.BlogId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.BlogId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
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
