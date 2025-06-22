using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class OurServicesManager : IOurServicesService
    {
        readonly IOurServicesDal _ourServicesDal;
        public OurServicesManager(IOurServicesDal ourServicesDal)
        {
            _ourServicesDal = ourServicesDal;
        }

        public async Task<bool> CreateAsync(OurServices entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/ourService/");
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
                        entity.IconUrl = fileName;
                        var result = await _ourServicesDal.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(OurServices entity, int id)
        {

            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _ourServicesDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _ourServicesDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<OurServices>> GetAllAsync()
        {
            try
            {
                var result = await _ourServicesDal.GetAllAsync(i => i.IsActive == true && i.IsDeleted == false);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public async Task<IEnumerable<OurServices>> GetAllForAdmin()
        {
            try
            {
                var result = await _ourServicesDal.GetAllAsync();
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public IEnumerable<OurServices> GetAllOurServicesForHome()
        {
            try
            {
                return _ourServicesDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public IEnumerable<OurServices> GetAllOurServicesRandomForAbout()
        {
            try
            {
                return _ourServicesDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => Guid.NewGuid()).Take(4).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public IEnumerable<OurServices> GetAllOurServicesRandomForHome()
        {
            try
            {
                return _ourServicesDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => Guid.NewGuid()).Take(4).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public IEnumerable<OurServices> GetAllSiteMap()
        {
            try
            {
               return _ourServicesDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<OurServices>();
            }
        }

        public async Task<OurServices> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _ourServicesDal.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _ourServicesDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _ourServicesDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _ourServicesDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _ourServicesDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(OurServices entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/ourService/");
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
                        entity.UpdatedDate = DateTime.Now.ToLocalTime();
                        entity.IconUrl = fileName;
                        var result = await _ourServicesDal.AddAsync(entity);
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
    }
}
