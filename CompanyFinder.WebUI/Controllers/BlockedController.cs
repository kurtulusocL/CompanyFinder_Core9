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
    public class BlockedController : Controller
    {
        readonly IBlockedService _blockedService;
        public BlockedController(IBlockedService blockedService)
        {
            _blockedService = blockedService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _blockedService.GetAllAsync());
        }
        public async Task<IActionResult> BlockedOps()
        {
            return View(await _blockedService.GetAllForAdminAsync());
        }
        public async Task<IActionResult> BlockedDetail(int? id)
        {
            return View(await _blockedService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blocked model)
        {
            var result = await _blockedService.CreateAsync(model);
            if (result)
            {
                TempData["blockedUserCreatedSuccessfull"] = "User Add to Blocked List Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["blockedUserCreateError"] = "There was an error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _blockedService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Blocked model)
        {
            var result = await _blockedService.UpdateAsync(model);
            if (result)
            {
                TempData["blockedUserUpdatedSuccessfull"] = "Blcked User Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["blockedUserUpdateError"] = "There was an error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _blockedService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteBlocked(Blocked model, int id)
        {
            var result = await _blockedService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(BlockedOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _blockedService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlockedOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _blockedService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlockedOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _blockedService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlockedOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _blockedService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlockedOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
