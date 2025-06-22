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
    public class LogoController : Controller
    {
        readonly ILogoService _logoService;
        public LogoController(ILogoService logoService)
        {
            _logoService = logoService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _logoService.GetAllAsync());
        }
        public async Task<IActionResult> LogoOps()
        {
            return View(await _logoService.GetAllForAdmin());
        }
        public async Task<IActionResult> LogoDetail(int? id)
        {
            return View(await _logoService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Logo model, IFormFile image)
        {
            var result = await _logoService.CreateAsync(model, image);
            if (result)
            {
                TempData["logoCreatedSuccessfull"] = "Logo Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["logoCreateError"] = "Logo Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _logoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Logo model, IFormFile image)
        {
            var result = await _logoService.UpdateAsync(model, image);
            if (result)
            {
                TempData["logoUpdatedSuccessfull"] = "Logo Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["logoUpdateError"] = "Logo Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _logoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteLogo(Logo model, int id)
        {
            var result = await _logoService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(LogoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _logoService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LogoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _logoService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LogoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _logoService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LogoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _logoService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LogoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
