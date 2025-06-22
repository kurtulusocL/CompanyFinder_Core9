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
    public class BlogManagementController : Controller
    {
        readonly IBlogService _blogService;
        public BlogManagementController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _blogService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> Category(int id)
        {
            return View(await _blogService.GetAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> Company(int? id)
        {
            return View(await _blogService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> UserBlog(string id)
        {
            return View(await _blogService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> UserBlogForAdmin(string id)
        {
            return View(await _blogService.GetAllIncludingForAdminByUserIdAsync(id));
        }
        public async Task<IActionResult> BlogOps()
        {
            return View(await _blogService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> BlogDetail(int? id)
        {
            return View(await _blogService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _blogService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteBlog(Blog model, int id)
        {
            var result = await _blogService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(BlogOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _blogService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _blogService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _blogService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _blogService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(BlogOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
