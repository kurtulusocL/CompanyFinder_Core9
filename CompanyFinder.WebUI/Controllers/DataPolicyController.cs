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
    public class DataPolicyController : Controller
    {
        readonly IDataPolicyService _dataPolicyService;
        public DataPolicyController(IDataPolicyService dataPolicyService)
        {
            _dataPolicyService = dataPolicyService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _dataPolicyService.GetAllAsync());
        }
        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _dataPolicyService.GetAllAsync());
        }
        public async Task<IActionResult> DataPolicyOps()
        {
            return View(await _dataPolicyService.GetAllForAdmin());
        }
        public async Task<IActionResult> DataPolicyDetail(int? id)
        {
            return View(await _dataPolicyService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DataPolicy model)
        {
            var result = await _dataPolicyService.CreateAsync(model);
            if (result)
            {
                TempData["dataPolicyCreatedSuccessfull"] = "Data Policy Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["dataPolicyCreateError"] = "Data Policy Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _dataPolicyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DataPolicy model)
        {
            var result = await _dataPolicyService.UpdateAsync(model);
            if (result)
            {
                TempData["dataPolicyUpdatedSuccessfull"] = "Data Policy Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["dataPolicyUpdateError"] = "Data Policy Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _dataPolicyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteDataPolicy(DataPolicy model, int id)
        {
            var result = await _dataPolicyService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(DataPolicyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _dataPolicyService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DataPolicyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _dataPolicyService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DataPolicyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _dataPolicyService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DataPolicyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _dataPolicyService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(DataPolicyOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
