using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class BlogManager : IBlogService
    {
        readonly IBlogDal _blogDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public BlogManager(IBlogDal blogDal, IHttpContextAccessor httpContextAccessor)
        {
            _blogDal = blogDal;
            _httpContextAccessor = httpContextAccessor;
        }
        public int BlogCounter()
        {
            var result = _blogDal.BlogCounter();
            return result;
        }

        public async Task<bool> CreateAsync(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/blog/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Desc = desc,
                            BlogCategoryId = blogCategoryId,
                            CompanyId = companyId,
                            UserId = userId
                        };

                        entity.ImageUrl = fileName;
                        var results = await _blogDal.AddAsync(entity);
                        if (!results)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Blog entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _blogDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blogDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingBlogHitForCompanyUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, y => y.Hits);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingBlogLikesForCompanyByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, y => y.Likes);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = await _blogDal.GetAllIncludeByIdAsync(categoryId, "BlogCategoryId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "Id was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "Id was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public IEnumerable<Blog> GetAllIncludingCompanyBlogs(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = _blogDal.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.BlogCategory, y => y.Comments);
                return result.OrderByDescending(i => i.CreatedDate).Take(6).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {

                }, null, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingForAdminByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "Id was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                {

                }, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingMostCommentBlogsAsync()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Comments.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingMostHitBlogsAsync()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingMostLikedBlogsAsync()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingMostSavedBlogsAsync()
        {
            try
            {
                var result = await _blogDal.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public IEnumerable<Blog> GetAllIncludingRandomBlog()
        {
            try
            {
                return _blogDal.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company).OrderByDescending(i => Guid.NewGuid()).Take(10).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public IEnumerable<Blog> GetAllIncludingRandomBlogForHome()
        {
            try
            {
                var result = _blogDal.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => Guid.NewGuid()).Take(9).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingSavedBlogsForSaveContentBlogByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                }, y => y.SaveContents, y => y.Likes, y => y.Hits);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingYourMostCommentBlogsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Comments.Count()>0
                 }, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Comments.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingYourMostHitBlogsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                 }, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingYourMostLikedBlogsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                 }, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllIncludingYourMostSavedBlogsAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _blogDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<Blog, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SaveContents.Count()>0
                 }, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SaveContents);
                return result.OrderByDescending(i => i.SaveContents.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public IEnumerable<Blog> GetAllSiteMap()
        {
            try
            {
                var result = _blogDal.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }

        public Blog GetBlogById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return _blogDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<Blog> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _blogDal.GetIncludeAsync(i => i.Id == id, y => y.BlogCategory, y => y.Company, y => y.User, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SaveContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Blog GetIncludeBlogCompanyInfoForBlogDetailByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _blogDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.CompanyId == companyId, y => y.Company);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Blog GetIncludingBlogForHeaderByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _blogDal.Get(i => i.IsActive == true && i.IsDeleted == false && i.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blogDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blogDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blogDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blogDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, int id, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/blog/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Console.WriteLine($"Path is preparing: {directoryPath}");
                        Directory.CreateDirectory(directoryPath);
                    }
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(directoryPath, fileName);
                    try
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Desc = desc,
                            BlogCategoryId = blogCategoryId,
                            CompanyId = companyId,
                            UserId = userId,
                            Id = id
                        };

                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        var results = await _blogDal.UpdateAsync(entity);
                        if (!results)
                        {
                            errors.Add($"Error {fileName}.");
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"Error {fileName} : {ex.Message}");
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }
    }
}
