using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class BlogCategoryManager : IBlogCategoryService
    {
        readonly IBlogCategoryDal _blogCategoryDal;
        public BlogCategoryManager(IBlogCategoryDal blogCategoryDal)
        {
            _blogCategoryDal = blogCategoryDal;
        }

        public async Task<bool> CreateAsync(BlogCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _blogCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(BlogCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _blogCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blogCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<BlogCategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _blogCategoryDal.GetAllIncludeAsync(new Expression<Func<BlogCategory, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, null, y => y.Blogs);
                return result.OrderByDescending(i => i.Blogs.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public IEnumerable<BlogCategory> GetAllIncludingBlogCategories()
        {
            try
            {
                return _blogCategoryDal.GetAllInclude(new Expression<Func<BlogCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Blogs.Count()>0
                }, null, y => y.Blogs).OrderByDescending(i => i.Blogs.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public async Task<IEnumerable<BlogCategory>> GetAllIncludingForAddBlogAsync()
        {
            try
            {
                var result = await _blogCategoryDal.GetAllIncludeAsync(new Expression<Func<BlogCategory, bool>>[] {
                i=>i.IsActive==true,
                i=>i.IsDeleted==false
                }, null, y => y.Blogs);
                return result.OrderByDescending(i => i.Blogs.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public async Task<IEnumerable<BlogCategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _blogCategoryDal.GetAllIncludeAsync(new Expression<Func<BlogCategory, bool>>[] {

                }, null, y => y.Blogs);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public IEnumerable<BlogCategory> GetAllSiteMap()
        {
            try
            {
                var result = _blogCategoryDal.GetAllInclude(new Expression<Func<BlogCategory, bool>>[] {
                    i=>i.IsActive== true,
                    i=>i.IsDeleted==false
                }, null);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlogCategory>();
            }
        }

        public async Task<BlogCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _blogCategoryDal.GetIncludeAsync(i => i.Id == id, y => y.Blogs);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blogCategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blogCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blogCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blogCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(BlogCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _blogCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
