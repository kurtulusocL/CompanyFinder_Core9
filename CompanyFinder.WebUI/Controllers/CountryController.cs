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
    public class CountryController : Controller
    {
        readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _countryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> CountryOps()
        {
            return View(await _countryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CountryDetail(int? id)
        {
            return View(await _countryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country model)
        {
            var result = await _countryService.CreateAsync(model);
            if (result)
            {
                TempData["countryCreatedSuccessfull"] = "Country Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["countryCreatedError"] = "Country Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _countryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country model)
        {
            var result = await _countryService.UpdateAsync(model);
            if (result)
            {
                TempData["countryUpdatedSuccessfull"] = "Country Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["countryUpdateError"] = "Country Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _countryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteCountry(Country model, int id)
        {
            var result = await _countryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CountryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _countryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CountryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _countryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CountryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _countryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CountryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _countryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CountryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
