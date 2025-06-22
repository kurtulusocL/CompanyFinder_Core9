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
    public class SavedContentController : Controller
    {
        readonly ISavedContentService _savedContentService;
        public SavedContentController(ISavedContentService savedContentService)
        {
            _savedContentService = savedContentService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _savedContentService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> NotSavedContent()
        {
            return View(await _savedContentService.GetAllIncludingByNotSavedAsync());
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByCompanyIdAsync(id));
        }
        public async Task<IActionResult>ByPartnership(int? id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByCompanyPartnershipIdAsync(id));
        }
        public async Task<IActionResult> ByProduct(int? id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByProductIdAsync(id));
        }
        public async Task<IActionResult> ByBlog(int? id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByBlogIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _savedContentService.GetAllIncludingSavedByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> SavedContentOps()
        {
            return View(await _savedContentService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> SavedContentDetail(int? id)
        {
            return View(await _savedContentService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _savedContentService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteSavedContent(SavedContent model, int id)
        {
            var result = await _savedContentService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(SavedContentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _savedContentService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SavedContentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _savedContentService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SavedContentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _savedContentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SavedContentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _savedContentService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SavedContentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
