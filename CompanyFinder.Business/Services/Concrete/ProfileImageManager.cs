using System.Linq.Expressions;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Http;

namespace CompanyFinder.Business.Services.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        readonly IProfileImageDal _profileImageDal;
        readonly IHttpContextAccessor _httpContextAccessor;
        public ProfileImageManager(IProfileImageDal profileImageDal, IHttpContextAccessor httpContextAccessor)
        {
            _profileImageDal = profileImageDal;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string userId, IFormFile image)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("adminId");
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId), "userId was null");
                }

                var entity = new ProfileImage
                {
                    UserId = userId
                };
                if (entity != null)
                {
                    var errors = new List<string>();
                    if (image != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/us/profileImage/");
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
                            var result = await _profileImageDal.AddAsync(entity);
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
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ProfileImage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _profileImageDal.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _profileImageDal.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingAdminImageAsync()
        {
            try
            {
                var result = await _profileImageDal.GetAllIncludeAsync(new Expression<Func<ProfileImage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingAdminImageForAdminAsync()
        {
            try
            {
                var result = await _profileImageDal.GetAllIncludeAsync(new Expression<Func<ProfileImage, bool>>[]
                {

                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingAsync()
        {
            try
            {
                var result = await _profileImageDal.GetAllIncludeAsync(new Expression<Func<ProfileImage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _profileImageDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<ProfileImage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingByUserIdForAdminAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = await _profileImageDal.GetAllIncludeByIdAsync(userId, "UserId", new Expression<Func<ProfileImage, bool>>[]
                {

                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllIncludingForAdmin()
        {
            try
            {
                var result = await _profileImageDal.GetAllIncludeAsync(new Expression<Func<ProfileImage, bool>>[]
                {

                }, null, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public IEnumerable<ProfileImage> GetAllIncludinggProfileImageForAdminHomeByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var result = _profileImageDal.GetAllIncludeById(userId, "UserId", new Expression<Func<ProfileImage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.User);
                return result.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public async Task<ProfileImage> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                return await _profileImageDal.GetIncludeAsync(i => i.Id == id, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public ProfileImage GetProfileImageByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _profileImageDal.GetInclude(i => i.IsActive == true && i.IsDeleted == false && i.UserId == userId, y => y.User);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the profile image.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _profileImageDal.SetActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _profileImageDal.SetDeActiveAsync(id);
            return result;
        }
        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _profileImageDal.SetDeletedAsync(id);
            return result;
        }
        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _profileImageDal.SetNotDeletedAsync(id);
            return result;
        }
    }
}
