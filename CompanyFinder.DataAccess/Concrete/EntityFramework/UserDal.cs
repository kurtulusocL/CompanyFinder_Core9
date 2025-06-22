using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class UserDal : EntityRepositoryBase<User, ApplicationDbContext>, IUserDal
    {
        private readonly ApplicationDbContext _context;
        public UserDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsActive = true;

                var appointments = await _context.Set<Appointment>().Where(a => a.UserId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsActive = true;
                }

                var blogs = await _context.Set<Blog>().Where(b => b.UserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = true;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(b => b.UserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsActive = true;
                }

                var comments = await _context.Set<Comment>().Where(b => b.UserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = true;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = true;
                }

                var companies = await _context.Set<Company>().Where(b => b.UserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsActive = true;
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var companyMessage in companyMessages)
                {
                    companyMessage.IsActive = true;
                }

                var hits = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = true;
                }

                var likes = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = true;
                }

                var products = await _context.Set<Product>().Where(b => b.UserId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsActive = true;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsActive = true;
                }

                var questions = await _context.Set<Question>().Where(b => b.UserId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = true;
                }

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsActive = true;
                }

                var reports = await _context.Set<Report>().Where(b => b.UserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = true;
                }

                var savedContents = await _context.Set<SavedContent>().Where(b => b.UserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = true;
                }

                var toDos = await _context.Set<ToDo>().Where(b => b.UserId == id).ToListAsync();
                foreach (var toDo in toDos)
                {
                    toDo.IsActive = true;
                }

                var userSessions = await _context.Set<UserSession>().Where(b => b.UserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsActive = false;
                data.SuspendedDate = DateTime.Now.ToLocalTime();

                var appointments = await _context.Set<Appointment>().Where(a => a.UserId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsActive = false;
                    appointment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var blogs = await _context.Set<Blog>().Where(b => b.UserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsActive = false;
                    blog.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(b => b.UserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsActive = false;
                    cancelMembership.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var comments = await _context.Set<Comment>().Where(b => b.UserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsActive = false;
                    comment.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsActive = false;
                    commentAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companies = await _context.Set<Company>().Where(b => b.UserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsActive = false;
                    company.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var companyMessage in companyMessages)
                {
                    companyMessage.IsActive = false;
                    companyMessage.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsActive = false;
                    hit.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsActive = false;
                    like.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var products = await _context.Set<Product>().Where(b => b.UserId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsActive = false;
                    product.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var profileImages = await _context.Set<ProfileImage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsActive = false;
                    profileImage.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(b => b.UserId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsActive = false;
                    question.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsActive = false;
                    questionAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(b => b.UserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsActive = false;
                    report.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(b => b.UserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsActive = false;
                    savedContent.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var toDos = await _context.Set<ToDo>().Where(b => b.UserId == id).ToListAsync();
                foreach (var toDo in toDos)
                {
                    toDo.IsActive = false;
                    toDo.SuspendedDate = DateTime.Now.ToLocalTime();
                }

                var userSessions = await _context.Set<UserSession>().Where(b => b.UserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsActive = false;
                    userSession.SuspendedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsDeleted = true;
                data.DeletedDate = DateTime.Now.ToLocalTime();

                var appointments = await _context.Set<Appointment>().Where(a => a.UserId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsDeleted = true;
                    appointment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var blogs = await _context.Set<Blog>().Where(b => b.UserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = true;
                    blog.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(b => b.UserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsDeleted = true;
                    cancelMembership.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var comments = await _context.Set<Comment>().Where(b => b.UserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = true;
                    comment.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = true;
                    commentAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companies = await _context.Set<Company>().Where(b => b.UserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsDeleted = true;
                    company.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var companyMessage in companyMessages)
                {
                    companyMessage.IsDeleted = true;
                    companyMessage.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var hits = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = true;
                    hit.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var likes = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = true;
                    like.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var products = await _context.Set<Product>().Where(b => b.UserId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var profileImages = await _context.Set<ProfileImage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsDeleted = true;
                    profileImage.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var questions = await _context.Set<Question>().Where(b => b.UserId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = true;
                    question.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsDeleted = true;
                    questionAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var reports = await _context.Set<Report>().Where(b => b.UserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = true;
                    report.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var savedContents = await _context.Set<SavedContent>().Where(b => b.UserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = true;
                    savedContent.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var toDos = await _context.Set<ToDo>().Where(b => b.UserId == id).ToListAsync();
                foreach (var toDo in toDos)
                {
                    toDo.IsDeleted = true;
                    toDo.DeletedDate = DateTime.Now.ToLocalTime();
                }

                var userSessions = await _context.Set<UserSession>().Where(b => b.UserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsDeleted = true;
                    userSession.DeletedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var data = await _context.Set<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return false;
                }

                data.IsDeleted = false;

                var appointments = await _context.Set<Appointment>().Where(a => a.UserId == id).ToListAsync();
                foreach (var appointment in appointments)
                {
                    appointment.IsDeleted = false;
                }

                var blogs = await _context.Set<Blog>().Where(b => b.UserId == id).ToListAsync();
                foreach (var blog in blogs)
                {
                    blog.IsDeleted = false;
                }

                var cancelMemberships = await _context.Set<CancelMembership>().Where(b => b.UserId == id).ToListAsync();
                foreach (var cancelMembership in cancelMemberships)
                {
                    cancelMembership.IsDeleted = false;
                }

                var comments = await _context.Set<Comment>().Where(b => b.UserId == id).ToListAsync();
                foreach (var comment in comments)
                {
                    comment.IsDeleted = false;
                }

                var commentAnswers = await _context.Set<CommentAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var commentAnswer in commentAnswers)
                {
                    commentAnswer.IsDeleted = false;
                }

                var companies = await _context.Set<Company>().Where(b => b.UserId == id).ToListAsync();
                foreach (var company in companies)
                {
                    company.IsDeleted = false;
                }

                var companyMessages = await _context.Set<CompanyMessage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var companyMessage in companyMessages)
                {
                    companyMessage.IsDeleted = false;
                }

                var hits = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var hit in hits)
                {
                    hit.IsDeleted = false;
                }

                var likes = await _context.Set<Hit>().Where(b => b.UserId == id).ToListAsync();
                foreach (var like in likes)
                {
                    like.IsDeleted = false;
                }

                var products = await _context.Set<Product>().Where(b => b.UserId == id).ToListAsync();
                foreach (var product in products)
                {
                    product.IsDeleted = false;
                }

                var profileImages = await _context.Set<ProfileImage>().Where(b => b.UserId == id).ToListAsync();
                foreach (var profileImage in profileImages)
                {
                    profileImage.IsDeleted = false;
                }

                var questions = await _context.Set<Question>().Where(b => b.UserId == id).ToListAsync();
                foreach (var question in questions)
                {
                    question.IsDeleted = false;
                }

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(b => b.UserId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsDeleted = false;
                }

                var reports = await _context.Set<Report>().Where(b => b.UserId == id).ToListAsync();
                foreach (var report in reports)
                {
                    report.IsDeleted = false;
                }

                var savedContents = await _context.Set<SavedContent>().Where(b => b.UserId == id).ToListAsync();
                foreach (var savedContent in savedContents)
                {
                    savedContent.IsDeleted = false;
                }

                var toDos = await _context.Set<ToDo>().Where(b => b.UserId == id).ToListAsync();
                foreach (var toDo in toDos)
                {
                    toDo.IsDeleted = false;
                }

                var userSessions = await _context.Set<UserSession>().Where(b => b.UserId == id).ToListAsync();
                foreach (var userSession in userSessions)
                {
                    userSession.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public int UserCounter()
        {
            try
            {
                return _context.Users.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
