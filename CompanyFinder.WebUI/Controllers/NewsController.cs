using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
    public class NewsController : Controller
    {
        readonly INewsService _newsService;
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _newsService.GetAllAsync();
            return View(result.ToPagedList(page, 24));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _newsService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Like(int? id)
        {
            var result = await _newsService.LikeAsync(id);
            if (result)
                return RedirectToAction(nameof(Detail), new { id = id });
            return RedirectToAction(nameof(Detail), new { id = id });
        }

        [AllowAnonymous]
        public IActionResult HitRead(int? id)
        {
            return PartialView(_newsService.HitRead(id));
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _newsService.GetAllAsync());
        }
        public async Task<IActionResult> NewsOps()
        {
            return View(await _newsService.GetAllForAdmin());
        }
        public async Task<IActionResult> NewsDetail(int? id)
        {
            return View(await _newsService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News model, IFormFile image)
        {
            var result = await _newsService.CreateAsync(model, image);
            if (result)
            {
                TempData["newsCreatedSuccessfull"] = "News Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["newsCreateError"] = "News Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _newsService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News model, IFormFile image)
        {
            var result = await _newsService.UpdateAsync(model, image);
            if (result)
            {
                TempData["newsUpdatedSuccessfull"] = "News Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["newsUpdateError"] = "News Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _newsService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteNews(News model, int id)
        {
            var result = await _newsService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(NewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _newsService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(NewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _newsService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(NewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _newsService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(NewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _newsService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(NewsOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
