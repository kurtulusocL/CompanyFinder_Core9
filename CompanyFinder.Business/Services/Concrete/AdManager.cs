using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class AdManager : IAdService
    {
        readonly IAdDal _adDal;
        public AdManager(IAdDal adDal)
        {
            _adDal = adDal;
        }

        public int AdCounter()
        {
            var result = _adDal.AdCounter();
            return result;
        }

        public async Task<bool> CreateAsync(Ad entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/ad/");
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
                        var results = await _adDal.AddAsync(entity);
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
                var result = await _adDal.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Ad entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _adDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _adDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingAdsByTargetAsync()
        {
            try
            {
                var result = await _adDal.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false,
                    y=>y.AdTargets.Count()>0
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingAdsNoTargetAsync()
        {
            try
            {
                var result = await _adDal.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false,
                    y=>y.AdTargets.Count()==0
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _adDal.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingByCompanyIdAsync(int companyId)
        {
            try
            {
                var result = await _adDal.GetAllIncludeByIdAsync(companyId, "AdCompanyId", new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingByExpiryDateAsync()
        {
            try
            {
                var result = await _adDal.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    y=>y.IsDeleted==false
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.ExpiryDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<IEnumerable<Ad>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _adDal.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {

                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public IEnumerable<Ad> GetAllIncludingRandomAd()
        {
            try
            {
                var result = _adDal.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ExpiryDate>DateTime.Now
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderBy(i => Guid.NewGuid()).Take(7).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public IEnumerable<Ad> GetAllIncludingRandomAdForHome()
        {
            try
            {
                var result = _adDal.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.ExpiryDate>DateTime.Now
                }, null, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
                return result.OrderByDescending(i => Guid.NewGuid()).Take(8).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public async Task<Ad> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _adDal.GetIncludeAsync(i => i.Id == id, y => y.AdTargets, y => y.Hits, y => y.AdCompany);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _adDal.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _adDal.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _adDal.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _adDal.SetNotDeletedAsync(id);
            return result;
        }

        public Ad SimilarHitRead(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id was null");

            var result = _adDal.SimilarHitRead(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Ad entity, IFormFile image)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity was null");

                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/ad/");
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
                        var results = await _adDal.UpdateAsync(entity);
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
                entity.UpdatedDate = DateTime.Now.ToLocalTime();
                var result = await _adDal.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
