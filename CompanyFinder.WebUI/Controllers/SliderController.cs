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
    public class SliderController : Controller
    {
        readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IActionResult> kurtulusocL(int page = 1)
        {
            var result = await _sliderService.GetAllAsync();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> SliderOps(int page = 1)
        {
            var result = await _sliderService.GetAllForAdmin();
            return View(result.ToPagedList(page, 45));
        }
        public async Task<IActionResult> SliderDetail(int? id)
        {
            return View(await _sliderService.GetByIdAsync(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider model, IFormFile image)
        {
            var result = await _sliderService.CreateAsync(model, image);
            if (result)
            {
                TempData["sliderCreatedSuccessfull"] = "Slide Created Successfull";
                return RedirectToAction(nameof(Create));
            }
            TempData["sliderCreateError"] = "Slide Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var data = await _sliderService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slider model, IFormFile image)
        {
            var result = await _sliderService.UpdateAsync(model, image);
            if (result)
            {
                TempData["sliderUpdatedSuccessfull"] = "Slide Updated Successfull";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["sliderUpdateError"] = "Slide Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _sliderService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeleteSlider(Slider model, int id)
        {
            var result = await _sliderService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(SliderOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _sliderService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SliderOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _sliderService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SliderOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _sliderService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SliderOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _sliderService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(SliderOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
