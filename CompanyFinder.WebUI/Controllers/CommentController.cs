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
    public class CommentController : Controller
    {
        readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _commentService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> BlogComment(int? id)
        {
            return View(await _commentService.GetAllIncludingByBlogIdAsync(id));
        }
        public async Task<IActionResult> CompanyComment(int? id)
        {
            return View(await _commentService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ProductComment(int? id)
        {
            return View(await _commentService.GetAllIncludingByProductIdAsync(id));
        }
        public async Task<IActionResult> UserComment(string id)
        {
            return View(await _commentService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> BlogCommentForAdmin(int? id)
        {
            return View(await _commentService.GetAllIncludingByBlogIdForAdminAsync(id));
        }
        public async Task<IActionResult> CompanyCommentForAdmin(int? id)
        {
            return View(await _commentService.GetAllIncludingByCompanyIdForAdminAsync(id));
        }
        public async Task<IActionResult> ProductCommentForAdmin(int? id)
        {
            return View(await _commentService.GetAllIncludingByProductIdForAdminAsync(id));
        }
        public async Task<IActionResult> UserCommentForAdmin(string id)
        {
            return View(await _commentService.GetAllIncludingByUserIdForAdminAsync(id));
        }
        public async Task<IActionResult> CommentOps()
        {
            return View(await _commentService.GetAllIncludingForAdmin());
        }
        public async Task<IActionResult> CommentDetail(int? id)
        {
            return View(await _commentService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _commentService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteComment(Comment model, int id)
        {
            var result = await _commentService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(CommentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _commentService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _commentService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _commentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _commentService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(CommentOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
