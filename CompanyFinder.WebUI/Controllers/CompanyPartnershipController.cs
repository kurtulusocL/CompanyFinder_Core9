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
    [Authorize(Roles = "Companies")]
    public class CompanyPartnershipController : Controller
    {
        readonly ICompanyPartnershipService _companyPartnershipService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly ISavedContentService _savedContentService;
        readonly IReportCategoryService _reportCategoryService;
        readonly IReportService _reportService;
        public CompanyPartnershipController(ICompanyPartnershipService companyPartnershipService, IHitService hitService, ILikeService likeService, ISavedContentService savedContentService, IReportCategoryService reportCategoryService, IReportService reportService)
        {
            _companyPartnershipService = companyPartnershipService;
            _hitService = hitService;
            _likeService = likeService;
            _savedContentService = savedContentService;
            _reportCategoryService = reportCategoryService;
            _reportService = reportService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> StartDate(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingByStartDateAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> ExpiryDate(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingByExpiryDateAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Available(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingAvailableAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Company(int? id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingByProductCategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _companyPartnershipService.GetByIdAsync(id));
        }
        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyPartnershipHit(id, userId, currentValue));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            var result = await _likeService.LikeCompanyPartnershipAsync(id, userId, currentValue);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> SavePartnership(bool isSaved, int? companyId, int? productId, int? blogId, int? id, string userId, SavedContent model)
        {
            model.CompanyPartnershipId = id;
            var result = await _savedContentService.SaveAsync(isSaved, companyId, productId, blogId, id, userId);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> Report(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.CompanyPartnershipId = await _companyPartnershipService.GetByIdAsync(id);
            Report model = new Report
            {
                CompanyPartnershipId = id
            };
            return View("Report", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(string title, string desc, int reportCategoryId, int? id, string userId)
        {
            var result = await _reportService.CreateCompanyPartnershipReportAsync(title, desc, reportCategoryId, id, userId);
            if (result)
            {
                TempData["partnershipReportSentSuccessfull"] = "Bildiriminizi aldık. En kısa sürede bildiriminizi inceleyip size dönüş yapacağız. Bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(Report), new { id = id });
            }
            TempData["partnershipReportSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz. Eğer hata hala devam ederse, bildirmek istediğiniz içeriğin adresini kopyalayıp bize iletişim sayfamızda yazan email adresimizi kullanarak mail ile bildirimde bulunabilirsiniz. Almış olduğunuz hatayı en kısa sürede çözeceğiz, hata için özür dileriz.";
            return RedirectToAction(nameof(Report), new { id = id });
        }
    }
}
