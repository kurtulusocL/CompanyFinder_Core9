using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    public class CompanyPartnershipManagementController : Controller
    {
        readonly ICompanyPartnershipService _companyPartnershipService;
        public CompanyPartnershipManagementController(ICompanyPartnershipService companyPartnershipService)
        {
            _companyPartnershipService = companyPartnershipService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _companyPartnershipService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> StartDate()
        {
            return View(await _companyPartnershipService.GetAllIncludingByStartDateAsync());
        }
        public async Task<IActionResult> ExpiryDate()
        {
            return View(await _companyPartnershipService.GetAllIncludingByExpiryDateAsync());
        }
        public async Task<IActionResult> Available()
        {
            return View(await _companyPartnershipService.GetAllIncludingAvailableAsync());
        }
        public async Task<IActionResult> NotAvailable()
        {
            return View(await _companyPartnershipService.GetAllIncludingNotAvailableAsync());
        }
        public async Task<IActionResult> Company(int? id)
        {
            return View(await _companyPartnershipService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> Category(int id)
        {
            return View(await _companyPartnershipService.GetAllIncludingByProductCategoryIdAsync(id));
        }
        public async Task<IActionResult> ByUser(string id)
        {
            return View(await _companyPartnershipService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> ByUserForAdmin(string id)
        {
            return View(await _companyPartnershipService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> CompanyPartnershipOps()
        {
            return View(await _companyPartnershipService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> CompanyPartnershipDetail(int? id)
        {
            return View(await _companyPartnershipService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _companyPartnershipService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCompanyPartnership(CompanyPartnership model, int id)
        {
            var result = await _companyPartnershipService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyPartnershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _companyPartnershipService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyPartnershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _companyPartnershipService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyPartnershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _companyPartnershipService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyPartnershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _companyPartnershipService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CompanyPartnershipOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
