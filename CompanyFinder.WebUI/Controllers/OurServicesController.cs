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
    public class OurServicesController : Controller
    {
        readonly IOurServicesService _ourServicesService;
        public OurServicesController(IOurServicesService ourServicesService)
        {
            _ourServicesService = ourServicesService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _ourServicesService.GetAllAsync());
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _ourServicesService.GetAllAsync());
        }
        public async Task<IActionResult> OurServicesOps()
        {
            return View(await _ourServicesService.GetAllForAdmin());
        }
        public async Task<IActionResult> OurServicesDetail(int? id)
        {
            return View(await _ourServicesService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OurServices model, IFormFile image)
        {
            var result = await _ourServicesService.CreateAsync(model, image);
            if (result)
            {
                TempData["ourServicesCreatedSuccessfull"] = "Our Services Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["ourServicesCreateError"] = "Our Services Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _ourServicesService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OurServices model, IFormFile image)
        {
            var result = await _ourServicesService.UpdateAsync(model, image);
            if (result)
            {
                TempData["ourServicesUpdatedSuccessfull"] = "Our Services Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["ourServicesUpdateError"] = "Our Services Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _ourServicesService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteOurServices(OurServices model, int id)
        {
            var result = await _ourServicesService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(OurServicesOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _ourServicesService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(OurServicesOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _ourServicesService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(OurServicesOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _ourServicesService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(OurServicesOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _ourServicesService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(OurServicesOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
