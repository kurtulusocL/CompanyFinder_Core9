using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AboutManager : IAboutService
    {
        readonly IAboutDal _aboutDal;
        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public async Task<bool> CreateAsync(About entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/about/");
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
                        entity.ImageUrl = fileName;
                        var result = await _aboutDal.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(About entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _aboutDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _aboutDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IEnumerable<About> GetAllAboutForFooter()
        {
            try
            {
                return _aboutDal.GetAll(i => i.IsDeleted == false && i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(1).ToList();
            }
            catch (Exception)
            {
                return new List<About>();
            }
        }

        public IEnumerable<About> GetAllAboutForHome()
        {
            try
            {
                return _aboutDal.GetAll(i => i.IsDeleted == false && i.IsActive == true).OrderByDescending(i => i.CreatedDate).Take(1).ToList();
            }
            catch (Exception)
            {
                return new List<About>();
            }
        }

        public async Task<IEnumerable<About>> GetAllAsync()
        {
            try
            {
                var result = await _aboutDal.GetAllAsync(i => i.IsDeleted == false && i.IsActive == true);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<About>();
            }
        }

        public async Task<IEnumerable<About>> GetAllForAdmin()
        {
            try
            {
                var result = await _aboutDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<About>();
            }
        }

        public IEnumerable<About> GetAllSiteMap()
        {
            try
            {
                return _aboutDal.GetAll().OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<About>();
            }
        }

        public async Task<About> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id is null");

                return await _aboutDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public About HitRead(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "id was null");
            var result = _aboutDal.HitRead(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _aboutDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _aboutDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _aboutDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _aboutDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(About entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/about/");
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
                        entity.ImageUrl = fileName;
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        var result = await _aboutDal.UpdateAsync(entity);
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
    }
}
