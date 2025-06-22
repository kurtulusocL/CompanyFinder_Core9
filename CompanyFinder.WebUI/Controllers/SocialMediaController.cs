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
    public class SocialMediaController : Controller
    {
        readonly ISocialMediaService _socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _socialMediaService.GetAllAsync());
        }
        public async Task<IActionResult> SocialMediaOps()
        {
            return View(await _socialMediaService.GetAllForAdmin());
        }
        public async Task<IActionResult> SocialMediaDetail(int? id)
        {
            return View(await _socialMediaService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia model, IFormFile image)
        {
            var result = await _socialMediaService.CreateAsync(model, image);
            if (result)
            {
                TempData["socialMediaCreatedSuccessfull"] = "Social Media Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["socialMediaCreateError"] = "Social Media Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _socialMediaService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SocialMedia model, IFormFile image)
        {
            var result = await _socialMediaService.UpdateAsync(model, image);
            if (result)
            {
                TempData["socialMediaUpdatedSuccessfull"] = "Social Media Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["socialMediaUpdateError"] = "Social Media Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _socialMediaService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteSocialMedia(SocialMedia model, int id)
        {
            var result = await _socialMediaService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(SocialMediaOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _socialMediaService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SocialMediaOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _socialMediaService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SocialMediaOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _socialMediaService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SocialMediaOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _socialMediaService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SocialMediaOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
