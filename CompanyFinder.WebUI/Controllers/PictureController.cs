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
    public class PictureController : Controller
    {
        readonly IPictureService _pictureService;
        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _pictureService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> BlogImage(int? id)
        {
            return View(await _pictureService.GetAllIncludingByBlogIdAsync(id));
        }
        public async Task<IActionResult> CompanyImage(int? id)
        {
            return View(await _pictureService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ProductImage(int? id)
        {
            return View(await _pictureService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> BlogImageForAdmin(int? id)
        {
            return View(await _pictureService.GetAllIncludingByBlogIdForAdminAsync(id));
        }
        public async Task<IActionResult> CompanyImageForAdmin(int? id)
        {
            return View(await _pictureService.GetAllIncludingByCompanyIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProductImageForAdmin(int? id)
        {
            return View(await _pictureService.GetAllIncludingByProductIdForAdminAsync(id));
        }
        public async Task<IActionResult> PictureOps()
        {
            return View(await _pictureService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> PictureDetail(int? id)
        {
            return View(await _pictureService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _pictureService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeletePicture(Picture model, int id)
        {
            var result = await _pictureService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(PictureOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _pictureService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PictureOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _pictureService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PictureOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _pictureService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PictureOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _pictureService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PictureOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
