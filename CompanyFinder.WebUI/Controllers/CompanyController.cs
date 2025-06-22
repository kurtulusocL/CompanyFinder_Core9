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
    public class CompanyController : Controller
    {
        readonly ICompanyService _companyService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly ICommentService _commentService;
        readonly IReportCategoryService _reportCategoryService;
        readonly IReportService _reportService;
        readonly IQuestionService _questionService;
        readonly ISavedContentService _savedContentService;
        readonly IAppointmentService _appointmentService;
        public CompanyController(ICompanyService companyService, IHitService hitService, ILikeService likeService, ICommentService commentService, IReportCategoryService reportCategoryService, IReportService reportService, IQuestionService questionService, ISavedContentService savedContentService, IAppointmentService appointmentService)
        {
            _companyService = companyService;
            _hitService = hitService;
            _likeService = likeService;
            _commentService = commentService;
            _reportCategoryService = reportCategoryService;
            _reportService = reportService;
            _questionService = questionService;
            _savedContentService = savedContentService;
            _appointmentService = appointmentService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _companyService.GetAllIncludingCompaniesForCompanyUserAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Country(int id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingByCountryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingByCategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> City(int? id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingByCityIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Subcategory(int id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingBySubcategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _companyService.GetIncludingCompanyForCompanyUserByIdAsync(id));
        }
        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return View(_hitService.CompanyHit(id, userId, currentValue));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            await _likeService.LikeCompanyAsync(id, userId, currentValue);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> CreateComment(int? id)
        {
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Comment model = new Comment
            {
                CompanyId = id
            };
            return View("CreateComment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string? subject, string text, int? companyId, string userId)
        {
            var result = await _commentService.CreateCompanyCommentAsync(subject, text, companyId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = companyId });
            }
            TempData["companyCommentCreateError"] = "Yorumunuz kaydedilirken bir hata meydana geldi. Lütfen zorunlu olanları doldurun ve tekrar yorum yapmayı deneyin. Sorununuz devam ederse lütfen bizze bildiriniz. Yaşadığınız sorundan dolayı üzgünüz.";
            return RedirectToAction(nameof(Detail), new { id = companyId });
        }
        public async Task<IActionResult> Report(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Report model = new Report
            {
                CompanyId = id
            };
            return View("Report", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(string title, string desc, int reportCategoryId, int? companyId, string userId)
        {
            var result = await _reportService.CreateCompanyReportAsync(title, desc, reportCategoryId, companyId, userId);
            if (result)
            {
                TempData["companyReportSentSuccessfull"] = "Bildiriminizi aldık. En kısa sürede bildiriminizi inceleyip size dönüş yapacağız. Bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(Report), new { id = companyId });
            }
            TempData["companyReportSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz. Eğer hata hala devam ederse, bildirmek istediğiniz içeriğin adresini kopyalayıp bize iletişim sayfamızda yazan email adresimizi kullanarak mail ile bildirimde bulunabilirsiniz. Almış olduğunuz hatayı en kısa sürede çözeceğiz, hata için özür dileriz.";
            return RedirectToAction(nameof(Report), new { id = companyId });
        }
        public async Task<IActionResult> SaveCompany(bool isSaved, int? id, int? productId, int? blogId, int? companyPartnershipId, string userId, SavedContent model)
        {
            model.CompanyId = id;
            var result = await _savedContentService.SaveAsync(isSaved, id, productId, blogId, companyPartnershipId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = id });
            }
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> CompanyQuestion(int? id)
        {
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Question model = new Question
            {
                CompanyId = id
            };
            return View("CompanyQuestion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyQuestion(string text, int? companyId, string userId)
        {
            var result = await _questionService.CreateCompanyQuestionAsync(text, companyId, userId);
            if (result)
            {
                TempData["companyQuestionSentSuccessfull"] = "Sorunuz başarı ile iletildi.";
                return RedirectToAction(nameof(Detail), new { id = companyId });
            }
            TempData["companyQuestionSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz.";
            return RedirectToAction(nameof(Detail), new { id = companyId });
        }
        public async Task<IActionResult> CreateAppointment(int? id)
        {
            ViewBag.CompanyId = await _appointmentService.GetByIdAsync(id);
            Appointment model = new Appointment
            {
                CompanyId = id
            };
            return View("CreateAppointment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(string subject, string desc, DateTime appointmentDate, int? companyId, string userId)
        {
            var result = await _appointmentService.CreateAsync(subject, desc, appointmentDate, companyId, userId);
            if (result)
            {
                TempData["companyAppointmentSentSuccessfull"] = "Randevu talebiniz başarı ile iletildi.";
                return RedirectToAction(nameof(Detail), new { id = companyId });
            }
            TempData["companyAppointmentSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz.";
            return RedirectToAction(nameof(Detail), new { id = companyId });
        }
    }
}
