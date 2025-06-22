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
    public class ContactController : Controller
    {
        readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _contactService.GetAllAsync());
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _contactService.GetAllAsync());
        }
        public async Task<IActionResult> ContactOps()
        {
            return View(await _contactService.GetAllForAdmin());
        }
        public async Task<IActionResult> ContactDetail(int? id)
        {
            return View(await _contactService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact model)
        {
            var result = await _contactService.CreateAsync(model);
            if (result)
            {
                TempData["contactCreateSuccess"] = "Contact Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["errorCreateSuccess"] = "Contact Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _contactService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contact model)
        {
            var result = await _contactService.UpdateAsync(model);
            if (result)
            {
                TempData["contactUpdateSuccess"] = "Contact Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["errorUpdateSuccess"] = "Contact Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _contactService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteContact(Contact model, int id)
        {
            var result = await _contactService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(ContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _contactService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _contactService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _contactService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _contactService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(ContactOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
