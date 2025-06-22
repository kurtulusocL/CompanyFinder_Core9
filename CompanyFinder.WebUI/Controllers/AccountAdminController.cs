using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Core.Dtos.AdminAuthDtos;
using CompanyFinder.Core.Dtos.UserAuthDtos;
using CompanyFinder.Core.ViewModels.RoleVM;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    public class AccountAdminController : Controller
    {
        readonly IAuthAdminService _authAdminService;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public AccountAdminController(IAuthAdminService authAdminService, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _authAdminService = authAdminService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public IActionResult Login(string returnUrl)
        {
            return View(new AdminLoginDto()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new ArgumentNullException(nameof(user), "user was null");

            if (user.IsLoginConfirmCode == true)
            {
                var result = await _authAdminService.LoginWithConfirmCodeAsync(model);
                if (result)
                {
                    return RedirectToAction(nameof(LoginConfirmMail));
                }
                return RedirectToAction(nameof(LoginConfirmMail));
            }
            else
            {
                var result = await _authAdminService.LoginAsync(model);
                if (result)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });
                }
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult LoginConfirmMail()
        {
            ViewData["loginEmail"] = _httpContextAccessor.HttpContext.Session.GetString("adminLoginEmail");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginConfirmMail(AdminLoginConfirmCodeDto model, string value)
        {
            var result = await _authAdminService.LoginConfirmMailAsync(model, value);
            if (result)
            {
                return RedirectToAction("Index", "HomeAdmin", new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });
            }
            return RedirectToAction(nameof(Login));
        }

        [Authorize(Roles = "Admins")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var result = await _authAdminService.RegisterAsync(model);
            if (result)
            {
                TempData["adminCreatedSuccessfull"] = "Admin Created Successfull";
                return RedirectToAction(nameof(Register));
            }
            TempData["adminCreateError"] = "Admin Create Error";
            return RedirectToAction(nameof(Register));
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public IActionResult RegisterCompany()
        {
            return View();
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompany(UserRegisterDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var result = await _authAdminService.RegisterCompanyAsync(model);
            if (result)
            {
                TempData["companyRegisteredSuccessfull"] = "Company Registered Successfull";
                return RedirectToAction(nameof(RegisterCompany));
            }
            TempData["companyRegisterError"] = "Company Register Error";
            return RedirectToAction(nameof(RegisterCompany));
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> ChangePassword()
        {
            var result = await _authAdminService.GetChangePasswordAsync();
            return View(result);
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(AdminChangePasswordDto model)
        {
            var result = await _authAdminService.ChangePasswordAsync(model);
            if (result)
            {
                TempData["changePasswordSuccessfull"] = "Admin Password Changed";
                return RedirectToAction(nameof(ChangePassword), new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });
            }
            TempData["changePasswordError"] = "Admin Password Change Error";
            return RedirectToAction(nameof(ChangePassword), new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> UpdateProfile()
        {
            AdminUpdateProfileDto model = new AdminUpdateProfileDto();
            var result = await _authAdminService.GetUpdateProfileAsync(model);
            if (result != null)
            {
                return View(result);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(AdminUpdateProfileDto model)
        {
            var result = await _authAdminService.UpdateProfileAsync(model);
            if (result)
            {
                TempData["profileUpdatedSuccessfull"] = "Profile Updated Successfull";
                return RedirectToAction(nameof(UpdateProfile), new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });
            }
            TempData["profileUpdateError"] = "Profile Update Error";
            return RedirectToAction(nameof(UpdateProfile), new { id = _httpContextAccessor.HttpContext.Session.GetString("adminId") });

        }

        [Authorize(Roles = "Admins")]
        [HttpGet]
        public async Task<IActionResult> RoleAssign(string id)
        {
            if (id == null)
            {
                return View();
            }
            var result = await _authAdminService.GetRoleAssingAsync(id);
            return View(result);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleAssign(List<RoleAssignVM> modelList, string id)
        {
            var result = await _authAdminService.RoleAssignAsync(modelList, id);
            if (result)
            {
                TempData["roleSetSuccessfull"] = "Admin Role Set Successfull";
                return RedirectToAction(nameof(RoleAssign), new { id = id });
            }
            TempData["roleSetError"] = "Admin Role Set Error";
            return RedirectToAction(nameof(RoleAssign), new { id = id });
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            var result = await _authAdminService.LogoutAsync();
            if (result)
                return RedirectToAction(nameof(Login));
            return View();
        }
    }
}
