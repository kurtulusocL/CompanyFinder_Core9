using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Companies")]
    public class SectorNewsController : Controller
    {
        readonly ISectorNewsService _sectorNewsService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        public SectorNewsController(ISectorNewsService sectorNewsService, IHitService hitService, ILikeService likeService)
        {
            _sectorNewsService = sectorNewsService;
            _hitService = hitService;
            _likeService = likeService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _sectorNewsService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _sectorNewsService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            var result = await _likeService.LikeSectorNewsAsync(id, userId, currentValue);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return PartialView(_hitService.SectorNewsHit(id, userId, currentValue));
        }
    }
}
