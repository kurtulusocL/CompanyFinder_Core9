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
    public class AdController : Controller
    {
        readonly IAdService _adService;
        readonly IAdTargetService _adTargetService;
        readonly IAdCompanyService _adCompanyService;
        readonly IHitService _hitService;
        readonly ICountryService _countryService;
        readonly ICityService _cityService;
        public AdController(IAdService adService, IAdTargetService adTargetService, IAdCompanyService adCompanyService, IHitService hitService, ICountryService countryService, ICityService cityService)
        {
            _adService = adService;
            _adTargetService = adTargetService;
            _adCompanyService = adCompanyService;
            _hitService = hitService;
            _countryService = countryService;
            _cityService = cityService;
        }

        public IActionResult HitRead(int? adId, string userId, int currentValue)
        {
            var result = _hitService.AdHit(adId, userId, currentValue);
            return View(result);
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _adService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> TargetlessAd()
        {
            return View(await _adService.GetAllIncludingAdsNoTargetAsync());
        }
        public async Task<IActionResult> TargetfullAd()
        {
            return View(await _adService.GetAllIncludingAdsByTargetAsync());
        }
        public async Task<IActionResult> ExpiryDateAds()
        {
            return View(await _adService.GetAllIncludingByExpiryDateAsync());
        }
        public async Task<IActionResult> AdCompanyList(int id)
        {
            return View(await _adService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> AdOps()
        {
            return View(await _adService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> AdDetail(int? id)
        {
            return View(await _adService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.AdCompanies = await _adCompanyService.GetAllIncludingForAddAdAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ad model, IFormFile image)
        {
            var result = await _adService.CreateAsync(model, image);
            if (result)
            {
                TempData["adCreatedSuccessfull"] = "Ad Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["adCreateError"] = "Ad Created Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.AdCompanies = await _adCompanyService.GetAllIncludingForAddAdAsync();
            var data = await _adService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ad model, IFormFile image)
        {
            var result = await _adService.UpdateAsync(model, image);
            if (result)
            {
                TempData["adUpdatedSuccessfull"] = "Ad Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["adUpdateError"] = "Ad Updated Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        public async Task<IActionResult> CreateAdTarget(int? id)
        {
            ViewBag.AdTargetCountries = await _countryService.GetAllIncludingForAddAdTargetAsync();
            ViewBag.AdTargetCities = await _cityService.GetAllIncludingCitiesForAddAdTargetAsync();
            ViewBag.AdId = await _adService.GetByIdAsync(id);
            AdTarget model = new AdTarget
            {
                AdId = id
            };
            return View("CreateAdTarget", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdTarget(int? cityId, int? countryId, int? adId)
        {
            var result = await _adTargetService.CreateAsync(cityId, countryId, adId);
            if (result)
            {
                TempData["adTargetCreatedSuccessfull"] = "Ad Target Created";
                return RedirectToAction(nameof(CreateAdTarget), new { id = adId });
            }
            TempData["adTargetCreatedError"] = "Ad Target Created Error";
            return RedirectToAction(nameof(CreateAdTarget), new { id = adId });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _adService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAd(Ad model, int id)
        {
            var result = await _adService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AdOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _adService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _adService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _adService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _adService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
