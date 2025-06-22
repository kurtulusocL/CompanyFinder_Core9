using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class CompanyDal : EntityRepositoryBase<Company, ApplicationDbContext>, ICompanyDal
    {
        private readonly ApplicationDbContext _context;
        public CompanyDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int CompanyCounter()
        {
            try
            {
                return _context.Companies.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesAsync()
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            try
            {
                var popularCompanies = await _context.Companies.Where(i => i.IsActive == true && i.IsDeleted == false)
                    .Include(c => c.Likes)
                    .Include(c => c.Hits)
                    .Include(c => c.SaveContents)
                    .Include(c => c.Comments)
                    .Include(c => c.Appointments)
                    .Include(c => c.City)
                    .Include(c => c.Country)
                    .Include(c => c.CompanyCategory)
                    .Include(c => c.CompanySubcategory)
                    .AsSplitQuery()
                    .Select(c => new
                    {
                        Company = c,
                        LikeCount = c.Likes.Count(l => l.CreatedDate >= startDate && l.CreatedDate <= endDate),
                        HitCount = c.Hits.Count(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate),
                        SavedCount = c.SaveContents.Count(s => s.CreatedDate >= startDate && s.CreatedDate <= endDate),
                        AppointmentCount = c.Appointments.Count(ap => ap.CreatedDate >= startDate && ap.CreatedDate <= endDate),
                        CommentCount = c.Comments.Count(cm => cm.CreatedDate >= startDate && cm.CreatedDate <= endDate)
                    })
                    .OrderByDescending(x => x.LikeCount * 0.3 + x.HitCount * 0.1 + x.SavedCount * 0.2 + x.CommentCount * 0.1 + x.AppointmentCount * 0.3)
                    .Take(48)
                    .Select(x => x.Company)
                    .ToListAsync();

                return popularCompanies;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting weekly popular companies.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingWeeklyPopularCompaniesFortAdminAsync()
        {
            var startDate = DateTime.UtcNow.AddDays(-7);
            var endDate = DateTime.UtcNow;

            try
            {
                var popularCompanies = await _context.Companies
                    .Include(c => c.Likes)
                    .Include(c => c.Hits)
                    .Include(c => c.SaveContents)
                    .Include(c => c.Comments)
                    .Include(c => c.Appointments)
                    .Include(c => c.City)
                    .Include(c => c.Country)
                    .Include(c => c.CompanyCategory)
                    .Include(c => c.CompanySubcategory)
                    .AsSplitQuery()
                    .Select(c => new
                    {
                        Company = c,
                        LikeCount = c.Likes.Count(l => l.CreatedDate >= startDate && l.CreatedDate <= endDate),
                        HitCount = c.Hits.Count(h => h.CreatedDate >= startDate && h.CreatedDate <= endDate),
                        SavedCount = c.SaveContents.Count(s => s.CreatedDate >= startDate && s.CreatedDate <= endDate),
                        AppointmentCount = c.Appointments.Count(ap => ap.CreatedDate >= startDate && ap.CreatedDate <= endDate),
                        CommentCount = c.Comments.Count(cm => cm.CreatedDate >= startDate && cm.CreatedDate <= endDate)
                    })
                    .OrderByDescending(x => x.LikeCount * 0.3 + x.HitCount * 0.1 + x.SavedCount * 0.2 + x.CommentCount * 0.1 + x.AppointmentCount * 0.3)
                    .Take(240)
                    .Select(x => x.Company)
                    .ToListAsync();

                return popularCompanies;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting weekly popular companies.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var company = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (company == null)
                {
                    return false;
                }
                company.IsActive = false;

                var appointments = await _context.Set<Appointment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsActive = true;
                }

                var blogs = await _context.Set<Blog>().Where(b => b.CompanyId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = true;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(cc => cc.CompanyId == id).ToListAsync();
                foreach (var contact in companyContacts)
                {
                    contact.IsActive = true;
                }

                var comments = await _context.Set<Comment>().Where(c => c.CompanyId == id/* || c.CompanyId == null*/).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var message in companyMessages)
                {
                    message.IsActive = true;
                }

                var companyPartnerships = await _context.Set<CompanyPartnership>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyPartnership in companyPartnerships)
                {
                    companyPartnership.IsActive = true;
                }

                var companyForms = await _context.Set<CompanyForm>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyForm in companyForms)
                {
                    companyForm.IsActive = true;
                }

                var customers = await _context.Set<Customer>().Where(cu => cu.CompanyId == id).ToListAsync();
                foreach (var customer in customers)
                {
                    customer.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = true;
                }

                var products = await _context.Set<Product>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsActive = true;
                }

                var questions = await _context.Set<Question>().Where(q => q.CompanyId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.CompanyId == id).ToListAsync();
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

        public async Task<bool> SetCommentableAsync(int id)
        {
            try
            {
                var commentable = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                var company = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (company == null)
                {
                    return false;
                }
                company.IsActive = false;
                company.SuspendedDate = DateTime.Now.ToLocalTime();

                var appointments = await _context.Set<Appointment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsActive = false;
                    appointment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var blogs = await _context.Set<Blog>().Where(b => b.CompanyId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = false;
                    blog.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(cc => cc.CompanyId == id).ToListAsync();
                foreach (var contact in companyContacts)
                {
                    contact.IsActive = false;
                    contact.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var comments = await _context.Set<Comment>().Where(c => c.CompanyId == id/* || c.CompanyId == null*/).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var message in companyMessages)
                {
                    message.IsActive = false;
                    message.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companyPartnerships = await _context.Set<CompanyPartnership>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyPartnership in companyPartnerships)
                {
                    companyPartnership.IsActive = false;
                    companyPartnership.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companyForms = await _context.Set<CompanyForm>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyForm in companyForms)
                {
                    companyForm.IsActive = false;
                    companyForm.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var customers = await _context.Set<Customer>().Where(cu => cu.CompanyId == id).ToListAsync();
                foreach (var customer in customers)
                {
                    customer.IsActive = false;
                    customer.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsActive = false;
                    picture.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var products = await _context.Set<Product>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsActive = false;
                    product.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(q => q.CompanyId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = false;
                    question.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.CompanyId == id).ToListAsync();
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
                var company = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (company == null)
                {
                    return false;
                }
                company.IsDeleted = true;
                company.DeletedDate = DateTime.Now.ToLocalTime();

                var appointments = await _context.Set<Appointment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsDeleted = true;
                    appointment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var blogs = await _context.Set<Blog>().Where(b => b.CompanyId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = true;
                    blog.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(cc => cc.CompanyId == id).ToListAsync();
                foreach (var contact in companyContacts)
                {
                    contact.IsDeleted = true;
                    contact.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var comments = await _context.Set<Comment>().Where(c => c.CompanyId == id/* || c.CompanyId == null*/).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var message in companyMessages)
                {
                    message.IsDeleted = true;
                    message.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companyPartnerships = await _context.Set<CompanyPartnership>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyPartnership in companyPartnerships)
                {
                    companyPartnership.IsDeleted = true;
                    companyPartnership.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companyForms = await _context.Set<CompanyForm>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyForm in companyForms)
                {
                    companyForm.IsDeleted = true;
                    companyForm.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var customers = await _context.Set<Customer>().Where(cu => cu.CompanyId == id).ToListAsync();
                foreach (var customer in customers)
                {
                    customer.IsDeleted = true;
                    customer.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var pictures = await _context.Set<Picture>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = true;
                    picture.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var products = await _context.Set<Product>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(q => q.CompanyId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = true;
                    question.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.CompanyId == id).ToListAsync();
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

        public async Task<bool> SetNotCommentableAsync(int id)
        {
            try
            {
                var commentable = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
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
                var company = await _context.Set<Company>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (company == null)
                {
                    return false;
                }
                company.IsDeleted = false;

                var appointments = await _context.Set<Appointment>().Where(a => a.CompanyId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsDeleted = false;
                }

                var blogs = await _context.Set<Blog>().Where(b => b.CompanyId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = false;
                }

                var companyContacts = await _context.Set<CompanyContact>().Where(cc => cc.CompanyId == id).ToListAsync();
                foreach (var contact in companyContacts)
                {
                    contact.IsDeleted = false;
                }

                var comments = await _context.Set<Comment>().Where(c => c.CompanyId == id/* || c.CompanyId == null*/).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var message in companyMessages)
                {
                    message.IsDeleted = false;
                }

                var companyPartnerships = await _context.Set<CompanyPartnership>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyPartnership in companyPartnerships)
                {
                    companyPartnership.IsDeleted = false;
                }

                var companyForms = await _context.Set<CompanyForm>().Where(cm => cm.CompanyId == id).ToListAsync();
                foreach (var companyForm in companyForms)
                {
                    companyForm.IsDeleted = false;
                }

                var customers = await _context.Set<Customer>().Where(cu => cu.CompanyId == id).ToListAsync();
                foreach (var customer in customers)
                {
                    customer.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(h => h.CompanyId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Like>().Where(l => l.CompanyId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var pictures = await _context.Set<Picture>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var picture in pictures)
                {
                    picture.IsDeleted = false;
                }

                var products = await _context.Set<Product>().Where(p => p.CompanyId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsDeleted = false;
                }

                var questions = await _context.Set<Question>().Where(q => q.CompanyId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(r => r.CompanyId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(sc => sc.CompanyId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
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
