using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ReportManager : IReportService
    {
        readonly IReportDal _reportDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public ReportManager(IReportDal reportDal, IHttpContextAccessor httpContextAccessor)
        {
            _reportDal = reportDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateBlogReportAsync(string title, string desc, int reportCategoryId, int? blogId, string userId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    BlogId = blogId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Blog Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCommentAnswerReportAsync(string title, string desc, int reportCategoryId, int? commentAnswerId, string userId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "CommentAnswerId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CommentAnswerId = commentAnswerId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Comment Answer Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCommentReportAsync(string title, string desc, int reportCategoryId, int? commentId, string userId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CommentId = commentId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Comment Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyReportAsync(string title, string desc, int reportCategoryId, int? companyId, string userId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CompanyId = companyId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Report the entity.", ex);
            }
        }

        public async Task<bool> CreateProductReportAsync(string title, string desc, int reportCategoryId, int? productId, string userId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    ProductId = productId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Product Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyPartnershipReportAsync(string title, string desc, int reportCategoryId, int? companyPartnershipId, string userId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CompanyPartnershipId = companyPartnershipId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Blog Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyFormReportAsync(string title, string desc, int reportCategoryId, int? companyFormId, string userId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CompanyFormId = companyFormId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Form Report the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyFormAnswerReportAsync(string title, string desc, int reportCategoryId, int? companyFormAnswerId, string userId)
        {
            try
            {
                if (companyFormAnswerId == null)
                    throw new ArgumentNullException(nameof(companyFormAnswerId), "companyFormAnswerId was null");

                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new Report
                {
                    Title = title,
                    Desc = desc,
                    ReportCategoryId = reportCategoryId,
                    CompanyFormAnswerId = companyFormAnswerId,
                    UserId = userId
                };
                if (entity != null)
                {
                    var result = await _reportDal.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding Company Form Report the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Report entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _reportDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _reportDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }
        public async Task<IEnumerable<Report>> GetAllIncludingNotFixedReportsAsync()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFixed==false,
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingFixedReportsAsync()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFixed==true,
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.FixedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }
        public async Task<IEnumerable<Report>> GetAllIncludingAnsweredAsync()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ReportAnswers.Count()>0
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }
        public async Task<IEnumerable<Report>> GetAllIncludingNotAnsweredAsync()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ReportAnswers.Count()==0
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(blogId, "BlogId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = await _reportDal.GetAllIncludeByIdAsync(categoryId, "ReportCategoryId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCommentAnswerIdAsync(int? commentAnswerId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "CommentAnswer ID was null");
               
                var result = await _reportDal.GetAllIncludeByIdAsync(commentAnswerId, "CommentAnswerId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "comment Id was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(commentId, "CommentId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "company Id was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByProductIdAsync(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "product Id was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(productId, "ProductId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCompanyPartnershipIdAsync(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCompanyFormIdAsync(int? companyFormId)
        {
            try
            {
                if (companyFormId == null)
                    throw new ArgumentNullException(nameof(companyFormId), "companyFormId was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(companyFormId, "CompanyFormId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyForm.Company, y => y.CompanyFormAnswer);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByCompanyFormAnswerIdAsync(int? companyFormAnswerId)
        {
            try
            {
                if (companyFormAnswerId == null)
                    throw new ArgumentNullException(nameof(companyFormAnswerId), "companyFormAnswerId was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(companyFormAnswerId, "CompanyFormAnswerId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyFormAnswer.CompanyForm);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "user Id was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }
        public async Task<IEnumerable<Report>> GetAllIncludingReportForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "user Id was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Report, bool>>[]
                {

                }, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _reportDal.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {

                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyForm.Company);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<Report> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _reportDal.GetIncludeAsync(i => i.Id == id, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership, y => y.CompanyPartnership.Company, y => y.CompanyForm, y => y.CompanyFormAnswer, y => y.CompanyFormAnswer.CompanyForm, y => y.CompanyForm.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Report HitRead(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id was null");

            return _reportDal.HitRead(id);
        }

        public async Task<bool> SetFixedAsync(int id)
        {
            var result = await _reportDal.SetFixedAsync(id);
            return result;
        }
        public async Task<bool> SetNotFixedAsync(int id)
        {
            var result = await _reportDal.SetNotFixedAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _reportDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _reportDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _reportDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _reportDal.SetNotDeletedAsync(id);
            return result;
        }

        public int ReportCounter()
        {
            var result = _reportDal.ReportCounter();
            return result;
        }

        public IEnumerable<Report> GetAllIncludingReportsForAdminHeader()
        {
            try
            {
                return _reportDal.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFixed==false
                }, null, y => y.ReportCategory, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Product, y => y.User, y => y.ReportAnswers, y => y.CompanyPartnership).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public IEnumerable<Report> GetAllIncludingProductReportForCompanyUserByProductId(int? productId)
        {
            try
            {
                if (productId == null)
                    throw new ArgumentNullException(nameof(productId), "productId was null");

                var result = _reportDal.GetAllIncludeById(productId, "ProductId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.Product, y => y.ReportCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public IEnumerable<Report> GetAllIncludingCompanyReportForCompanyUserByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _reportDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.Company, y => y.ReportCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public IEnumerable<Report> GetAllIncludingBlogReportForCompanyUserByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var result = _reportDal.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.Blog, y => y.ReportCategory, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingReportForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _reportDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.Company, y => y.Product, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.ReportCategory, y => y.User, y => y.ReportAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingCompanyReportForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _reportDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.UserId==userId
                }, y => y.Company, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingProductReportForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _reportDal.GetAllIncludingByPropertyPathAsync(userId, "Product.UserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Product.UserId==userId
                }, y => y.Product, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public async Task<IEnumerable<Report>> GetAllIncludingBlogReportForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _reportDal.GetAllIncludingByPropertyPathAsync(userId, "Blog.UserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Blog.UserId==userId
                }, y => y.Blog, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public IEnumerable<Report> GetAllIncludingPartnershipReportForCompanyUserByCompanyPartnershipId(int? companyPartnershipId)
        {
            try
            {
                if (companyPartnershipId == null)
                    throw new ArgumentNullException(nameof(companyPartnershipId), "companyPartnershipId was null");

                var result = _reportDal.GetAllIncludeById(companyPartnershipId, "CompanyPartnershipId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, y => y.CompanyPartnership, y => y.ReportCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }
    }
}
