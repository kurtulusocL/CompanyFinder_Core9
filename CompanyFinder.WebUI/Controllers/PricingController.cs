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
    public class PricingController : Controller
    {
        readonly IPricingService _pricingService;
        readonly IPricingCategoryService _pricingCategoryService;
        readonly IPricingContactService _pricingContactService;
        public PricingController(IPricingService pricingService, IPricingCategoryService pricingCategoryService, IPricingContactService pricingContactService)
        {
            _pricingService = pricingService;
            _pricingCategoryService = pricingCategoryService;
            _pricingContactService = pricingContactService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _pricingService.GettAllIncludingAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetContact(int? id)
        {
            ViewBag.PricingId = await _pricingService.GetByIdAsync(id);
            PricingContact model = new PricingContact
            {
                PricingId = id
            };
            return View("GetContact", model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetContact(string nameSurname, string companyName, string? email, string phoneNumber, string desc, int? pricingId)
        {
            var result = await _pricingContactService.CreateAsync(nameSurname, companyName, email, phoneNumber, desc, pricingId);
            if (result)
            {
                TempData["pricingContactSentSuccessfull"] = "Talebiniz başarı ile iletilmiştir. Talebinizi inceleyip en kısa süre içinde belirttiğiniz iletişim bilgileri üzerinden sizinle iletişime geçeceğiz. Talebiniz için teşekkür ederiz.";
                return LocalRedirect($"{Request.Path}{Request.QueryString}");
            }
            TempData["pricingContactSentError"] = "Talebiniz iletilirken bir hata meydana geldi. Lütfen formdaki zorunlu alanları tekrar doldurduktan sonra yeniden dener misiniz? Bu durum karşısında özür diler, dilerseniz iletişim sayfamızdaki mail adreslerimizden bizimle iletişime geçebileceğinizi hatırlatmak isteriz. Teşekkür ederiz.";
            return LocalRedirect($"{Request.Path}{Request.QueryString}");
        }

        public async Task<IActionResult> kurtulusocL()
        {
            return View(await _pricingService.GettAllIncludingAsync());
        }
        public async Task<IActionResult> ByCategory(int id)
        {
            return View(await _pricingService.GettAllIncludingByCategoryIdAsync(id));
        }
        public async Task<IActionResult> PricingOps()
        {
            return View(await _pricingService.GettAllIncludingForAdminAsync());
        }
        public async Task<IActionResult> PricingDetail(int? id)
        {
            return View(await _pricingService.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.PricingCategories = await _pricingCategoryService.GettAllIncludingForAddPricingAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pricing model)
        {
            var result = await _pricingService.CreateAsync(model);
            if (result)
            {
                TempData["pricingCreate"] = "Pricing Created";
                return RedirectToAction(nameof(Create));
            }
            TempData["pricingCreateError"] = "Pricing Create Error";
            return RedirectToAction(nameof(Create));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.PricingCategories = await _pricingCategoryService.GettAllIncludingForAddPricingAsync();
            var data = await _pricingService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pricing model)
        {
            var result = await _pricingService.UpdateAsync(model);
            if (result)
            {
                TempData["pricingUpdate"] = "Pricing Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            TempData["pricingUpdateError"] = "Pricing Update Error";
            return RedirectToAction(nameof(Edit), new { id = model.Id });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await _pricingService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> DeletePricing(Pricing model, int id)
        {
            var result = await _pricingService.DeleteAsync(model, id);
            if (result)
            {
                return RedirectToAction(nameof(PricingOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetActive(int id)
        {
            var result = await _pricingService.SetActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeActive(int id)
        {
            var result = await _pricingService.SetDeActiveAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _pricingService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _pricingService.SetNotDeletedAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(PricingOps));
            }
            return RedirectToAction("Page404", "HomeAdmin");
        }
    }
}
