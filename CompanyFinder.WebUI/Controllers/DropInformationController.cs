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
    public class DropInformationController : Controller
    {
        readonly IDropInformationService _dropInformationService;
        public DropInformationController(IDropInformationService dropInformationService)
        {
            _dropInformationService = dropInformationService;
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DropInformation model)
        {
            var result = _dropInformationService.Create(model);
            if (result)
            {
                TempData["dropInformationSuccessfull"] = "Bilgilerinizi gönderdiğiniz için teşekkür ederiz. En kısa sürede sizinle iletişime geçeceğiz. İyi günler dileriz.";
                var currentUrls = $"{Request.Path}{Request.QueryString}";
                return LocalRedirect(currentUrls);
            }
            TempData["dropInformationError"] = "Bilgilerinizi gönderirken bir hata meydana geldi. Lütfen gerekli alanları doldurup tekrar göndermeyi deneyiniz. Bu durum için üzgün olduğumuzu bildiririz.";
            var currentUrl = $"{Request.Path}{Request.QueryString}";
            return LocalRedirect(currentUrl);
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _dropInformationService.GetAllAsync());
        }
        public async Task<IActionResult> DropInformationOps()
        {
            return View(await _dropInformationService.GetAllForAdmin());
        }
        public async Task<IActionResult> DropInformationDetail(int? id)
        {
            return View(await _dropInformationService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _dropInformationService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteDropInformation(DropInformation model, int id)
        {
            var result = await _dropInformationService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(DropInformationOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _dropInformationService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DropInformationOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _dropInformationService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DropInformationOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _dropInformationService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DropInformationOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _dropInformationService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DropInformationOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
