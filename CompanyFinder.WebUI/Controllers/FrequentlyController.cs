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
    public class FrequentlyController : Controller
    {
        readonly IFrequentlyService _frequentlyService;
        public FrequentlyController(IFrequentlyService frequentlyService)
        {
            _frequentlyService = frequentlyService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _frequentlyService.GetAllAsync());
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _frequentlyService.GetAllAsync());
        }
        public async Task<IActionResult> FrequentlyOps()
        {
            return View(await _frequentlyService.GetAllForAdmin());
        }
        public async Task<IActionResult> FrequentlyDetail(int? id)
        {
            return View(await _frequentlyService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Frequently model)
        {
            var result = await _frequentlyService.CreateAsync(model);
            if (result)
            {
                TempData["frequentlyCreatedSuccessfull"] = "Frequently Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["frequentlyCreateError"] = "Frequently Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _frequentlyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Frequently model)
        {
            var result = await _frequentlyService.UpdateAsync(model);
            if (result)
            {
                TempData["frequentlyUpdatedSuccessfull"] = "Frequently Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["frequentlyUpdateError"] = "Frequently Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _frequentlyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteFrequently(Frequently model, int id)
        {
            var result = await _frequentlyService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(FrequentlyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _frequentlyService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FrequentlyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _frequentlyService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FrequentlyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _frequentlyService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FrequentlyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _frequentlyService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FrequentlyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
