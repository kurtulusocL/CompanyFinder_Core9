using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]   
    public class HomeAdminController : Controller
    {
        readonly IUserService _userService;
        readonly IProfileImageService _profileImageService;
        public HomeAdminController(IUserService userService, IProfileImageService profileImageService)
        {
            _userService = userService;
            _profileImageService = profileImageService;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admins") || User.IsInRole("SecondAdmins") || User.IsInRole("HelperAdmins") || User.IsInRole("WorkerAdmins"))
            {
                return View();
            }
            return RedirectToAction("Login", "AccountAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public IActionResult Page404()
        {
            return View();
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> MyProfile(string id)
        {
            return View(await _userService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> CreateProfileImage(string id)
        {
            ViewBag.UserId = await _userService.GetByIdAsync(id);
            ProfileImage model = new ProfileImage
            {
                UserId = id
            };
            return View("CreateProfileImage", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> CreateProfileImage(string userId, IFormFile image)
        {
            var result = await _profileImageService.CreateAsync(userId, image);
            if (result)
            {
                TempData["profileImageCreatedSuccessfull"] = "Profile Image Created Successfull";
                return RedirectToAction(nameof(CreateProfileImage), new { id = userId });
            }
            TempData["profileImageCreateError"] = "Profile Image Create Error";
            return RedirectToAction(nameof(CreateProfileImage), new { id = userId });
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> DeleteProfileImage(ProfileImage model, int id)
        {
            var result = await _profileImageService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(MyProfile), new { id = model.UserId });
            }
            return RedirectToAction(nameof(MyProfile), new { id = model.UserId });
        }
    }
}
