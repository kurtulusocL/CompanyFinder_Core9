using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyCategoryManager : ICompanyCategoryService
    {
        readonly ICompanyCategoryDal _companyCategoryDal;
        public CompanyCategoryManager(ICompanyCategoryDal companyCategoryDal)
        {
            _companyCategoryDal = companyCategoryDal;
        }

        public async Task<bool> CreateAsync(CompanyCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _companyCategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyCategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyCategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IEnumerable<CompanyCategory> GetAllCompanyCategoriesForAdminHome()
        {
            try
            {
                return _companyCategoryDal.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    x=>x.IsDeleted==false,
                    i => i.Companies.Count() > 0
                }, null, y => y.Companies, y => y.CompanySubcategories).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public async Task<IEnumerable<CompanyCategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyCategoryDal.GetAllIncludeAsync(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    x=>x.IsDeleted==false
                }, null, x => x.Companies, x => x.CompanySubcategories);
                return result.OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public IEnumerable<CompanyCategory> GetAllIncludingCompanyCategoriesForCompany()
        {
            try
            {
                return _companyCategoryDal.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, x => x.Companies).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public IEnumerable<CompanyCategory> GetAllIncludingCompanyCategoriesForCompanyAdvancedSearch()
        {
            try
            {
                return _companyCategoryDal.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    x=>x.IsDeleted==false,
                    i => i.Companies.Count() > 0
                }, null).OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public async Task<IEnumerable<CompanyCategory>> GetAllIncludingForAddCompanyAsync()
        {
            try
            {
                var result = await _companyCategoryDal.GetAllIncludeAsync(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, x => x.Companies, x => x.CompanySubcategories);
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public async Task<IEnumerable<CompanyCategory>> GetAllIncludingForAddSubcategoryAsync()
        {
            try
            {
                var result = await _companyCategoryDal.GetAllIncludeAsync(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    x=>x.IsDeleted==false
                }, null, x => x.Companies, x => x.CompanySubcategories);
                return result.OrderByDescending(i => i.CompanySubcategories.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public async Task<IEnumerable<CompanyCategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _companyCategoryDal.GetAllIncludeAsync(new Expression<Func<CompanyCategory, bool>>[]
                {

                }, null, x => x.Companies, x => x.CompanySubcategories);
                return result.OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public IEnumerable<CompanyCategory> GetAllSiteMap()
        {
            try
            {
                var result = _companyCategoryDal.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    x=>x.IsDeleted==false
                }, null);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public async Task<CompanyCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companyCategoryDal.GetIncludeAsync(i => i.Id == id, x => x.Companies, y => y.CompanySubcategories);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyCategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyCategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyCategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyCategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CompanyCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _companyCategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
