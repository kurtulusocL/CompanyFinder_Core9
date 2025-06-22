using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class QuestionDal : EntityRepositoryBase<Question, ApplicationDbContext>, IQuestionDal
    {
        private readonly ApplicationDbContext _context;
        public QuestionDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Question HitRead(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                var hitRead = _context.Set<Question>().Where(i => i.Id == id).FirstOrDefault();
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

        public int QuestionCounter()
        {
            try
            {
                return _context.Questions.Count();
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
                var question = await _context.Set<Question>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (question == null)
                {
                    return false;
                }
                question.IsActive = true;

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(a => a.QuestionId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsActive = true;
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
                var question = await _context.Set<Question>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (question == null)
                {
                    return false;
                }
                question.IsActive = false;
                question.SuspendedDate = DateTime.Now.ToLocalTime();

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(a => a.QuestionId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsActive = false;
                    questionAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
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
                var question = await _context.Set<Question>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (question == null)
                {
                    return false;
                }
                question.IsDeleted = true;
                question.DeletedDate = DateTime.Now.ToLocalTime();

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(a => a.QuestionId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsDeleted = false;
                    questionAnswer.DeletedDate = DateTime.Now.ToLocalTime();
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
                var question = await _context.Set<Question>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (question == null)
                {
                    return false;
                }
                question.IsDeleted = false;

                var questionAnswers = await _context.Set<QuestionAnswer>().Where(a => a.QuestionId == id).ToListAsync();
                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.IsDeleted = false;
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
