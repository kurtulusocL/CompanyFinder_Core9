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
    public class BlogCategoryController : Controller
    {
        readonly IBlogCategoryService _blogCategoryService;
        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _blogCategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> BlogCategoryOps()
        {
            return View(await _blogCategoryService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> BlogCategoryDetail(int? id)
        {
            return View(await _blogCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategory model)
        {
            var result = await _blogCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["blogCategoryCreatedSuccessfull"] = "Blog Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["blogCategoryCreatedError"] = "Blog Category Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _blogCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogCategory model)
        {
            var result = await _blogCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["blogCategoryUpdatedSuccessfull"] = "Blog Category Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["blogCategoryUpdateError"] = "Blog Category Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _blogCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteBlogCategory(BlogCategory model, int id)
        {
            var result = await _blogCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(BlogCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _blogCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _blogCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _blogCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _blogCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}