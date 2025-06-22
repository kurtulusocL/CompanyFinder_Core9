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
    public class AdTargetController : Controller
    {
        readonly IAdTargetService _adTargetService;
        readonly ICountryService _countryService;
        readonly ICityService _cityService;
        public AdTargetController(IAdTargetService adTargetService, ICountryService countryService, ICityService cityService)
        {
            _adTargetService = adTargetService;
            _countryService = countryService;
            _cityService = cityService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _adTargetService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> TargetCity(int? id)
        {
            return View(await _adTargetService.GetAllIncludingByCityIdAsync(id));
        }
        public async Task<IActionResult> TargetCountry(int? id)
        {
            return View(await _adTargetService.GetAllIncludingByCountryIdAsync(id));
        }       
        public async Task<IActionResult> TargetAd(int? id)
        {
            return View(await _adTargetService.GetAllIncludingByAdIdAsync(id));
        }
        public async Task<IActionResult> TargetOps()
        {
            return View(await _adTargetService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> TargetDetail(int? id)
        {
            return View(await _adTargetService.GetByIdAsync(id));
        }
        public async Task<IActionResult>Edit(int? id)
        {
            ViewBag.AdTargetCountries = await _countryService.GetAllIncludingForAddAdTargetAsync();
            ViewBag.AdTargetCities = await _cityService.GetAllIncludingCitiesForAddAdTargetAsync();
            var data = await _adTargetService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int? cityId, int? countryId, int? adId, int id)
        {
            var result = await _adTargetService.UpdateAsync(cityId, countryId, adId, id);
            if (result)
            {
                TempData["adTargetUpdatedSuccessfull"] = "Ad Target Upated";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            TempData["adTargetUpdatedError"] = "Ad Target Upate Error";
            return RedirectToAction(nameof(Edit), new { id = id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _adTargetService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteTarget(AdTarget model, int id)
        {
            var result = await _adTargetService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(TargetOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _adTargetService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TargetOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _adTargetService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TargetOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _adTargetService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TargetOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _adTargetService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TargetOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
