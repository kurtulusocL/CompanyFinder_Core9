using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CompanyFinder.WebUI.Controllers
{
    [ExceptionHandler]
    public class ToDoController : Controller
    {
        readonly IToDoService _toDoService;
        readonly IUserService _userService;
        public ToDoController(IToDoService toDoService, IUserService userService)
        {
            _toDoService = toDoService;
            _userService = userService;
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _toDoService.GetAllIncludingOpenTaskAsync();
            return View(result.ToPagedList(page, 36));
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _toDoService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public IActionResult HitRead(int? id)
        {
            return PartialView(_toDoService.HitRead(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _toDoService.GetAllIncludingAsync());
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> OpenTask()
        {
            return View(await _toDoService.GetAllIncludingOpenTaskAsync());
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> ClosedTask()
        {
            return View(await _toDoService.GetAllIncludingFinishedAsync());
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> ByAdmin(string id)
        {
            return View(await _toDoService.GetAllIncludingByUserIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> OpenTaskByAdmin(string id)
        {
            return View(await _toDoService.GetAllIncludingOpenTaskByUserIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> ClosedTaskByAdmin(string id)
        {
            return View(await _toDoService.GetAllIncludingFinishedTaskByUserIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> TaskOps()
        {
            return View(await _toDoService.GetAllIncludingForAdmin());
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> TaskDetail(int? id)
        {
            return View(await _toDoService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Admins = await _userService.GetAllAdminForAddTask();
            return View();
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId)
        {
            var result = await _toDoService.CreateAsync(title, subtitle, desc, startDate, endDate, userId);
            if (result)
            {
                TempData["taskCreatedSuccessfull"] = "Task Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["taskCreateError"] = "Task Create Error";
            return RedirectToAction(nameof(Create));
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Admins = await _userService.GetAllAdminForAddTask();
            var data = await _toDoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string title, string? subtitle, string desc, DateTime startDate, DateTime endDate, string userId, int id)
        {
            var result = await _toDoService.UpdateAsync(title, subtitle, desc, startDate, endDate, userId, id);
            if (result)
            {
                TempData["taskUpdatedSuccessfull"] = "Task Upadted Successfull";
                return RedirectToAction(nameof(Edit), new { id = id });
            }
            TempData["taskUpdateError"] = "Task Update Error";
            return RedirectToAction(nameof(Edit), new { id = id });
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _toDoService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> DeleteTask(ToDo model, int id)
        {
            var result = await _toDoService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(TaskOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins, HelperAdmins, AsistantAdmins, WorkerAdmins")]
        public async Task<IActionResult> SetFinished(int id)
        {
            var result = await _toDoService.SetFinishedAsync(id);
            if (result)
            {
                return LocalRedirect($"{Request.Path}{Request.QueryString}");
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> SetNotFinished(int id)
        {
            var result = await _toDoService.SetNotFinishedAsync(id);
            if (result)
            {
                return LocalRedirect($"{Request.Path}{Request.QueryString}");
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _toDoService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TaskOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _toDoService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TaskOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _toDoService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TaskOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [Authorize(Roles = "Admins, SecondAdmins")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _toDoService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(TaskOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
