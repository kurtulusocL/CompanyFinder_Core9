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
    public class AnnouncementController : Controller
    {
        readonly IAnnouncementService _announcemenetService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcemenetService = announcementService;
        }
        public async Task<IActionResult> kurtulusocL(int page = 1)
        {
            var result = await _announcemenetService.GetAllAsync();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> AdminAnnouncement(int page = 1)
        {
            var result = await _announcemenetService.GetAllAdminAnnouncementAsync();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> UserAnnouncement(int page = 1)
        {
            var result = await _announcemenetService.GetAllUserAnnouncementAsync();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> AnnouncementforEveryone(int page = 1)
        {
            var result = await _announcemenetService.GetAllAnnouncementForEverybodyAsync();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> AnnouncementOps(int page = 1)
        {
            var result = await _announcemenetService.GetAllForAdmin();
            return View(result.ToPagedList(page, 40));
        }
        public async Task<IActionResult> AnnouncementDetail(int? id)
        {
            return View(await _announcemenetService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Announcement model, IFormFile image)
        {
            var result = await _announcemenetService.CreateAsync(model, image);
            if (result)
            {
                TempData["announcementCreatedSuccessfull"] = "Announcement Created Successfully";
                return RedirectToAction(nameof(Create));
            }
            TempData["announcementCreateError"] = "Announcement Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _announcemenetService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Announcement model, IFormFile image)
        {
            var result = await _announcemenetService.UpdateAsync(model, image);
            if (result)
            {
                TempData["announcementUpdatedSuccessfull"] = "Announcement Updated Successfully";
                return RedirectToAction(nameof(Create));
            }
            TempData["announcementUpdateError"] = "Announcement Update Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _announcemenetService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteAnnouncement(Announcement model, int id)
        {
            var result = await _announcemenetService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(AnnouncementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _announcemenetService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnnouncementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _announcemenetService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnnouncementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _announcemenetService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnnouncementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _announcemenetService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(AnnouncementOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}