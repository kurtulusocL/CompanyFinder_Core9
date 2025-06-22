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
    public class BlogController : Controller
    {
        readonly IBlogService _blogService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly ICommentService _commentService;
        readonly IReportService _reportService;
        readonly IReportCategoryService _reportCategoryService;
        readonly ISavedContentService _savedContentService;
        public BlogController(IBlogService blogService, IHitService hitService, ILikeService likeService, ICommentService commentService, IReportService reportService, IReportCategoryService reportCategoryService, ISavedContentService savedContentService)
        {
            _blogService = blogService;
            _hitService = hitService;
            _likeService = likeService;
            _commentService = commentService;
            _reportService = reportService;
            _reportCategoryService = reportCategoryService;
            _savedContentService = savedContentService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _blogService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingByCategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Company(int? id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _blogService.GetByIdAsync(id));
        }
        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return View(_hitService.BlogHit(id, userId, currentValue));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            var result = await _likeService.LikeBlogAsync(id, userId, currentValue);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> CommentCreate(int? id)
        {
            ViewBag.BlogId = await _blogService.GetByIdAsync(id);
            Comment model = new Comment
            {
                BlogId = id
            };
            return View("CommentCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentCreate(string? subject, string text, int? blogId, string userId)
        {
            var result = await _commentService.CreateBlogCommentAsync(subject, text, blogId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = blogId });
            }
            TempData["blogCommentCreateError"] = "Yorumunuz kaydedilirken bir hata meydana geldi. Lütfen zorunlu olanları doldurun ve tekrar yorum yapmayı deneyin. Sorununuz devam ederse lütfen bizze bildiriniz. Yaşadığınız sorundan dolayı üzgünüz.";
            return RedirectToAction(nameof(Detail), new { id = blogId });
        }

        public async Task<IActionResult> Report(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.BlogId = await _blogService.GetByIdAsync(id);
            Report model = new Report
            {
                BlogId = id
            };
            return View("Report", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(string title, string desc, int reportCategoryId, int? blogId, string userId)
        {
            var result = await _reportService.CreateBlogReportAsync(title, desc, reportCategoryId, blogId, userId);
            if (result)
            {
                TempData["reportSentSuccessfull"] = "Bildiriminizi aldık. En kısa sürede bildiriminizi inceleyip size dönüş yapacağız. Bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(Report), new { id = blogId });
            }
            TempData["reportSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz. Eğer hata hala devam ederse, bildirmek istediğiniz içeriğin adresini kopyalayıp bize iletişim sayfamızda yazan email adresimizi kullanarak mail ile bildirimde bulunabilirsiniz. Almış olduğunuz hatayı en kısa sürede çözeceğiz, hata için özür dileriz.";
            return RedirectToAction(nameof(Report), new { id = blogId });
        }
        public async Task<IActionResult> SaveArticle(bool isSaved, int? companyId, int? productId, int? id, int? companyPartnershipId, string userId, SavedContent model)
        {
            model.BlogId = id;
            var result = await _savedContentService.SaveAsync(isSaved, companyId, productId, id, companyPartnershipId, userId);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
    }
}
