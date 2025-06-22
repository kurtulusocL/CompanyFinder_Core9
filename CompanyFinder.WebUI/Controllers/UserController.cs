using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class UserController : Controller
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _userService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Admin()
        {
            return View(await _userService.GetAllIncludingAdminUserAsync());
        }
        public async Task<IActionResult> SuspendedAdmin()
        {
            return View(await _userService.GetAllIncludingSuspendedAdminAsync());
        }
        public async Task<IActionResult> Company()
        {
            return View(await _userService.GetAllIncludingCompanyUserAsync());
        }
        public async Task<IActionResult> SuspendedCompany()
        {
            return View(await _userService.GetAllIncludingSuspendedCompanyAsync());
        }
        public async Task<IActionResult> DeletedAdmin()
        {
            return View(await _userService.GetAllIncludingDeletedAdminAsync());
        }
        public async Task<IActionResult> DeletedCompany()
        {
            return View(await _userService.GetAllIncludingDeletedCompanyAsync());
        }
        public async Task<IActionResult> UserOps()
        {
            return View(await _userService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> UserDetail(string id)
        {
            return View(await _userService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _userService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteUser(User model, string id)
        {
            var result = await _userService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(UserOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(string id)
        {
            var result = await _userService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(string id)
        {
            var result = await _userService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(string id)
        {
            var result = await _userService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(string id)
        {
            var result = await _userService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
