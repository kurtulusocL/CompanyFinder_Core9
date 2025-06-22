using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class UserRoleController : Controller
    {
        readonly IUserRoleService _userRoleService;
        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _userRoleService.GetAllAsync());
        }
        public async Task<IActionResult> UserRoleOps()
        {
            return View(await _userRoleService.GetAllForAdmin());
        }
        public async Task<IActionResult> UserRoleDetail(string id)
        {
            return View(await _userRoleService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRole model)
        {
            var result = await _userRoleService.CreateAsync(model);
            if (result)
            {
                TempData["userRoleCreatedSuccessfull"] = "User Role Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["userRoleCreateError"] = "User Role Create Error";
            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var data = await _userRoleService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRole model)
        {
            var result = await _userRoleService.UpdateAsync(model);
            if (result)
            {
                TempData["userRoleUpdatedSuccessfull"] = "User Role Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["userRoleUpdateError"] = "User Role Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _userRoleService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteUserRole(UserRole model, string id)
        {
            var result = await _userRoleService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(UserRoleOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(string id)
        {
            var result = await _userRoleService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserRoleOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(string id)
        {
            var result = await _userRoleService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserRoleOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(string id)
        {
            var result = await _userRoleService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserRoleOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(string id)
        {
            var result = await _userRoleService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(UserRoleOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
