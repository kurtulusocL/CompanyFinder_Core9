using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class CompanyFormManager : ICompanyFormService
    {
        readonly ICompanyFormDal _companyFormDal;
        public CompanyFormManager(ICompanyFormDal companyFormDal)
        {
            _companyFormDal = companyFormDal;
        }
        public async Task<bool> CreateAsync(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/formImage/");
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

                        var entity = new CompanyForm
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Desc = desc,
                            IsAnswerable = isAnswerable,
                            FormCategoryId = formCategoryId,
                            CompanyId = companyId
                        };
                        entity.ImageUrl = fileName;
                        var result = await _companyFormDal.AddAsync(entity);
                        if (!result)
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

        public async Task<bool> DeleteAsync(CompanyForm entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _companyFormDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyFormDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingCompanyUserCompanyFormByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyFormDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers, y => y.FormCategory);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingAnswerableAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAnswerable==true
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyFormDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingByFormCategoryIdAsync(int formCategoryId)
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeByIdAsync(formCategoryId, "FormCategoryId", new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {

                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingForAdminByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var result = await _companyFormDal.GetAllIncludeByIdAsync(companyId, "CompanyId", new Expression<Func<CompanyForm, bool>>[]
                {

                }, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingNotAnswerableAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsAnswerable==false
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public IEnumerable<CompanyForm> GetAllSitemap()
        {
            try
            {
                return _companyFormDal.GetAllInclude(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, y => y.FormCategory).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<CompanyForm> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyFormDal.GetIncludeAsync(i => i.Id == id, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Reports, y => y.Likes, y => y.CompanyFormAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyFormDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetAnswerable(int id)
        {
            var result = await _companyFormDal.SetAnswerable(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyFormDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyFormDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotAnswerable(int id)
        {
            var result = await _companyFormDal.SetNotAnswerable(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyFormDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/company/formImage/");
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

                        var entity = new CompanyForm
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Desc = desc,
                            IsAnswerable = isAnswerable,
                            FormCategoryId = formCategoryId,
                            CompanyId = companyId,
                            Id = id
                        };
                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        var result = await _companyFormDal.UpdateAsync(entity);
                        if (!result)
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
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public IEnumerable<CompanyForm> GetAllIncludingRandom()
        {
            try
            {
                return _companyFormDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => Guid.NewGuid()).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingSearchResultAsync(string key)
        {
            try
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key), "key was null");

                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Title.Contains(key)||i.Desc.Contains(key))
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingMostLikedCompanyFormAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingMostHitCompanyFormAsync()
        {
            try
            {
                var result = await _companyFormDal.GetAllIncludeAsync(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingYourMostLikedCompanyFormByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyFormDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0,
                    i=>i.Company.UserId==userId
                }, y => y.FormCategory, y=>y.Company, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.Likes.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public async Task<IEnumerable<CompanyForm>> GetAllIncludingYourMostHitCompanyFormByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _companyFormDal.GetAllIncludingByPropertyPathAsync(userId, "Company.UserId", new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0,
                     i=>i.Company.UserId==userId
                }, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Likes, y => y.CompanyFormAnswers);
                return result.OrderByDescending(i => i.Hits.Count()).Take(350).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }

        public IEnumerable<CompanyForm> GetAllIncludingCompanyFormRandomForExplorerHome()
        {
            try
            {
                var result = _companyFormDal.GetAllInclude(new Expression<Func<CompanyForm, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.FormCategory, y => y.Company, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => Guid.NewGuid()).Take(12).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyForm>();
            }
        }
    }
}
