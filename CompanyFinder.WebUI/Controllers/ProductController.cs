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
    public class ProductController : Controller
    {
        readonly IProductService _productService;
        readonly ICommentService _commentService;
        readonly IReportCategoryService _reportCategoryService;
        readonly IReportService _reportService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly IQuestionService _questionService;
        readonly ISavedContentService _savedContentService;
        public ProductController(IProductService productService, ICommentService commentService, IReportCategoryService reportCategoryService, IReportService reportService, IHitService hitService, ILikeService likeService, IQuestionService questionService, ISavedContentService savedContentService)
        {
            _productService = productService;
            _commentService = commentService;
            _reportCategoryService = reportCategoryService;
            _reportService = reportService;
            _hitService = hitService;
            _likeService = likeService;
            _questionService = questionService;
            _savedContentService = savedContentService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _productService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> AvailableProduct(int page = 1)
        {
            var result = await _productService.GetAllIncludingAvailableProductsAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _productService.GetAllIncludingByCategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Subcategory(int? id, int page = 1)
        {
            var result = await _productService.GetAllIncludingBySubcategoryIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Company(int id, int page = 1)
        {
            var result = await _productService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _productService.GetByIdAsync(id));
        }

        public IActionResult HitRead(int? id, string userId, int currentValue)
        {
            return View(_hitService.ProductHit(id, userId, currentValue));
        }
        public async Task<IActionResult> Like(int? id, string userId, int currentValue)
        {
            var result = await _likeService.LikeProductAsync(id, userId, currentValue);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = id });
            }
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> SaveProduct(bool isSaved, int? companyId, int? id, int? blogId, int? companyPartnershipId, string userId, SavedContent model)
        {
            model.ProductId = id;
            var result = await _savedContentService.SaveAsync(isSaved, companyId, id, blogId, companyPartnershipId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = id });
            }
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        public async Task<IActionResult> CreateComment(int? id)
        {
            ViewBag.ProductId = await _productService.GetByIdAsync(id);
            Comment model = new Comment
            {
                ProductId = id
            };
            return View("CreateComment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string? subject, string text, int? productId, string userId)
        {
            var result = await _commentService.CreateProductCommentAsync(subject, text, productId, userId);
            if (result)
            {
                return RedirectToAction(nameof(Detail), new { id = productId });
            }
            TempData["productCommentCreateError"] = "Yorumunuz kaydedilirken bir hata meydana geldi. Lütfen zorunlu olanları doldurun ve tekrar yorum yapmayı deneyin. Sorununuz devam ederse lütfen bizze bildiriniz. Yaşadığınız sorundan dolayı üzgünüz.";
            return RedirectToAction(nameof(Detail), new { id = productId });
        }
        public async Task<IActionResult> CreateQuestion(int? id)
        {
            ViewBag.ProductId = await _productService.GetByIdAsync(id);
            Question model = new Question
            {
                ProductId = id
            };
            return View("CreateQuestion", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuestion(string text, int? productId, string userId)
        {
            var result = await _questionService.CreateProductQuestionAsync(text, productId, userId);
            if (result)
            {
                TempData["productQuestionCreateSuccessfull"] = "Sorunuz başarı ile gönderildi.";
                return RedirectToAction(nameof(Detail), new { id = productId });
            }
            TempData["productQuestionCreateError"] = "Sorunuz gönderilirken bir hata meydana geldi. Lütfen zorunlu olanları doldurun ve tekrar göndermeyi deneyin.";
            return RedirectToAction(nameof(Detail), new { id = productId });
        }
        public async Task<IActionResult> SendReport(int? id)
        {
            ViewBag.ProductId = await _productService.GetByIdAsync(id);
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            Report model = new Report
            {
                ProductId = id
            };
            return View("SendReport", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendReport(string title, string desc, int reportCategoryId, int? productId, string userId)
        {
            var result = await _reportService.CreateProductReportAsync(title, desc, reportCategoryId, productId, userId);
            if (result)
            {
                TempData["productReportSentSuccessfull"] = "Bildiriminizi aldık. En kısa sürede bildiriminizi inceleyip size dönüş yapacağız. Bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(SendReport), new { id = productId });
            }
            TempData["productReportSentError"] = "Bir hata meydana geldi. Lütfen gereki alanları doldurup tekrar göndermeyi deneyiniz. Eğer hata hala devam ederse, bildirmek istediğiniz içeriğin adresini kopyalayıp bize iletişim sayfamızda yazan email adresimizi kullanarak mail ile bildirimde bulunabilirsiniz. Almış olduğunuz hatayı en kısa sürede çözeceğiz, hata için özür dileriz.";
            return RedirectToAction(nameof(SendReport), new { id = productId });
        }
    }
}
