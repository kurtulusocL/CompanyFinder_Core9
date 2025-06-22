using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class ProfileImageController : Controller
    {
        readonly IProfileImageService _profileImageService;
        public ProfileImageController(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _profileImageService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> AdminProfileImage()
        {
            return View(await _profileImageService.GetAllIncludingAdminImageAsync());
        }        
        public async Task<IActionResult> ImageByUserId(string id)
        {
            return View(await _profileImageService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> AdminProfileImageForAdmin()
        {
            return View(await _profileImageService.GetAllIncludingAdminImageForAdminAsync());
        }
        public async Task<IActionResult> ImageByUserIdForAdmin(string id)
        {
            return View(await _profileImageService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProfileImageOps()
        {
            return View(await _profileImageService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> ProfileImageDetail(int? id)
        {
            return View(await _profileImageService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _profileImageService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteProfileImage(ProfileImage model, int id)
        {
            var result = await _profileImageService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ProfileImageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _profileImageService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProfileImageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _profileImageService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProfileImageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _profileImageService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProfileImageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _profileImageService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ProfileImageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
