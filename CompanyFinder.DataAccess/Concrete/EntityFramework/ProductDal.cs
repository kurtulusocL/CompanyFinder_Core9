using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class ProductDal : EntityRepositoryBase<Product, ApplicationDbContext>, IProductDal
    {
        private readonly ApplicationDbContext _context;
        public ProductDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsAsync()
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            try
            {
                var popularProducts = await _context.Products.Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(c => c.Likes)
                    .Include(c => c.Hits)
                    .Include(c => c.SaveContents)
                    .Include(c => c.Comments)
                    .Include(c => c.Questions)
                    .Include(c => c.Company)
                    .Include(c => c.Company.City)
                    .Include(c => c.Company.Country)
                    .Include(c => c.ProductCategory)
                    .Include(c => c.ProductSubcategory)
                    .AsSplitQuery()
                    .Select(p => new
                    {
                        Product = p,
                        LikeCount = p.Likes.Count(l => l.CreatedDate >= startDate && l.CreatedDate <= endDate),
                        HitCount = p.Hits.Count(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate),
                        SavedCount = p.SaveContents.Count(s => s.CreatedDate >= startDate && s.CreatedDate <= endDate),
                        QuestionCount = p.Questions.Count(q => q.CreatedDate >= startDate && q.CreatedDate <= endDate),
                        CommentCount = p.Comments.Count(cm => cm.CreatedDate >= startDate && cm.CreatedDate <= endDate)
                    })
                    .OrderByDescending(x => x.LikeCount * 0.3 + x.HitCount * 0.2 + x.SavedCount * 0.2 + x.CommentCount * 0.1 + x.QuestionCount * 0.2)
                    .Take(120)
                    .Select(x => x.Product)
                    .ToListAsync();

                return popularProducts;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting weekly popular companies.", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllIncludingWeeklyPopularProductsForAdminAsync()
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            try
            {
                var popularProducts = await _context.Products
                    .Include(c => c.Likes)
                    .Include(c => c.Hits)
                    .Include(c => c.SaveContents)
                    .Include(c => c.Comments)
                    .Include(c => c.Questions)
                    .Include(c => c.Company)
                    .Include(c => c.Company.City)
                    .Include(c => c.Company.Country)
                    .Include(c => c.ProductCategory)
                    .Include(c => c.ProductSubcategory)
                    .AsSplitQuery()
                    .Select(p => new
                    {
                        Product = p,
                        LikeCount = p.Likes.Count(l => l.CreatedDate >= startDate && l.CreatedDate <= endDate),
                        HitCount = p.Hits.Count(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate),
                        SavedCount = p.SaveContents.Count(s => s.CreatedDate >= startDate && s.CreatedDate <= endDate),
                        QuestionCount = p.Questions.Count(q => q.CreatedDate >= startDate && q.CreatedDate <= endDate),
                        CommentCount = p.Comments.Count(cm => cm.CreatedDate >= startDate && cm.CreatedDate <= endDate)
                    })
                    .OrderByDescending(x => x.LikeCount * 0.3 + x.HitCount * 0.2 + x.SavedCount * 0.2 + x.CommentCount * 0.1 + x.QuestionCount * 0.2)
                    .Take(360)
                    .Select(x => x.Product)
                    .ToListAsync();

                return popularProducts;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting weekly popular companies.", ex);
            }
        }

        public int ProductCounter()
        {
            try
            {
                return _context.Products.Count();
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
                var product = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                product.IsActive = true;

                var comments = await _context.Set<Comment>().Where(a => a.ProductId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(h => h.ProductId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(l => l.ProductId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.ProductId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = true;
                }

                var questions = await _context.Set<Question>().Where(q => q.ProductId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(r => r.ProductId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.ProductId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }

                var stocks = await _context.Set<Stock>().Where(b => b.ProductId == id).ToListAsync();
                foreach (var stock in stocks)
                {
                    stock.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetAvailableAsync(int id)
        {
            try
            {
                var available = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (available != null)
                {
                    available.IsAvailable = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Available the entity.", ex);
            }
        }

        public async Task<bool> SetCommentableAsync(int id)
        {
            try
            {
                var commentable = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentable != null)
                {
                    commentable.IsCommentable = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Commentable the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var product = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                product.IsActive = false;
                product.SuspendedDate = DateTime.Now.ToLocalTime();

                var comments = await _context.Set<Comment>().Where(a => a.ProductId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.ProductId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.ProductId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.ProductId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = false;
                    picture.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(q => q.ProductId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = false;
                    question.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.ProductId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.ProductId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var stocks = await _context.Set<Stock>().Where(b => b.ProductId == id).ToListAsync();
                foreach (var stock in stocks)
                {
                    stock.IsActive = false;
                    stock.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var product = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                product.IsDeleted = true;
                product.DeletedDate = DateTime.Now.ToLocalTime();

                var comments = await _context.Set<Comment>().Where(a => a.ProductId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.ProductId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.ProductId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.ProductId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = true;
                    picture.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(q => q.ProductId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = true;
                    question.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.ProductId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.ProductId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var stocks = await _context.Set<Stock>().Where(b => b.ProductId == id).ToListAsync();
                foreach (var stock in stocks)
                {
                    stock.IsDeleted = true;
                    stock.DeletedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotAvailableAsync(int id)
        {
            try
            {
                var available = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (available != null)
                {
                    available.IsAvailable = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotAvailable the entity.", ex);
            }
        }

        public async Task<bool> SetNotCommentableAsync(int id)
        {
            try
            {
                var commentable = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (commentable != null)
                {
                    commentable.IsCommentable = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotCommentable the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var product = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (product == null)
                {
                    return false;
                }
                product.IsDeleted = false;

                var comments = await _context.Set<Comment>().Where(a => a.ProductId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(h => h.ProductId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(l => l.ProductId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.ProductId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = false;
                }

                var questions = await _context.Set<Question>().Where(q => q.ProductId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(r => r.ProductId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.ProductId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }

                var stocks = await _context.Set<Stock>().Where(b => b.ProductId == id).ToListAsync();
                foreach (var stock in stocks)
                {
                    stock.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotQuestionableAsync(int id)
        {
            try
            {
                var questionable = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (questionable != null)
                {
                    questionable.IsQuestionable = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Questionable the entity.", ex);
            }
        }

        public async Task<bool> SetQuestionableAsync(int id)
        {
            try
            {
                var questionable = await _context.Set<Product>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (questionable != null)
                {
                    questionable.IsQuestionable = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting NotQuestionable the entity.", ex);
            }
        }
    }
}
