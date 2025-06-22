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
    public class AdCompanyController : Controller
    {
        readonly IAdCompanyService _adCompanyService;
        public AdCompanyController(IAdCompanyService adCompanyService)
        {
            _adCompanyService = adCompanyService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _adCompanyService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> AdCompanyOps()
        {
            return View(await _adCompanyService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> AdCompanyDetail(int? id)
        {
            return View(await _adCompanyService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdCompany model)
        {
            var result = await _adCompanyService.CreateAsync(model);
            if (result)
            {
                TempData["adCompanyCreatedSuccessfull"] = "Ad Company Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["adCompanyCreatedError"] = "Ad Company Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _adCompanyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdCompany model)
        {
            var result = await _adCompanyService.UpdateAsync(model);
            if (result)
            {
                TempData["adCompanyUpdatedSuccessfull"] = "Ad Company Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["adCompanyUpdateError"] = "Ad Company Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _adCompanyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> AdCompanyDelete(AdCompany model, int id)
        {
            var result = await _adCompanyService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AdCompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _adCompanyService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdCompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _adCompanyService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdCompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _adCompanyService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdCompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _adCompanyService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AdCompanyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
