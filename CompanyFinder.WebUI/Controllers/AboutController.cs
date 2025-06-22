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
    public class AboutController : Controller
    {
        readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _aboutService.GetAllAsync());
        }

        [AllowAnonymous]
        public IActionResult HitRead(int? id)
        {
            return PartialView(_aboutService.HitRead(id));
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _aboutService.GetAllAsync());
        }
        public async Task<IActionResult> AboutOps()
        {
            return View(await _aboutService.GetAllForAdmin());
        }
        public async Task<IActionResult> AboutDetail(int? id)
        {
            return View(await _aboutService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About model, IFormFile image)
        {
            var result = await _aboutService.CreateAsync(model, image);
            if (result)
            {
                TempData["aboutCreateSuccessfull"] = "About Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["aboutCreateError"] = "About Created Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _aboutService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(About model, IFormFile image)
        {
            var result = await _aboutService.UpdateAsync(model, image);
            if (result)
            {
                TempData["aboutUpdateSuccessfull"] = "About Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["aboutUpdateError"] = "About Updated Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _aboutService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAbout(About model, int id)
        {
            var result = await _aboutService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AboutOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _aboutService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AboutOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _aboutService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AboutOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _aboutService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AboutOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _aboutService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AboutOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
