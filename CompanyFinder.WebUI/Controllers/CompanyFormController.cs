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
    public class CompanyFormController : Controller
    {
        readonly ICompanyFormService _companyFormService;
        readonly IFormCategoryService _formCategoryService;
        readonly ICompanyFormAnswerService _companyFormAnswerService;
        readonly ICompanyService _companyService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly IReportService _reportService;
        readonly IReportCategoryService _reportCategoryService;
        public CompanyFormController(ICompanyFormService companyFormService, IFormCategoryService formCategoryService, ICompanyFormAnswerService companyFormAnswerService, ICompanyService companyService, IHitService hitService, ILikeService likeService, IReportService reportService, IReportCategoryService reportCategoryService)
        {
            _companyFormService = companyFormService;
            _formCategoryService = formCategoryService;
            _companyFormAnswerService = companyFormAnswerService;
            _companyService = companyService;
            _hitService = hitService;
            _likeService = likeService;
            _reportService = reportService;
            _reportCategoryService = reportCategoryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 42));
        }
        public async Task<IActionResult> Company(int? id, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 42));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingByFormCategoryIdAsync(id);
            return View(result.ToPagedList(page, 42));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            await _likeService.LikeCompanyFormAsync(id, userId, currentValue);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyFormHit(id, userId, currentValue));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _companyFormService.GetByIdAsync(id));
        }
        public async Task<IActionResult> TopicCreate(int? id)
        {
            ViewBag.FormCategories = await _formCategoryService.GetAllIncludingForAddAsync();
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            CompanyForm model = new CompanyForm
            {
                CompanyId = id
            };
            return View("TopicCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopicCreate(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image)
        {
            var result = await _companyFormService.CreateAsync(title, subtitle, desc, isAnswerable, formCategoryId, companyId, image);
            if (result)
            {
                TempData["formTopicCreatedSuccessfull"] = "Firma formu başarıyla yayınlandı.";
                return RedirectToAction(nameof(TopicCreate), new { id = companyId });
            }
            TempData["formTopicCreateError"] = "Firma formu yayınlanırken bir hata meydana geldi. Tüm zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyin. Hatanızın devam etmesi durumunda bizimle iletişime geçmekten lütfen çekinmeyin.";
            return RedirectToAction(nameof(TopicCreate), new { id = companyId });
        }
        public async Task<IActionResult> TopicUpdate(int? id)
        {
            ViewBag.FormCategories = await _formCategoryService.GetAllIncludingForAddAsync();
            var data = await _companyFormService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopicUpdate(string title, string? subtitle, string desc, bool isAnswerable, int formCategoryId, int? companyId, IFormFile image, int id)
        {
            var result = await _companyFormService.UpdateAsync(title, subtitle, desc, isAnswerable, formCategoryId, companyId, image, id);
            if (result)
            {
                TempData["formTopicUpdatedSuccessfull"] = "Firma formu başarıyla güncellendi ve tekrar yayınlandı.";
                return RedirectToAction(nameof(TopicUpdate), new { id = id });
            }
            TempData["formTopicUpdateError"] = "Firma formu güncellenip yayınlanırken bir hata meydana geldi. Tüm zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyin. Hatanızın devam etmesi durumunda bizimle iletişime geçmekten lütfen çekinmeyin.";
            return RedirectToAction(nameof(TopicUpdate), new { id = id });
        }
        public async Task<IActionResult> TopicAnswerCreate(int? id)
        {
            ViewBag.CompanyFormId = await _companyFormService.GetByIdAsync(id);
            CompanyFormAnswer model = new CompanyFormAnswer
            {
                CompanyFormId = id
            };
            return View("TopicAnswerCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TopicAnswerCreate(string? title, string desc, int? companyFormId, string userId)
        {
            var result = await _companyFormAnswerService.CreateAsync(title, desc, companyFormId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = companyFormId });
            }
            TempData["formTopicAnswerCreateError"] = "Cevabınız yayınlanırken bir hata meydana geldi. Tüm zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyin. Hatanızın devam etmesi durumunda bizimle iletişime geçmekten lütfen çekinmeyin.";
            return RedirectToAction(nameof(Detail), new { id = companyFormId });
        }
        public async Task<IActionResult> SendReport(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.CompanyFormId = await _companyFormService.GetByIdAsync(id);
            Report model = new Report
            {
                CompanyFormId = id
            };
            return View("SendReport", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendReport(string title, string desc, int reportCategoryId, int? companyFormId, string userId)
        {
            var result = await _reportService.CreateCompanyFormReportAsync(title, desc, reportCategoryId, companyFormId, userId);
            if (result)
            {
                TempData["companyFormReportSentSuccessfull"] = "Bildiriminizi aldık. En kısa sürede bildiriminizi inceleyip size dönüş yapacağız. Bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(SendReport), new { id = companyFormId });
            }
            TempData["companyFormReportSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz. Eğer hata hala devam ederse, bildirmek istediğiniz içeriğin adresini kopyalayıp bize iletişim sayfamızda yazan email adresimizi kullanarak mail ile bildirimde bulunabilirsiniz. Almış olduğunuz hatayı en kısa sürede çözeceğiz, hata için özür dileriz.";
            return RedirectToAction(nameof(SendReport), new { id = companyFormId });
        }
    }
}
