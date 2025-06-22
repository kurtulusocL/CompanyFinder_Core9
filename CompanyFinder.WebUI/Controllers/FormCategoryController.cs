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
    public class FormCategoryController : Controller
    {
        readonly IFormCategoryService _formCategoryService;
        public FormCategoryController(IFormCategoryService formCategoryService)
        {
            _formCategoryService = formCategoryService;
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _formCategoryService.GetAllIncludingAsync());
        }
        public async Task<IActionResult> FormCategoryOps()
        {
            return View(await _formCategoryService.GetAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> FormCategoryDetail(int? id)
        {
            return View(await _formCategoryService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormCategory model)
        {
            var result = await _formCategoryService.CreateAsync(model);
            if (result)
            {
                TempData["formCategoryCreatedSuccessfull"] = "Form Category Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["formCategoryCreateError"] = "There was an error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _formCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FormCategory model)
        {
            var result = await _formCategoryService.UpdateAsync(model);
            if (result)
            {
                TempData["formCategoryUpdatedSuccessfull"] = "Form Category Updated.";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["formCategoryUpdateError"] = "Form Category Update Error.";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _formCategoryService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteFormCategory(FormCategory model, int id)
        {
            var result = await _formCategoryService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(FormCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _formCategoryService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FormCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _formCategoryService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FormCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _formCategoryService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FormCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _formCategoryService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(FormCategoryOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
