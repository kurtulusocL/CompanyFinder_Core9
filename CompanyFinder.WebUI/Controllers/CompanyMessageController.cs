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
    public class CompanyMessageController : Controller
    {
        readonly ICompanyMessageService _companyMessageService;
        public CompanyMessageController(ICompanyMessageService companyMessageService)
        {
            _companyMessageService = companyMessageService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyMessageService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _companyMessageService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _companyMessageService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> CompanyMessageOps()
        {
            return View(await _companyMessageService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CompanyMessageDetail(int? id)
        {
            return View(await _companyMessageService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _companyMessageService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? title, string subject, string desc, int? companyId, string userId, int id)
        {
            var result = await _companyMessageService.UpdateAsync(title, subject, desc, companyId, userId, id);
            if (result)
            {
                TempData["successCompanyMessageEdit"] = "Company Message Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            TempData["errorCompanyMessageEdit"] = "Company Message Update Error";
            return RedirectToAction(nameof(Edit), new { id = id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyMessageService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyMessage(CompanyMessage model, int id)
        {
            var result = await _companyMessageService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyMessageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyMessageService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyMessageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyMessageService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyMessageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyMessageService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyMessageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyMessageService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyMessageOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
