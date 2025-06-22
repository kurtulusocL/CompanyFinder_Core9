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
    public class SectorNewsManagementController : Controller
    {
        readonly ISectorNewsService _sectorNewsService;
        public SectorNewsManagementController(ISectorNewsService sectorNewsService)
        {
            _sectorNewsService = sectorNewsService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _sectorNewsService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> MostLiked()
        {
            return View(await _sectorNewsService.GetAllIncludingMostLikedAsync());
        }
        public async Task<IActionResult> MostHit()
        {
            return View(await _sectorNewsService.GetAllIncludingMostHitAsync());
        }
        public async Task<IActionResult> SectorNewsOps()
        {
            return View(await _sectorNewsService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> SectorNewsDetail(int? id)
        {
            return View(await _sectorNewsService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SectorNews model, IFormFile image)
        {
            var result = await _sectorNewsService.CreateAsync(model, image);
            if (result)
            {
                TempData["sectorNewsCreatedSuccessfull"] = "Sector News Created Successfully";
                return RedirectToAction(nameof(Create));
            }
            TempData["sectorNewsCreateError"] = "There was an error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _sectorNewsService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SectorNews model, IFormFile image)
        {
            var result = await _sectorNewsService.UpdateAsync(model, image);
            if (result)
            {
                TempData["sectorNewsUpdatedSuccessfull"] = "Sector News Updated Successfully";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["sectorNewsUpdateError"] = "There was an error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _sectorNewsService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteSectorNews(SectorNews model, int id)
        {
            var result = await _sectorNewsService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(SectorNewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _sectorNewsService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SectorNewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _sectorNewsService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SectorNewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _sectorNewsService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SectorNewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _sectorNewsService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SectorNewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
