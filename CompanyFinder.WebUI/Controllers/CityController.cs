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
    public class CityController : Controller
    {
        readonly ICityService _cityService;
        readonly ICountryService _countryService;
        public CityController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _cityService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Country(int? id)
        {
            return View(await _cityService.GetAllIncludingByCountryIdAsync(id));
        }
        public async Task<IActionResult> CityOps()
        {
            return View(await _cityService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CityDetail(int? id)
        {
            return View(await _cityService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Countries = await _countryService.GetAllIncludingForAddCityAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City model)
        {
            var result = await _cityService.CreateAsync(model);
            if (result)
            {
                TempData["cityCreate"] = "City Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["cityCreateError"] = "City Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Countries = await _countryService.GetAllIncludingForAddCityAsync();
            var data = await _cityService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(City model)
        {
            var result = await _cityService.UpdateAsync(model);
            if (result)
            {
                TempData["cityUpdate"] = "City Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["cityUpdateError"] = "City Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _cityService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCity(City model, int id)
        {
            var result = await _cityService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CityOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _cityService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CityOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _cityService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CityOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _cityService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CityOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _cityService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CityOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
