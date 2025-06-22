using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class SectorNewsManager : ISectorNewsService
    {
        readonly ISectorNewsDal _sectorNewsDal;
        public SectorNewsManager(ISectorNewsDal sectorNewsDal)
        {
            _sectorNewsDal = sectorNewsDal;
        }

        public async Task<bool> CreateAsync(SectorNews entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/sectorNews/");
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
                        var result = await _sectorNewsDal.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(SectorNews entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _sectorNewsDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _sectorNewsDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SectorNews>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _sectorNewsDal.GetAllIncludeAsync(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public async Task<IEnumerable<SectorNews>> GetAllIncludingForAdminAsync()
        {
            try
            {
                var result = await _sectorNewsDal.GetAllIncludeAsync(new Expression<Func<SectorNews, bool>>[]
                {

                }, null, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public async Task<IEnumerable<SectorNews>> GetAllIncludingMostHitAsync()
        {
            try
            {
                var result = await _sectorNewsDal.GetAllIncludeAsync(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.Hits.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public async Task<IEnumerable<SectorNews>> GetAllIncludingMostLikedAsync()
        {
            try
            {
                var result = await _sectorNewsDal.GetAllIncludeAsync(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.Hits, y => y.Likes);
                return result.OrderByDescending(i => i.Likes.Count()).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public IEnumerable<SectorNews> GetAllIncludingRandomSectorNews()
        {
            try
            {
                return _sectorNewsDal.GetAllInclude(new Expression<Func<SectorNews, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()<60,
                    i=>i.Likes.Count()<60
                }, null, y => y.Hits, y => y.Likes).OrderByDescending(i => Guid.NewGuid()).Take(9).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public IEnumerable<SectorNews> GetAllSiteMap()
        {
            try
            {
                return _sectorNewsDal.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SectorNews>();
            }
        }

        public async Task<SectorNews> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _sectorNewsDal.GetIncludeAsync(i => i.Id == id, y => y.Hits, y => y.Likes);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _sectorNewsDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _sectorNewsDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _sectorNewsDal.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _sectorNewsDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(SectorNews entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/sectorNews/");
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
                        var result = await _sectorNewsDal.UpdateAsync(entity);
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
