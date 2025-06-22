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
    public class LayoutInfoController : Controller
    {
        readonly ILayoutInfoService _layoutInfoService;
        public LayoutInfoController(ILayoutInfoService layoutInfoService)
        {
            _layoutInfoService = layoutInfoService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _layoutInfoService.GetAllAsync());
        }
        public async Task<IActionResult> LayoutInfoOps()
        {
            return View(await _layoutInfoService.GetAllForAdmin());
        }
        public async Task<IActionResult> LayoutInfoDetail(int? id)
        {
            return View(await _layoutInfoService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LayoutInfo model, IFormFile image)
        {
            var result = await _layoutInfoService.CreateAsync(model, image);
            if (result)
            {
                TempData["layoutInfoCreatedSuccessfull"] = "Layout Info Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["layoutInfoCreateError"] = "Layout Info Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _layoutInfoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LayoutInfo model, IFormFile image)
        {
            var result = await _layoutInfoService.UpdateAsync(model, image);
            if (result)
            {
                TempData["layoutInfoUpdatedSuccessfull"] = "Layout Info Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["layoutInfoUpdateError"] = "Layout Info Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _layoutInfoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteLayoutInfo(LayoutInfo model, int id)
        {
            var result = await _layoutInfoService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(LayoutInfoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _layoutInfoService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LayoutInfoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _layoutInfoService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LayoutInfoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _layoutInfoService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LayoutInfoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _layoutInfoService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LayoutInfoOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
