using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class FormCategoryManager : IFormCategoryService
    {
        readonly IFormCategoryDal _formCategoryDal;
        public FormCategoryManager(IFormCategoryDal formCategoryDal)
        {
            _formCategoryDal = formCategoryDal;
        }

        public async Task<bool> CreateAsync(FormCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _formCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(FormCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _formCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _formCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<FormCategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _formCategoryDal.GetAllIncludeAsync(new Expression<Func<FormCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CompanyForms.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<FormCategory>();
            }
        }

        public async Task<IEnumerable<FormCategory>> GetAllIncludingForAddAsync()
        {
            try
            {
                var result = await _formCategoryDal.GetAllIncludeAsync(new Expression<Func<FormCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyForms);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<FormCategory>();
            }
        }

        public async Task<IEnumerable<FormCategory>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _formCategoryDal.GetAllIncludeAsync(new Expression<Func<FormCategory, bool>>[]
                {

                }, null, y => y.CompanyForms);
                return result.OrderByDescending(i => i.CompanyForms.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<FormCategory>();
            }
        }

        public IEnumerable<FormCategory> GetAllIncludingFormCategoriesForSidebar()
        {
            try
            {
                return _formCategoryDal.GetAllInclude(new Expression<Func<FormCategory, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted == false,
                    i=>i.CompanyForms.Count()>0
                }, null, y => y.CompanyForms).OrderByDescending(i => i.CompanyForms.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<FormCategory>();
            }
        }

        public IEnumerable<FormCategory> GetAllSitemap()
        {
            try
            {
                return _formCategoryDal.GetAllInclude(new Expression<Func<FormCategory, bool>>[]
                {
                    i=>i.IsActive == true,
                    i=>i.IsDeleted == false
                }, null, y => y.CompanyForms).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<FormCategory>();
            }
        }

        public async Task<FormCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _formCategoryDal.GetAsync(i => i.IsActive == true && i.IsDeleted == false && i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _formCategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _formCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _formCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _formCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(FormCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _formCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }
    }
}
