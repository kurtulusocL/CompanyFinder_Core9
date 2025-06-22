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
    public class UserSessionController : Controller
    {
        readonly IUserSessionService _userSessionService;
        public UserSessionController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _userSessionService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CurrentlyLogin()
        {
            return View(await _userSessionService.GetAllIncludingCurrentlyLoginAsync());
        }
        public async Task<IActionResult> LoginDateByUserId(string id)
        {
            return View(await _userSessionService.GetAllIncludingByLoginDateByUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserId(string id)
        {
            return View(await _userSessionService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserIdForAdmin(string id)
        {
            return View(await _userSessionService.GetAllIncludingUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> UserSessionOps()
        {
            return View(await _userSessionService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> UserSessionDetail(int? id)
        {
            return View(await _userSessionService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _userSessionService.GetByIdAsync(id);
            if (data == null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteUserSession(UserSession model, int id)
        {
            var result = await _userSessionService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(UserSessionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _userSessionService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserSessionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _userSessionService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserSessionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _userSessionService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserSessionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _userSessionService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserSessionOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
