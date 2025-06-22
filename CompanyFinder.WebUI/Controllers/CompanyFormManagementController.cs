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
    public class CompanyFormManagementController : Controller
    {
        readonly ICompanyFormService _companyFormService;
        public CompanyFormManagementController(ICompanyFormService companyFormService)
        {
            _companyFormService = companyFormService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyFormService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Answerable()
        {
            return View(await _companyFormService.GetAllIncludingAnswerableAsync());
        }
        public async Task<IActionResult> NotAnswerable()
        {
            return View(await _companyFormService.GetAllIncludingNotAnswerableAsync());
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _companyFormService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ByCategory(int id)
        {
            return View(await _companyFormService.GetAllIncludingByFormCategoryIdAsync(id));
        }
        public async Task<IActionResult> ByCompanyForAdmin(int? id)
        {
            return View(await _companyFormService.GetAllIncludingForAdminByCompanyIdAsync(id));
        }
        public async Task<IActionResult> CompanyFormOps()
        {
            return View(await _companyFormService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> CompanyFormDetail(int? id)
        {
            return View(await _companyFormService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyFormService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyForm(CompanyForm model, int id)
        {
            var result = await _companyFormService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyFormService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyFormService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyFormService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyFormService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyFormOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
