using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Companies")]
    public class HomeCompanyController : Controller
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IUserService _userService;
        readonly ICancelMembershipService _cancelMembershipService;
        readonly ICompanyService _companyService;
        readonly IProductService _productService;
        readonly ICompanyMessageService _companyMessageService;
        readonly ICompanyContactService _companyContactService;
        readonly ICommentService _commentService;
        readonly IReportService _reportService;
        readonly ISavedContentService _savedContentService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly IQuestionService _questionService;
        readonly IPictureService _pictureService;
        readonly IBlogService _blogService;
        readonly IAppointmentService _appointmentService;
        readonly IAppointmentAnswerService _appointmentAnswerService;
        readonly IAnnouncementService _announcementService;
        readonly IDataPolicyService _dataPolicyService;
        readonly IUserAgreementService _userAgreementService;
        readonly IFrequentlyService _frequentlyService;
        readonly IStockService _stockService;
        readonly ICustomerService _customerService;
        readonly IFeedbackService _feedbackService;
        readonly IAdService _adService;
        readonly ICompanyPartnershipService _companyPartnershipService;
        readonly ICompanyFormService _companyFormService;
        readonly ICompanyFormAnswerService _companyFormAnswerService;
        public HomeCompanyController(IHttpContextAccessor httpContextAccessor, IUserService userService, ICancelMembershipService cancelMembershipService, ICompanyService companyService, IProductService productService, ICompanyMessageService companyMessageService, ICompanyContactService companyContactService, ICommentService commentService, IReportService reportService, ISavedContentService savedContentService, IHitService hitService, ILikeService likeService, IQuestionService questionService, IPictureService pictureService, IBlogService blogService, IAppointmentService appointmentService, IAnnouncementService announcementService, IDataPolicyService dataPolicyService, IUserAgreementService userAgreementService, IFrequentlyService frequentlyService, IStockService stockService, ICustomerService customerService, IAppointmentAnswerService appointmentAnswerService, IFeedbackService feedbackService, IAdService adService, ICompanyPartnershipService companyPartnershipService, ICompanyFormService companyFormService, ICompanyFormAnswerService companyFormAnswerService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _cancelMembershipService = cancelMembershipService;
            _companyService = companyService;
            _productService = productService;
            _companyMessageService = companyMessageService;
            _companyContactService = companyContactService;
            _commentService = commentService;
            _reportService = reportService;
            _savedContentService = savedContentService;
            _hitService = hitService;
            _likeService = likeService;
            _questionService = questionService;
            _pictureService = pictureService;
            _blogService = blogService;
            _appointmentService = appointmentService;
            _announcementService = announcementService;
            _dataPolicyService = dataPolicyService;
            _userAgreementService = userAgreementService;
            _frequentlyService = frequentlyService;
            _stockService = stockService;
            _customerService = customerService;
            _appointmentAnswerService = appointmentAnswerService;
            _feedbackService = feedbackService;
            _adService = adService;
            _companyPartnershipService = companyPartnershipService;
            _companyFormService = companyFormService;
            _companyFormAnswerService = companyFormAnswerService;
        }
        public async Task<IActionResult> Index(string id)
        {
            return View(await _userService.GetByIdAsync(id));
        }
        public IActionResult HitRead(int? id)
        {
            return View(_questionService.HitRead(id));
        }
        public IActionResult SmilarHitRead(int? id)
        {
            return View(_adService.SimilarHitRead(id));
        }
        public IActionResult Page404()
        {
            ViewData["userId"] = _httpContextAccessor.HttpContext.Session.GetString("userId");
            return View();
        }
        public async Task<IActionResult> DataPolicy()
        {
            return View(await _dataPolicyService.GetAllAsync());
        }
        public async Task<IActionResult> UserAgreement()
        {
            return View(await _userAgreementService.GetAllAsync());
        }
        public async Task<IActionResult> Frequently()
        {
            return View(await _frequentlyService.GetAllAsync());
        }
        public async Task<IActionResult> AppointmentList(string id)
        {
            return View(await _appointmentService.GetAllIncludingAppointmentForCompanyUserByUserId(id));
        }
        public async Task<IActionResult> AnnouncementList()
        {
            return View(await _announcementService.GetAllUserAnnouncementAsync());
        }
        public async Task<IActionResult> FeedbackList(string id)
        {
            return View(await _feedbackService.GetAllByUserIdAsync(id));
        }
        public async Task<IActionResult> CancelRequestList(string id)
        {
            return View(await _cancelMembershipService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> CompanyMessageList(int? id)
        {
            return View(await _companyMessageService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> ProductList(int? id, int page = 1)
        {
            var result = await _productService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> PartnershipList(int? id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> PartnershipDetail(int? id)
        {
            return View(await _companyPartnershipService.GetByIdAsync(id));
        }
        public async Task<IActionResult> UserProductList(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> ProductDetail(int? id)
        {
            return View(await _productService.GetByIdAsync(id));
        }
        public async Task<IActionResult> ProductImageList(int? id)
        {
            return View(await _pictureService.GetAllIncludingProductImagesForCompanyUserByProductIdAsync(id));
        }
        public async Task<IActionResult> ProductStockList(int page = 1)
        {
            var result = await _stockService.GetAllIncludingAsync();
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> ProductStock(int? id)
        {
            return View(await _stockService.GetProductForCompanyUserStockByProductIdAsync(id));
        }
        public async Task<IActionResult> BlogList(int? id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> UserBlogList(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> BlogDetail(int? id)
        {
            return View(await _blogService.GetByIdAsync(id));
        }
        public async Task<IActionResult> BlogImageList(int? id)
        {
            return View(await _pictureService.GetAllIncludingBlogImagesForCompanyUserByBlogIdAsync(id));
        }
        public async Task<IActionResult> CompanyList(string id)
        {
            return View(await _companyService.GetAllIncludingByUserIdAsync(id));
        }
        public async Task<IActionResult> CompanyContactList(int? id)
        {
            return View(await _companyContactService.GetAllIncludingByCompanyIdAsync(id));
        }
        public async Task<IActionResult> CompanyImageList(int? id)
        {
            return View(await _pictureService.GetAllIncludingCompanyImagesForCompanyUserByCompanyIdAsync(id));
        }
        public async Task<IActionResult> YourCompanyForumList(int? id, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingCompanyUserCompanyFormByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourFormAnswerList(string id, int page = 1) // senin yazdığın cevaplar
        {
            var result = await _companyFormAnswerService.GetAllIncludingCompanyUserFormAnswerByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourFormTopicAnswerList(int id, int page = 1) // senin formuna yazılan cevaplar
        {
            var result = await _companyFormAnswerService.GetAllIncludingCompanyUserYourFormAnswerByCompanyFormIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> ReportList(string id, int page = 1)
        {
            var result = await _reportService.GetAllIncludingReportForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> CommentList(string id, int page = 1)
        {
            var result = await _commentService.GetAllIncludingCommentForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> SavedCompanyList(string id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedCompanyContentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedProductList(string id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedProductContentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedBlogList(string id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedBlogContentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedPartnershipList(string id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedPartnershipContentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> HitList(string id, int page = 1)
        {
            var result = await _hitService.GetAllIncludingByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> LikeList(string id, int page = 1)
        {
            var result = await _likeService.GetAllIncludingByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> QuestionList(string id, int page = 1)
        {
            var result = await _questionService.GetAllIncludingByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> MyProfile(string id)
        {
            return View(await _userService.GetCompanyUserForProfileByUserIdAsync(id));
        }
        public async Task<IActionResult> YourSavedProductList(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingSavedCompanyProductsForSaveContentProductByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourSavedPartnershipList(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingSavedPartnershipsForSaveContentPartnershipByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourSavedCompanyList(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingSavedCompaniesForSaveContentCompanyByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourSavedBlogList(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingSavedBlogsForSaveContentBlogByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourCompanyQuestionList(string id, int page = 1)
        {
            var result = await _questionService.GetAllIncludingCompanyQuestionsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourProductQuestionList(string id, int page = 1)
        {
            var result = await _questionService.GetAllIncludingProductQuestionsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourCompanyReportList(string id, int page = 1)
        {
            var result = await _reportService.GetAllIncludingCompanyReportForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourProductReportList(string id, int page = 1)
        {
            var result = await _reportService.GetAllIncludingProductReportForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourBlogReportList(string id, int page = 1)
        {
            var result = await _reportService.GetAllIncludingBlogReportForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourCompanyCommentList(string id, int page = 1)
        {
            var result = await _commentService.GetAllIncludingCompanyCommentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourProductCommentList(string id, int page = 1)
        {
            var result = await _commentService.GetAllIncludingProductCommentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourBlogCommentList(string id, int page = 1)
        {
            var result = await _commentService.GetAllIncludingBlogCommentsForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourCompanyLikeList(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingCompanyLikesForCompanyByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourProductLikeList(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingProductLikesForCompanyByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourBlogLikeList(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingBlogLikesForCompanyByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourPartnershipLikeList(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingPartnershipLikesforCompanyByUserId(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> LikedPartnershipUserList(int? id, int page = 1)
        {
            var result = await _likeService.GetAllIncludingPartnershipLikesPeopleByCompanyPartnershipIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> LikedCompanyUserList(int? id, int page = 1)
        {
            var result = await _likeService.GetAllIncludingCompanyLikesPeopleByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> LikedProductUserList(int? id, int page = 1)
        {
            var result = await _likeService.GetAllIncludingProductLikesPeopleByProductIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> LikedBlogUserList(int? id, int page = 1)
        {
            var result = await _likeService.GetAllIncludingBlogLikesPeopleByBlogIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> SavedCompanyUserList(int? id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedCompaniesPeopleByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedBlogUserList(int? id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedBlogsPeopleByBlogIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedProductUserList(int? id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedProductsPeopleByProductIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> SavedPartnershipUserList(int? id, int page = 1)
        {
            var result = await _savedContentService.GetAllIncludingSavedPartnershipPeopleByCompanyPartnershipIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourCompanyHitList(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingCompanyHitForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourProductHitList(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingProductHitForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourBlogHitList(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingBlogHitForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> YourPartnershipHitList(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingPartnershipHitsforCompanyByUserId(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> HitPartnershipUserList(int id, int page = 1)
        {
            var result = await _hitService.GetAllIncludingPartnershipHitsPeopleByCompanyPartnershipIdAsync(id);
            return View(result.ToPagedList(page, 60));
        }
        public async Task<IActionResult> HitCompanyUserList(int id, int page = 1)
        {
            var result = await _hitService.GetAllIncludingCompanyHitsPeopleByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 60));
        }
        public async Task<IActionResult> HitProductUserList(int id, int page = 1)
        {
            var result = await _hitService.GetAllIncludingProductHitsPeopleByProductIdAsync(id);
            return View(result.ToPagedList(page, 60));
        }
        public async Task<IActionResult> HitBlogUserList(int id, int page = 1)
        {
            var result = await _hitService.GetAllIncludingBlogHitsPeopleByBlogIdAsync(id);
            return View(result.ToPagedList(page, 60));
        }
        public async Task<IActionResult> YourCompanyAppointmentList(string id, int page = 1)
        {
            var result = await _appointmentService.GetAllIncludingCompanyAppointmentForCompanyUserByUserIdAsync(id);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> AppointmentAnswerList(int? id)
        {
            return View(await _appointmentAnswerService.GetAllIncludingByAppointmentIdAsync(id));
        }
        public async Task<IActionResult> CustomerList(int? id, int page = 1)
        {
            var result = await _customerService.GetAllIncludingForCompanyUserByCompanyIdAsync(id);
            return View(result.ToPagedList(page, 48));
        }
        public async Task<IActionResult> MostLikedCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingMostLikedCompaniesAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostHitCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingMostHitCompaniesAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostSavedCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingMostSavedCompaniesAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostAppointmentCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingMostAppointmentCompaniesAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostCommentCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingMostCommentCompaniesAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostLikedProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostLikedProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostHitProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostHitProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostSavedProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostSavedProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostQuestionProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostQuestionProductsAsync();
            return View(result.ToPagedList());
        }
        public async Task<IActionResult> MostCommentProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostCommentProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostExpensiveProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostExpensiveProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostCheapperProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingMostCheapperProductsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostLikedBlogs(int page = 1)
        {
            var result = await _blogService.GetAllIncludingMostLikedBlogsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostHitBlogs(int page = 1)
        {
            var result = await _blogService.GetAllIncludingMostHitBlogsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostSavedBlogs(int page = 1)
        {
            var result = await _blogService.GetAllIncludingMostSavedBlogsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostCommentBlogs(int page = 1)
        {
            var result = await _blogService.GetAllIncludingMostCommentBlogsAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostLikedPartnership(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingMostLikedPartnershipAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostHitPartnership(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingMostHitPartnershipAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostSavedPartnership(int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingMostSavedPartnershipAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostLikedCompanyForm(int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingMostLikedCompanyFormAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> MostHitCompanyForm(int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingMostHitCompanyFormAsync();
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostLikedCompanies(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingYourMostLikedCompaniesAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostHitCompanies(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingYourMostHitCompaniesAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostSavedCompanies(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingYourMostSavedCompaniesAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostAppointmentCompanies(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingYourMostAppointmentCompaniesAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostCommentCompanies(string id, int page = 1)
        {
            var result = await _companyService.GetAllIncludingYourMostCommentCompaniesAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostLikedProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostLikedProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostHitProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostHitProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostSavedProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostSavedProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostQuestionProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostQuestionProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostCommentProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostCommentProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostExpensiveProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostExpensiveProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostCheapperProducts(string id, int page = 1)
        {
            var result = await _productService.GetAllIncludingYourMostCheapperProductsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostLikedBlogs(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingYourMostLikedBlogsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostHitBlogs(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingYourMostHitBlogsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostSavedBlogs(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingYourMostSavedBlogsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostCommentBlogs(string id, int page = 1)
        {
            var result = await _blogService.GetAllIncludingYourMostCommentBlogsAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostLikedPartnership(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingYourMostLikedPartnershipByUserIdAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostHitPartnership(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingYourMostHitPartnershipByUserIdAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostSavedPartnership(string id, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingYourMostSavedPartnershipByUserIdAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostLikedCompanyForm(string id, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingYourMostLikedCompanyFormByUserIdAsync(id);
            return View(result.ToPagedList(page, 35));
        }
        public async Task<IActionResult> YourMostHitCompanyForm(string id, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingYourMostHitCompanyFormByUserIdAsync(id);
            return View(result.ToPagedList(page, 35));
        }
    }
}