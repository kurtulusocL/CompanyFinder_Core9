using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class QuestionManager : IQuestionService
    {
        readonly IQuestionDal _questionDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public QuestionManager(IQuestionDal questionDal, IHttpContextAccessor httpContextAccessor)
        {
            _questionDal = questionDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> CreateCompanyQuestionAsync(string text, int? companyId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Question
                {
                    Text = text,
                    CompanyId = companyId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _questionDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Question the entity.", ex);
            }
        }

        public async Task<bool> CreateProductQuestionAsync(string text, int? productId, string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Question
                {
                    Text = text,
                    ProductId = productId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _questionDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Question the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Question entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _questionDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _questionDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _questionDal.GetAllIncludeAsync(new Expression<Func<Question, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "Company Id was null");

                var result = await _questionDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Question, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "Product Id was null");

                var result = await _questionDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Question, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _questionDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Question, bool>>[]
                {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "User Id was null");

                var result = await _questionDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Question, bool>>[]
                {

                }, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public IEnumerable<Question> GetAllIncludingCompanyQuestionByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _questionDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Question, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingCompanyQuestionsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _questionDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<Question, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.UserId==userId,
                }, y => y.Company, y=>y.QuestionAnswers, y=>y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _questionDal.GetAllIncludeAsync(new Expression<Func<Question, bool>>[]
                {

                }, null, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public IEnumerable<Question> GetAllIncludingProductQuestionByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = _questionDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Question, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Product, y => y.User, y=>y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<IEnumerable<Question>> GetAllIncludingProductQuestionsForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _questionDal.GetAllIncludingByPropertyPathAsync(userId, "Product.UserId", new Expression<Func<Question, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Product.UserId==userId,
                }, y => y.Product,y=>y.User, y=>y.QuestionAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Question>();
            }
        }

        public async Task<Question> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return await _questionDal.GetIncludeAsync(i => i.Id == id, i => i.User, i => i.Company, i => i.Product, y => y.Product.Company, y => y.QuestionAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Question GetQuestionById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id));

                return _questionDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Question HitRead(int? id)
        {
            if(id==null)
                throw new ArgumentNullException(nameof(id),"Id was null");

            return _questionDal.HitRead(id);
        }

        public int QuestionCounter()
        {
            var result = _questionDal.QuestionCounter();
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _questionDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _questionDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _questionDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _questionDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
