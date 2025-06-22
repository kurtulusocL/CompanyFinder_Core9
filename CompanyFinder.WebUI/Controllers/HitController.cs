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
    public class HitController : Controller
    {
        readonly IHitService _hitService;
        public HitController(IHitService hitService)
        {
            _hitService = hitService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _hitService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> HitByValue()
        {
            return View(await _hitService.GetAllIncludingByValueAsync());
        }
        public async Task<IActionResult> HitByAd(int? id)
        {
            return View(await _hitService.GetAllIncludingByAdIdAsync(id));
        }
        public async Task<IActionResult> HitByBlog(int? id)
        {
            return View(await _hitService.GetAllIncludingByBlogIdAsync(id));
        }
        public async Task<IActionResult> HitByCompany(int? id)
        {
            return View(await _hitService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> HitByCompanyForm(int? id)
        {
            return View(await _hitService.GetAllIncludingByCompanyFormIdAsync(id));
        }
        public async Task<IActionResult> HitByPartnership(int? id)
        {
            return View(await _hitService.GetAllIncludingByCompanyPartnershipIdAsync(id));
        }
        public async Task<IActionResult> HitByProduct(int? id)
        {
            return View(await _hitService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> HitBySectorNews(int? id)
        {
            return View(await _hitService.GetAllIncludingBySectorNewsIdAsync(id));
        }
        public async Task<IActionResult> HitByUser(string id)
        {
            return View(await _hitService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> HitByUserForAdmin(string id)
        {
            return View(await _hitService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> HitOps()
        {
            return View(await _hitService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> HitDetail(int? id)
        {
            return View(await _hitService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _hitService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteHit(Hit model, int id)
        {
            var result = await _hitService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(HitOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _hitService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(HitOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _hitService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(HitOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _hitService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(HitOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _hitService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(HitOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
