using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanySubcategoryManager : ICompanySubcategoryService
    {
        readonly ICompanySubcategoryDal _companySubcategoryDal;
        public CompanySubcategoryManager(ICompanySubcategoryDal companySubcategoryDal)
        {
            _companySubcategoryDal = companySubcategoryDal;
        }

        public async Task<bool> CreateAsync(CompanySubcategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var result = await _companySubcategoryDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanySubcategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companySubcategoryDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companySubcategoryDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IEnumerable<CompanySubcategory> GetAllCompanySubcategoryForAdminHome()
        {
            try
            {
                return _companySubcategoryDal.GetAllInclude(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies, y => y.CompanyCategory).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<IEnumerable<CompanySubcategory>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companySubcategoryDal.GetAllIncludeAsync(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.CompanyCategory);

                return result.OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<IEnumerable<CompanySubcategory>> GetAllIncludingByCategoryIdAsync(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                    throw new ArgumentNullException(nameof(categoryId), "CategoryId was null");

                var result = await _companySubcategoryDal.GetAllIncludeByIdAsync(categoryId, "CompanyCategoryId",
                     new Expression<Func<CompanySubcategory, bool>>[]
                     {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                     },
                     i => i.Companies,
                     i => i.CompanyCategory
                 );
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<IEnumerable<CompanySubcategory>> GetAllIncludingCompanySubcategoriesForAdd()
        {
            try
            {
                var result = await _companySubcategoryDal.GetAllIncludeAsync(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.CompanyCategory);

                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public IEnumerable<CompanySubcategory> GetAllIncludingCompanySubcategoriesForCompany()
        {
            try
            {
                return _companySubcategoryDal.GetAllInclude(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderByDescending(i => i.Companies.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public IEnumerable<CompanySubcategory> GetAllIncludingCompanySubcategoriesForCompanyAdvancedSearch()
        {
            try
            {
                return _companySubcategoryDal.GetAllInclude(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null).OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<IEnumerable<CompanySubcategory>> GetAllIncludingForAddCompanyByCategoryIdAsync(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                    throw new ArgumentNullException(nameof(categoryId), "CategoryId was null");

                var result = await _companySubcategoryDal.GetAllIncludeByIdAsync(categoryId, "CompanyCategoryId",
                     new Expression<Func<CompanySubcategory, bool>>[]
                    {
                        i => i.IsActive == true,
                        y=> y.IsDeleted == false
                    },
                    i => i.CompanyCategory,
                    i => i.Companies
                );
                return result.OrderBy(i => i.Name).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<IEnumerable<CompanySubcategory>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _companySubcategoryDal.GetAllIncludeAsync(new Expression<Func<CompanySubcategory, bool>>[]
                {

                }, null, y => y.Companies, y => y.CompanyCategory);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public IEnumerable<CompanySubcategory> GetAllSiteMap()
        {
            try
            {
                var result = _companySubcategoryDal.GetAllInclude(new Expression<Func<CompanySubcategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory);

                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanySubcategory>();
            }
        }

        public async Task<CompanySubcategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _companySubcategoryDal.GetIncludeAsync(i => i.Id == id, y => y.Companies, y => y.CompanyCategory);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companySubcategoryDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companySubcategoryDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companySubcategoryDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companySubcategoryDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CompanySubcategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _companySubcategoryDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
