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
    public class LikeController : Controller
    {
        readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _likeService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByValueLessToMore()
        {
            return View(await _likeService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> ByValueMoreToLess()
        {
            return View(await _likeService.GetAllIncludingByLikeValueDescAsync());
        }
        public async Task<IActionResult> BlogLike(int? id)
        {
            return View(await _likeService.GetAllIncludingByBlogIdAsync(id));
        }
        public async Task<IActionResult> CompanyLike(int? id)
        {
            return View(await _likeService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> CompanyFormLike(int? id)
        {
            return View(await _likeService.GetAllIncludingByCompanyFormIdAsync(id));
        }
        public async Task<IActionResult> PartnershipLike(int? id)
        {
            return View(await _likeService.GetAllIncludingByCompanyPartnershipIdAsync(id));
        }
        public async Task<IActionResult> ProductLike(int? id)
        {
            return View(await _likeService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> SectorNewsLike(int? id)
        {
            return View(await _likeService.GetAllIncludingBySectorNewsIdAsync(id));
        }
        public async Task<IActionResult> UserLike(string id)
        {
            return View(await _likeService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> BlogLikeForAdmin(int? id)
        {
            return View(await _likeService.GetAllIncludingByBlogIdForAdminAsync(id));
        }
        public async Task<IActionResult> CompanyLikeForAdmin(int? id)
        {
            return View(await _likeService.GetAllIncludingByCompanyIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProductLikeForAdmin(int? id)
        {
            return View(await _likeService.GetAllIncludingByProductIdForAdminAsync(id));
        }
        public async Task<IActionResult> UserLikeForAdmin(string id)
        {
            return View(await _likeService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> LikeOps()
        {
            return View(await _likeService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> LikeDetail(int? id)
        {
            return View(await _likeService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _likeService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteLike(Like model, int id)
        {
            var result = await _likeService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(LikeOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _likeService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LikeOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _likeService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LikeOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _likeService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LikeOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _likeService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(LikeOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
