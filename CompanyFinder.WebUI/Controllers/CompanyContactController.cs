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
    public class CompanyContactController : Controller
    {
        readonly ICompanyContactService _companyContactService;
        public CompanyContactController(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyContactService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ContactHide()
        {
            return View(await _companyContactService.GetAllIncludingContactHideAsync());
        }
        public async Task<IActionResult> ByCompany(int? id)
        {
            return View(await _companyContactService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ByCompanyForAdmin(int? id)
        {
            return View(await _companyContactService.GetAllIncludingForAdminByCompanyIdAsync(id));
        }
        public async Task<IActionResult> CompanyContactOps()
        {
            return View(await _companyContactService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CompanyContactDetail(int? id)
        {
            return View(await _companyContactService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyContactService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyContact(CompanyContact model, int id)
        {
            var result = await _companyContactService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyContactService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyContactService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyContactService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyContactService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
