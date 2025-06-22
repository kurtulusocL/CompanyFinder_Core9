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
    public class PricingContactController : Controller
    {
        readonly IPricingContactService _pricingContactService;
        public PricingContactController(IPricingContactService pricingContactService)
        {
            _pricingContactService = pricingContactService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _pricingContactService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByPricing(int? id)
        {
            return View(await _pricingContactService.GetAllIncludingByPricingId(id));
        }
        public async Task<IActionResult> PricingContactOps()
        {
            return View(await _pricingContactService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> PricingContactDetail(int? id)
        {
            return View(await _pricingContactService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _pricingContactService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeletePricingContact(PricingContact model, int id)
        {
            var result = await _pricingContactService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(PricingContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _pricingContactService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _pricingContactService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _pricingContactService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _pricingContactService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
