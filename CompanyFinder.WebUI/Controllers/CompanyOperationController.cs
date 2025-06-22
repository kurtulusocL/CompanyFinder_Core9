using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Entities.Entities;
using CompanyFinder.Entities.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyFinder.WebUI.Controllers
{
    [AuditLog]
    [ExceptionHandler]
    [Authorize(Roles = "Companies")]
    public class CompanyOperationController : Controller
    {
        readonly ICompanyService _companyService;
        readonly ICompanyCategoryService _companyCategoryService;
        readonly ICompanySubcategoryService _companySubcategoryService;
        readonly ICompanyContactService _companyContactService;
        readonly IBlogService _blogService;
        readonly IBlogCategoryService _blogCategoryService;
        readonly IProductService _productService;
        readonly IProductCategoryService _productCategoryService;
        readonly IProductSubcategoryService _productSubcategoryService;
        readonly IStockService _stockService;
        readonly ICityService _cityService;
        readonly ICountryService _countryService;
        readonly IPictureService _pictureService;
        readonly IFeedbackService _feedbackService;
        readonly ICancelMembershipService _cancelMembershipService;
        readonly ICancelMembershipReasonService _cancelMembershipReasonService;
        readonly IUserService _userService;
        readonly ICommentService _commentService;
        readonly ICommentAnswerService _commentAnswerService;
        readonly IQuestionService _questionService;
        readonly IQuestionAnswerService _questionAnswerService;
        readonly IReportAnswerService _reportAnswerService;
        readonly ISavedContentService _savedContentService;
        readonly IHitService _hitService;
        readonly ILikeService _likeService;
        readonly IAppointmentService _appointmentService;
        readonly IAppointmentAnswerService _appointmentAnswerService;
        readonly ICustomerService _customerService;
        readonly ICustomerStatusService _customerStatusService;
        readonly IReportService _reportService;
        readonly IReportCategoryService _reportCategoryService;
        readonly ICompanyPartnershipService _companyPartnershipService;
        readonly ICompanyFormAnswerService _companyFormAnswerService;
        readonly ICompanyFormService _companyFormService;
        readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyOperationController(ICompanyService companyService, ICompanyCategoryService companyCategoryService, ICompanySubcategoryService companySubcategoryService, ICompanyContactService companyContactService, IBlogService blogService, IBlogCategoryService blogCategoryService, IProductService productService, IProductCategoryService productCategoryService, IProductSubcategoryService productSubcategoryService, IStockService stockService, ICityService cityService, ICountryService countryService, IPictureService pictureService, IFeedbackService feedbackService, ICancelMembershipService cancelMembershipService, ICancelMembershipReasonService cancelMembershipReasonService, IUserService userService, ICommentService commentService, ICommentAnswerService commentAnswerService, IQuestionService questionService, IQuestionAnswerService questionAnswerService, IReportAnswerService reportAnswerService, ISavedContentService savedContentService, IHitService hitService, ILikeService likeService, IAppointmentService appointmentService, IAppointmentAnswerService appointmentAnswerService, ICustomerService customerService, ICustomerStatusService customerStatusService, IReportService reportService, IReportCategoryService reportCategoryService, ICompanyPartnershipService companyPartnershipService, ICompanyFormAnswerService companyFormAnswerService,   IHttpContextAccessor httpContextAccessor, ICompanyFormService companyFormService)
        {
            _companyService = companyService;
            _companyCategoryService = companyCategoryService;
            _companySubcategoryService = companySubcategoryService;
            _companyContactService = companyContactService;
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productSubcategoryService = productSubcategoryService;
            _stockService = stockService;
            _cityService = cityService;
            _countryService = countryService;
            _pictureService = pictureService;
            _feedbackService = feedbackService;
            _cancelMembershipService = cancelMembershipService;
            _cancelMembershipReasonService = cancelMembershipReasonService;
            _userService = userService;
            _commentService = commentService;
            _commentAnswerService = commentAnswerService;
            _questionService = questionService;
            _questionAnswerService = questionAnswerService;
            _reportAnswerService = reportAnswerService;
            _savedContentService = savedContentService;
            _hitService = hitService;
            _likeService = likeService;
            _appointmentService = appointmentService;
            _appointmentAnswerService = appointmentAnswerService;
            _customerService = customerService;
            _customerStatusService = customerStatusService;
            _reportService = reportService;
            _reportCategoryService = reportCategoryService;
            _companyPartnershipService = companyPartnershipService;
            _companyFormAnswerService = companyFormAnswerService;
            _httpContextAccessor = httpContextAccessor;
            _companyFormService = companyFormService;
        }
        public async Task<IActionResult> CompanyCreate(string id)
        {
            ViewBag.Cities = await _cityService.GetAllIncludingCompanyCitiesForAdd();
            ViewBag.Subcategories = await _companySubcategoryService.GetAllIncludingCompanySubcategoriesForAdd();
            ViewBag.UserId = await _userService.GetByIdAsync(id);
            Company model = new Company
            {
                UserId = id
            };
            return View("CompanyCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyCreate(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, IFormFile image)
        {
            var result = await _companyService.CreateAsync(name, desc, foundationDate, slogan, websiteUrl, isCommentable, countryId, cityId, companyCategoryId, companySubcategoryId, userId, image);
            if (result)
            {
                TempData["companyCreatedSuccessfull"] = "Firmanız başarıyla eklenmiştir.";
                return RedirectToAction(nameof(CompanyCreate));
            }
            TempData["companyCreateError"] = "Firmanızı eklerken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyCreate));
        }

        public async Task<IActionResult> CompanyUpdate(int? id)
        {
            ViewBag.Cities = await _cityService.GetAllIncludingCompanyCitiesForAdd();
            ViewBag.Subcategories = await _companySubcategoryService.GetAllIncludingCompanySubcategoriesForAdd();
            var data = await _companyService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyUpdate(string name, string desc, DateTime foundationDate, string? slogan, string? websiteUrl, bool isCommentable, int countryId, int? cityId, int companyCategoryId, int? companySubcategoryId, string userId, int id, IFormFile image)
        {
            var result = await _companyService.UpdateAsync(name, desc, foundationDate, slogan, websiteUrl, isCommentable, countryId, cityId, companyCategoryId, companySubcategoryId, userId, id, image);
            if (result)
            {
                TempData["companyUpdatedSuccessfull"] = "Firmanız başarıyla güncellenmiştir.";

                return RedirectToAction(nameof(CompanyUpdate), new { id = id });
            }
            TempData["companyUpdateError"] = "Firmanızı güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyUpdate), new { id = id });
        }

        public async Task<IActionResult> CompanyImageCreate(int? id)
        {
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Picture model = new Picture
            {
                CompanyId = id
            };
            return View("CompanyImageCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyImageCreate(int? companyId, IEnumerable<IFormFile> images)
        {
            var result = await _pictureService.CreateCompanyImagesAsync(companyId, images);
            if (result)
            {
                TempData["companyImageCreatedSuccessfull"] = "Firma resimleriniz başarıyla eklenmiştir.";
                return RedirectToAction(nameof(CompanyImageCreate), new { id = companyId });
            }
            TempData["companyImageCreateError"] = "Firma resimleriniz eklerken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyImageCreate), new { id = companyId });
        }

        public async Task<IActionResult> CompanyImageUpdate(int? id)
        {
            var data = await _pictureService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyImageUpdate(int? companyId, int id, IFormFile image)
        {
            var result = await _pictureService.UpdateCompanyImageAsync(companyId, id, image);
            if (result)
            {
                TempData["companyImageUpdatedSuccessfull"] = "Firma resminiz başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(CompanyImageUpdate), new { id = id });
            }
            TempData["companyImageUpdateError"] = "Firma resminiz güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyImageUpdate), new { id = id });
        }

        public async Task<IActionResult> CompanyContactCreate(int? id)
        {
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            CompanyContact model = new CompanyContact
            {
                CompanyId = id
            };
            return View("CompanyContactCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyContactCreate(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId)
        {
            var result = await _companyContactService.CreateAsync(emailAddress, phoneNumber, whatsappNo, address, isHide, companyId);
            if (result)
            {
                TempData["companyContactCreatedSuccessfull"] = "Firma adres bilgisi başarıyla eklenmiştir.";
                return RedirectToAction(nameof(CompanyContactCreate), new { id = companyId });
            }
            TempData["companyContactCreateError"] = "Firma adres bilgisi eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyContactCreate), new { id = companyId });
        }

        public async Task<IActionResult> CompanyContactUpdate(int? id)
        {
            var data = await _companyContactService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyContactUpdate(string? emailAddress, string? phoneNumber, string? whatsappNo, string? address, bool isHide, int? companyId, int id)
        {
            var result = await _companyContactService.UpdateAsync(emailAddress, phoneNumber, whatsappNo, address, isHide, companyId, id);
            if (result)
            {
                TempData["companyContactUpdatedSuccessfull"] = "Firma adres bilgisi başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(CompanyContactUpdate), new { id = id });
            }
            TempData["companyContactUpdateError"] = "Firma adres bilgisi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyContactUpdate), new { id = id });
        }

        public async Task<IActionResult> CustomerCreate(int? id)
        {
            ViewBag.CustomerStatus = await _customerStatusService.GetAllIncludingForAddCustomerAsync();
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Customer model = new Customer
            {
                CompanyId = id
            };
            return View("CustomerCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerCreate(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId)
        {
            var result = await _customerService.CreateAsync(nameSurname, customerCompany, emailAddress, phoneNumber, location, desc, companyId, customerStatusId);
            if (result)
            {
                TempData["customerCreatedSuccessfull"] = "Müşteri bilgisi eklendi.";
                return RedirectToAction(nameof(CustomerCreate), new { id = companyId });
            }
            TempData["customerCreateError"] = "Müşteri bilgisi eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CustomerCreate), new { id = companyId });
        }

        public async Task<IActionResult> CustomerUpdate(int? id)
        {
            ViewBag.CustomerStatus = await _customerStatusService.GetAllIncludingForAddCustomerAsync();
            var data = await _customerService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerUpdate(string nameSurname, string customerCompany, string emailAddress, string? phoneNumber, string location, string? desc, int? companyId, int customerStatusId, int id)
        {
            var result = await _customerService.UpdateAsync(nameSurname, customerCompany, emailAddress, phoneNumber, location, desc, companyId, customerStatusId, id);
            if (result)
            {
                TempData["customerUpdatedSuccessfull"] = "Müşteri bilgisi güncellendi.";
                return RedirectToAction(nameof(CustomerCreate), new { id = companyId });
            }
            TempData["customerUpdateError"] = "Müşteri bilgisi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CustomerCreate), new { id = companyId });
        }

        public async Task<IActionResult> CompanyPartnershipCreate(int? id)
        {
            ViewBag.Categories = await _productCategoryService.GetAllIncludingForAddCompanyPartnershipAsync();
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            CompanyPartnership model = new CompanyPartnership
            {
                CompanyId = id
            };
            return View("CompanyPartnershipCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyPartnershipCreate(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, int? companyId, int productCategoryId, string userId)
        {
            var result = await _companyPartnershipService.CreateAsync(title, detail, desc, price, startDate, expiryDate, companyId, productCategoryId, userId);
            if (result)
            {
                TempData["partnershipCreatedSuccessfull"] = "İş talebi başarıyla eklendi.";
                return RedirectToAction(nameof(CompanyPartnershipCreate), new { id = companyId });
            }
            TempData["partnershipCreateError"] = "İş talebi eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurup tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyPartnershipCreate), new { id = companyId });
        }
        public async Task<IActionResult> CompanyPartnershipUpdate(int? id)
        {
            ViewBag.Categories = await _productCategoryService.GetAllIncludingForAddCompanyPartnershipAsync();
            var data = await _companyPartnershipService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyPartnershipUpdate(string title, string? detail, string desc, decimal? price, DateTime startDate, DateTime expiryDate, bool isAvailable, int? companyId, int productCategoryId, string userId, int id)
        {
            var result = await _companyPartnershipService.UpdateAsync(title, detail, desc, price, startDate, expiryDate, isAvailable, companyId, productCategoryId, userId, id);
            if (result)
            {
                TempData["partnershipUpdatedSuccessfull"] = "İş talebi başarıyla güncellendi.";
                return RedirectToAction(nameof(CompanyPartnershipUpdate), new { id = id });
            }
            TempData["partnershipUpdateError"] = "İş talebi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurup tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CompanyPartnershipUpdate), new { id = id });
        }

        public async Task<IActionResult> BlogCreate(int? id)
        {
            ViewBag.BlogCategories = await _blogCategoryService.GetAllIncludingForAddBlogAsync();
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Blog model = new Blog
            {
                CompanyId = id
            };
            return View("BlogCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogCreate(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, IFormFile image)
        {
            var result = await _blogService.CreateAsync(title, subtitle, desc, blogCategoryId, companyId, userId, image);
            if (result)
            {
                TempData["companyBlogCreatedSuccessfull"] = "Firma blog makalesi başarıyla eklenmiştir.";
                return RedirectToAction(nameof(BlogCreate), new { id = companyId });
            }
            TempData["companyBlogCreateError"] = "Firma blog makalesi eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(BlogCreate), new { id = companyId });
        }

        public async Task<IActionResult> BlogUpdate(int? id)
        {
            ViewBag.BlogCategories = await _blogCategoryService.GetAllIncludingForAddBlogAsync();
            var data = await _blogService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogUpdate(string title, string? subtitle, string desc, int blogCategoryId, int companyId, string userId, int id, IFormFile image)
        {
            var result = await _blogService.UpdateAsync(title, subtitle, desc, blogCategoryId, companyId, userId, id, image);
            if (result)
            {
                TempData["companyBlogUpdatedSuccessfull"] = "Firma blog makalesi başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(BlogUpdate), new { id = id });
            }
            TempData["companyBlogUpdateError"] = "Firma blog makalesi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(BlogUpdate), new { id = id });
        }

        public async Task<IActionResult> BlogImageCreate(int? id)
        {
            ViewBag.BlogId = await _blogService.GetByIdAsync(id);
            Picture model = new Picture
            {
                BlogId = id
            };
            return View("BlogImageCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogImageCreate(int? blogId, IEnumerable<IFormFile> images)
        {
            var result = await _pictureService.CreateBlogImagesAsync(blogId, images);
            if (result)
            {
                TempData["companyBlogImageCreatedSuccessfull"] = "Firma blog resimleriniz başarıyla eklenmiştir.";
                return RedirectToAction(nameof(BlogImageCreate), new { id = blogId });
            }
            TempData["companyBlogImageCreateError"] = "Firma blog resimleriniz eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(BlogImageCreate), new { id = blogId });
        }

        public async Task<IActionResult> BlogImageUpdate(int? id)
        {
            var data = await _pictureService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogImageUpdate(int? blogId, int id, IFormFile image)
        {
            var result = await _pictureService.UpdateBlogImageAsync(blogId, id, image);
            if (result)
            {
                TempData["companyBlogImageUpdatedSuccessfull"] = "Firma blog resminiz başarıyla güncelenmiştir.";
                return RedirectToAction(nameof(BlogImageUpdate), new { id = id });
            }
            TempData["companyBlogImageUpdateError"] = "Firma blog resminiz güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(BlogImageUpdate), new { id = id });
        }

        public async Task<IActionResult> ProductCreate(int? id)
        {
            ViewBag.CompanyId = await _companyService.GetByIdAsync(id);
            Product model = new Product
            {
                CompanyId = id
            };
            return View("ProductCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(string name, string desc, string? otherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, IFormFile image)
        {
            var result = await _productService.CreateAsync(name, desc, otherText, price, isCommentable, isAvailable, isQuestionable, AvailableDate, productCategoryId, productSubcategoryId, companyId, userId, image);
            if (result)
            {
                TempData["companyProductCreatedSuccessfull"] = "Firmanızın ürünü başarıyla eklenmiştir.";
                return RedirectToAction(nameof(ProductCreate), new { id = companyId });
            }
            TempData["companyProductCreateError"] = "Firmanızın ürünü eklerken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(ProductCreate), new { id = companyId });
        }

        public async Task<IActionResult> ProductUpdate(int? id)
        {
            var data = await _productService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductUpdate(string name, string desc, string? otherText, decimal? price, bool isCommentable, bool isAvailable, bool isQuestionable, DateTime? AvailableDate, int productCategoryId, int? productSubcategoryId, int companyId, string userId, int id, IFormFile image)
        {
            var result = await _productService.UpdateAsync(name, desc, otherText, price, isCommentable, isAvailable, isQuestionable, AvailableDate, productCategoryId, productSubcategoryId, companyId, userId, id, image);
            if (result)
            {
                TempData["companyProductUpdatedSuccessfull"] = "Firmanızın ürünü başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(ProductUpdate), new { id = id });
            }
            TempData["companyProductUpdateError"] = "Firmanızın ürünü güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(ProductUpdate), new { id = id });
        }

        public async Task<IActionResult> StockCreate(int? id)
        {
            ViewBag.ProductId = await _productService.GetByIdAsync(id);
            Stock model = new Stock
            {
                ProductId = id
            };
            return View("StockCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockCreate(int quantity, string? warehouse, int? productId)
        {
            var result = await _stockService.CreateAsync(quantity, warehouse, productId);
            if (result)
            {
                TempData["stockCreated"] = "Ürün stok bilgisi başarı ile kaydedildi.";
                return RedirectToAction(nameof(StockCreate), new { id = productId });
            }
            TempData["stockCreateError"] = "Ürün stok bilgisi kaydedilirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun. Zorunlu alanları doldurduktan sonra tekrar deeyiniz.";
            return RedirectToAction(nameof(StockCreate), new { id = productId });
        }

        public async Task<IActionResult> StockUpdate(int? id)
        {
            var data = await _stockService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StockUpdate(int quantity, string? warehouse, int? productId, int id)
        {
            var result = await _stockService.UpdateAsync(quantity, warehouse, productId, id);
            if (result)
            {
                TempData["stockUpdated"] = "Ürün stok bilgisi başarı ile güncellendi.";
                return RedirectToAction(nameof(StockUpdate), new { id = id });
            }
            TempData["stockUpdateError"] = "Ürün stok bilgisi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun. Zorunlu alanları doldurduktan sonra tekrar deeyiniz.";
            return RedirectToAction(nameof(StockUpdate), new { id = id });
        }

        public async Task<IActionResult> ProductImageCreate(int? id)
        {
            ViewBag.ProductId = await _productService.GetByIdAsync(id);
            Picture model = new Picture
            {
                ProductId = id
            };
            return View("ProductImageCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductImageCreate(int? productId, IEnumerable<IFormFile> images)
        {
            var result = await _pictureService.CreateProductImagesAsync(productId, images);
            if (result)
            {
                TempData["companyProductImageCreatedSuccessfull"] = "Firmanızın ürün resmleri başarıyla eklenmiştir.";
                return RedirectToAction(nameof(ProductImageCreate), new { id = productId });
            }
            TempData["companyProductImageCreateError"] = "Firmanızın ürün resimleri eklenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(ProductImageCreate), new { id = productId });
        }

        public async Task<IActionResult> ProductImageUpdate(int? id)
        {
            var data = await _pictureService.GetByIdAsync(id);
            if (data != null)
            {
                return View(data);
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductImageUpdate(int? productId, int id, IFormFile image)
        {
            var result = await _pictureService.UpdateProductImageAsync(productId, id, image);
            if (result)
            {
                TempData["companyProductImageUpdatedSuccessfull"] = "Firmanızın ürün resmi başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(ProductImageUpdate), new { id = id });
            }
            TempData["companyProductImageUpdateError"] = "Firmanızın ürün resimi güncellenirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(ProductImageUpdate), new { id = id });
        }

        public async Task<IActionResult> SendCancelRequest(string id)
        {
            ViewBag.CancelRequestReasons = await _cancelMembershipReasonService.GetAllIncludingForAddAsync();
            ViewBag.UserId = await _userService.GetByIdAsync(id);
            CancelMembership model = new CancelMembership
            {
                UserId = id
            };
            return View("SendCancelRequest", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCancelRequest(string title, string? otherReason, DateTime expectedCancelDate, int cancelMembershipReasonId, string userId)
        {
            var result = await _cancelMembershipService.CreateAsync(title, otherReason, expectedCancelDate, cancelMembershipReasonId, userId);
            if (result)
            {
                TempData["cancelRequestSentSuccessfull"] = "Üyelik iptal talebiniz başarı ile gönderilmiştir. En kısa sürede üyeliğinizi iptal edeceğiz. Bu sırada dilediğiniz zaman iptal talebinden vazgeçebilirsiniz.";
                return RedirectToAction(nameof(SendCancelRequest), new { id = userId });
            }
            TempData["cancelRequestSentError"] = "Üyelik iptal talebiniz gönderilirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(SendCancelRequest), new { id = userId });
        }

        public async Task<IActionResult> SendFeedback(string id)
        {
            ViewBag.UserId = await _companyService.GetCompanyByUserIdAsync(id);
            Feedback model = new Feedback
            {
                UserId = id
            };
            return View("SendFeedback", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendFeedback(string title, string subject, string desc, string userId)
        {
            var result = await _feedbackService.CreateAsync(title, subject, desc, userId);
            if (result)
            {
                TempData["feedbackSentSuccessfull"] = "Geri bildiriminiz başarı ile gönderilmiştir. Geri bildirimizi değerlendirip en kısa sürede size dönüş yapacağız. Geri bildiriminiz için teşekkür ederiz.";
                return RedirectToAction(nameof(SendFeedback), new { id = userId });
            }
            TempData["feedbackSentError"] = "Geri bildiriminiz gönderilirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(SendFeedback), new { id = userId });
        }

        public async Task<IActionResult> CommentAnswerCreate(int? id)
        {
            ViewBag.CommentId = await _commentService.GetByIdAsync(id);
            CommentAnswer model = new CommentAnswer
            {
                CommentId = id
            };
            return View("CommentAnswerCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentAnswerCreate(string? title, string text, int? commentId, string userId)
        {
            var result = await _commentAnswerService.CreateAsync(title, text, commentId, userId);
            if (result)
            {
                TempData["commentAnswerCreatedSuccessfull"] = "Yorum cevabınız başarılı bir şekilde gönderildi.";
                return RedirectToAction(nameof(CommentAnswerCreate), new { id = commentId });
            }
            TempData["commentAnswerCreateError"] = "Yorum cevabınız gönderilirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(CommentAnswerCreate), new { id = commentId });
        }

        public async Task<IActionResult> QuestionAnswerCreate(int? id)
        {
            ViewBag.QuestionId = await _questionService.GetByIdAsync(id);
            QuestionAnswer model = new QuestionAnswer
            {
                QuestionId = id
            };
            return View("QuestionAnswerCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuestionAnswerCreate(string? title, string answer, int? questionId, string userId)
        {
            var result = await _questionAnswerService.CreateAsync(title, answer, questionId, userId);
            if (result)
            {
                TempData["questionAnswerCreatedSuccessfull"] = "Cevabınız başarılı bir şekilde gönderildi.";
                return RedirectToAction(nameof(QuestionAnswerCreate), new { id = questionId });
            }
            TempData["questionAnswerCreateError"] = "Cevabınız gönderilirken bir hata meydana geldi. Lütfen zorunlu alanları doldurduğunuzdan emin olun ve tekrar deneyiniz. Hatanın devam etmesi durumunda mail adreslerimizden ya da geri bildirim kısmından bize ulaşmaktan lütfen çekinmeyiniz.";
            return RedirectToAction(nameof(QuestionAnswerCreate), new { id = questionId });
        }

        public async Task<IActionResult> AppointmentAnswerCreate(int? id)
        {
            ViewBag.AppointmentId = await _appointmentService.GetByIdAsync(id);
            AppointmentAnswer model = new AppointmentAnswer
            {
                AppointmentId = id
            };
            return View("AppointmentAnswerCreate", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AppointmentAnswerCreate(string title, string desc, DateTime reAppointmentDate, int? appointmentId)
        {
            var result = await _appointmentAnswerService.CreateAsync(title, desc, reAppointmentDate, appointmentId);
            if (result)
            {
                TempData["appointmentAnswerCreatedSuccesssfull"] = "Yeni Randevu Talebi Başarıyla Oluşturuldu.";
                return RedirectToAction(nameof(AppointmentAnswerCreate), new { id = appointmentId });
            }
            TempData["appointmentAnswerCreateError"] = "Yeni Randevu Talebi Başarıyla Oluşturuldu.";
            return RedirectToAction(nameof(AppointmentAnswerCreate), new { id = appointmentId });
        }

        public async Task<IActionResult> SendCommentReport(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.CommentId = await _commentService.GetByIdAsync(id);
            Report model = new Report
            {
                CommentId = id
            };
            return View("SendCommentReport", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCommentReport(string title, string desc, int reportCategoryId, int? commentId, string userId)
        {
            var result = await _reportService.CreateCommentReportAsync(title, desc, reportCategoryId, commentId, userId);
            if (result)
            {
                TempData["sendCommentReportSuccessfull"] = "Bildiriminiz başarıyla gönderildi. En kısa sürede size geri dönüş yapacağız. Teşekkür ederiz.";
                return RedirectToAction(nameof(SendCommentReport), new { id = commentId });
            }
            TempData["sendCommentReportError"] = "Bildriminiz gönderilirlen bir hata meydana geldi. Lütfen zorunlu alanları doldurduktan sonra tekrar deneyiniz. Eğer hala hata almaya devam ederseniz geri bildirim alanından ya da iletişim sayfamızdali email adresimizden bizimle iletişim kurmaktan çekinmeyiniz. Yaşadığınız sorun için özür diler sorunu en kısa sürede çözeceğimizi bildirmek isteriz.";
            return RedirectToAction(nameof(SendCommentReport), new { id = commentId });
        }

        public async Task<IActionResult> SendCommentAnswerReport(int? id)
        {
            ViewBag.ReportCategories = await _reportCategoryService.GetAllIncludingForAddReportAsync();
            ViewBag.CommentAnswerId = await _commentAnswerService.GetByIdAsync(id);
            Report model = new Report
            {
                CommentAnswerId = id
            };
            return View("SendCommentAnswerReport", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCommentAnswerReport(string title, string desc, int reportCategoryId, int? commentAnswerId, string userId)
        {
            var result = await _reportService.CreateCommentAnswerReportAsync(title, desc, reportCategoryId, commentAnswerId, userId);
            if (result)
            {
                TempData["sendCommentAnswerReportSuccessfull"] = "Bildiriminiz başarıyla gönderildi. En kısa sürede size geri dönüş yapacağız. Teşekkür ederiz.";
                return RedirectToAction(nameof(SendCommentReport), new { id = commentAnswerId });
            }
            TempData["sendCommentAnswerReportError"] = "Bildriminiz gönderilirlen bir hata meydana geldi. Lütfen zorunlu alanları doldurduktan sonra tekrar deneyiniz. Eğer hala hata almaya devam ederseniz geri bildirim alanından ya da iletişim sayfamızdali email adresimizden bizimle iletişim kurmaktan çekinmeyiniz. Yaşadığınız sorun için özür diler sorunu en kısa sürede çözeceğimizi bildirmek isteriz.";
            return RedirectToAction(nameof(SendCommentReport), new { id = commentAnswerId });
        }

        public async Task<IActionResult> SendCompanyFormAnswerReport(int? id)
        {
            ViewBag.CompanyFormAnswerId=await _companyFormAnswerService.GetByIdAsync(id);
            Report model = new Report
            {
                CompanyFormAnswerId = id
            };
            return View("SendCompanyFormAnswerReport", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCompanyFormAnswerReport(string title, string desc, int reportCategoryId, int? companyFormAnswerId, string userId)
        {
            var result = await _reportService.CreateCompanyFormAnswerReportAsync(title, desc, reportCategoryId, companyFormAnswerId, userId);
            if (result)
            {
                TempData["sendCompanyFormAnswerReportSuccessfull"] = "Bildiriminiz başarıyla gönderildi. En kısa sürede size geri dönüş yapacağız. Teşekkür ederiz.";
                return RedirectToAction(nameof(SendCompanyFormAnswerReport), new { id = companyFormAnswerId });
            }
            TempData["sendCompanyFormAnswerReportError"] = "Bildriminiz gönderilirlen bir hata meydana geldi. Lütfen zorunlu alanları doldurduktan sonra tekrar deneyiniz. Eğer hala hata almaya devam ederseniz geri bildirim alanından ya da iletişim sayfamızdali email adresimizden bizimle iletişim kurmaktan çekinmeyiniz. Yaşadığınız sorun için özür diler sorunu en kısa sürede çözeceğimizi bildirmek isteriz.";
            return RedirectToAction(nameof(SendCompanyFormAnswerReport), new { id = companyFormAnswerId });
        }


        [HttpPost]
        public async Task<JsonResult> CompanyCategorySelectSystem(int? categoryId, string tip)
        {
            try
            {
                var result = await _companyService.CategorySelectSystem(categoryId, tip);
                return Json(new { ok = true, text = result });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    text = new List<SelectListItem>
                {
                    new SelectListItem { Text = $"Error: {ex.Message}", Value = "" }
                }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> CompanyLocationSelectSystem(int? countryId, string tip)
        {
            try
            {
                var result = await _companyService.LocationSelectSystem(countryId, tip);
                return Json(new { ok = true, text = result });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    text = new List<SelectListItem>
                {
                    new SelectListItem { Text = $"Error: {ex.Message}", Value = "" }
                }
                });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ProductCategorySelectSystem(int? productCategoryId, string tip)
        {
            try
            {
                var result = await _productService.ProductSelectSystem(productCategoryId, tip);
                return Json(new { ok = true, text = result });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    ok = false,
                    text = new List<SelectListItem>
                {
                    new SelectListItem { Text = $"Error: {ex.Message}", Value = "" }
                }
                });
            }
        }
        public async Task<IActionResult> SetHideContactInfo(int id)
        {
            var result = await _companyContactService.SetHideAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyContactList", "HomeCompany", new { id = id });
            }
            return RedirectToAction("CompanyContactList", "HomeCompany", new { id = id });
        }
        public async Task<IActionResult> SetNotHideContactInfo(int id)
        {
            var result = await _companyContactService.SetNotHideAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyContactList", "HomeCompany", new { id = id });
            }
            return RedirectToAction("CompanyContactList", "HomeCompany", new { id = id });
        }
        public async Task<IActionResult> SetAnswerableCompanyForm(int id, CompanyForm model)
        {
            var result = await _companyFormService.SetAnswerable(id);
            if (result)
            {
                return RedirectToAction("YourCompanyForumList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("YourCompanyForumList", "HomeCompany", new { id = model.Id });
        }
        public async Task<IActionResult> SetNotAnswerableCompanyForm(int id, CompanyForm model)
        {
            var result = await _companyFormService.SetNotAnswerable(id);
            if (result)
            {
                return RedirectToAction("YourCompanyForumList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("YourCompanyForumList", "HomeCompany", new { id = model.Id });
        }

        public async Task<IActionResult> SetNotApprovedAppointment(int id, Appointment model)
        {
            var result = await _appointmentService.SetNotApprovedAsync(id);
            if (result)
            {
                return RedirectToAction("YourCompanyAppointmentList", "HomeCompany", new { model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetApprovedAppointment(int id, Appointment model)
        {
            var result = await _appointmentService.SetApprovedAsync(id);
            if (result)
            {
                return RedirectToAction("YourCompanyAppointmentList", "HomeCompany", new { model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetNotApprovedAppointmentAnswer(int id, AppointmentAnswer model)
        {
            var result = await _appointmentAnswerService.SetNotApprovedAsync(id);
            if (result)
            {
                return RedirectToAction("AppointmentAnswerList", "HomeCompany", new { model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetApprovedAppointmentAnswer(int id, AppointmentAnswer model)
        {
            var result = await _appointmentAnswerService.SetApprovedAsync(id);
            if (result)
            {
                return RedirectToAction("AppointmentAnswerList", "HomeCompany", new { model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetNotSavedBlogContent(bool isSaved, int? companyId, int? productId, int? id, int? companyPartnershipId, string userId)
        {
            var result = await _savedContentService.NotSaveAsync(isSaved, companyId, productId, id, companyPartnershipId, userId);
            if (result)
            {
                return RedirectToAction("SavedBlogList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetNotSavedCompanyContent(bool isSaved, int? id, int? productId, int? blogId, int? companyPartnershipId, string userId)
        {
            var result = await _savedContentService.NotSaveAsync(isSaved, id, productId, blogId, companyPartnershipId, userId);
            if (result)
            {
                return RedirectToAction("SavedCompanyList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetNotSavedProductContent(bool isSaved, int? companyId, int? id, int? blogId, int? companyPartnershipId, string userId)
        {
            var result = await _savedContentService.NotSaveAsync(isSaved, companyId, id, blogId, companyPartnershipId, userId);
            if (result)
            {
                return RedirectToAction("SavedProductList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetNotSavedCompanyPartnershipContent(bool isSaved, int? companyId, int? productId, int? blogId, int? id, string userId)
        {
            var result = await _savedContentService.NotSaveAsync(isSaved, companyId, productId, blogId, id, userId);
            if (result)
            {
                return RedirectToAction("SavedPartnershipList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetCompanyPartnershipAvailable(int id, CompanyPartnership model)
        {
            var result = await _companyPartnershipService.SetAvailablePartnership(id);
            if (result)
            {
                return RedirectToAction("PartnershipList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetCompanyPartnershipNotAvailable(int id, CompanyPartnership model)
        {
            var result = await _companyPartnershipService.SetNotAvailablePartnership(id);
            if (result)
            {
                return RedirectToAction("PartnershipList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetRequestCancel(int id, CancelMembership model)
        {
            var result = await _cancelMembershipService.SetCancelledAsync(id);
            if (result)
            {
                return RedirectToAction("CancelRequestList", "HomeCompany", new { id = model.UserId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetReportAnswerSatisfield(int id, Report model)
        {
            var result = await _reportAnswerService.SetSatisfieldAsync(id);
            if (result)
            {
                return RedirectToAction("ReportList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetCompanyCommentable(int id, Company model)
        {
            var result = await _companyService.SetCommentableAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetCompanyNotCommentable(int id, Company model)
        {
            var result = await _companyService.SetNotCommentableAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductCommentable(int id, Product model)
        {
            var result = await _productService.SetCommentableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductNotCommentable(int id, Product model)
        {
            var result = await _productService.SetNotCommentableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductQuestionable(int id, Product model)
        {
            var result = await _productService.SetQuestionableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductNotQuestionable(int id, Product model)
        {
            var result = await _productService.SetNotQuestionableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductAvailable(int id, Product model)
        {
            var result = await _productService.SetAvailableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> SetProductNotAvailable(int id, Product model)
        {
            var result = await _productService.SetNotAvailableAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompany(int id, User model)
        {
            var result = await _companyService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompanyContact(int id, CompanyContact model)
        {
            var result = await _companyContactService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyContactList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteBlog(int id, Blog model)
        {
            var result = await _blogService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("BlogList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteProduct(int id, Product model)
        {
            var result = await _productService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("ProductList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteProductImage(int id, Picture model)
        {
            var result = await _pictureService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("ProductImageList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteBlogImage(int id, Picture model)
        {
            var result = await _pictureService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("BlogImageList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompanyImage(int id, Picture model)
        {
            var result = await _pictureService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("CompanyImageList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCancelRequest(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _cancelMembershipService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("CancelRequestList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteFeedback(int id, Feedback model)
        {
            var result = await _feedbackService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("FeedbackList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteStokList(int id, Stock model)
        {
            var result = await _stockService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("ProductStockList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteStok(int id, Stock model)
        {
            var result = await _stockService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("ProductStock", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteSavedBlogContent(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _savedContentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("SavedBlogList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteSavedCompanyContent(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _savedContentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("SavedCompanyList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteSavedProductContent(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _savedContentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("SavedProductList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteSavedCompanyPartnershipContent(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _savedContentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("SavedPartnershipList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteLike(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _likeService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("LikeList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteQuestion(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _questionService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("QuestionList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteQuestionAnswer(int id, string userId)
        {
            userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
            var result = await _questionAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("QuestionList", "HomeCompany", new { id = userId });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteAppointment(int id, Appointment model)
        {
            var result = await _appointmentService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("YourCompanyAppointmentList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteAppointmentAnswer(int id, AppointmentAnswer model)
        {
            var result = await _appointmentAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("AppointmentAnswerList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCustomer(int id, Customer model)
        {
            var result = await _customerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("CustomerList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompanyPartnership(int id, CompanyPartnership model)
        {
            var result = await _companyPartnershipService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("PartnershipList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompanyFormAnswer(int id, User model)
        {
            var result = await _companyFormAnswerService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("YourFormAnswerList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }
        public async Task<IActionResult> DeleteCompanyForm(int id, CompanyForm model)
        {
            var result = await _companyFormService.SetDeletedAsync(id);
            if (result)
            {
                return RedirectToAction("YourCompanyFormList", "HomeCompany", new { id = model.Id });
            }
            return RedirectToAction("Page404", "HomeCompany");
        }

        //[HttpPost]
        //public async Task<JsonResult> CompanySelectSystem(int? countryId, int? categoryId, string tip)
        //{
        //    var countryList = await _countryService.GetAllIncludingForAddCompanyAsync();
        //    var cityList = await _cityService.GetAllIncludingForAddCompanyAsync(countryId);

        //    var categoryList = await _companyCategoryService.GetAllIncludingForAddCompanyAsync();
        //    var subcategoryList = await _companySubcategoryService.GetAllIncludingByCategoryIdAsync(categoryId);

        //    List<SelectListItem> sonuc = new List<SelectListItem>();
        //    bool basariliMi = true;
        //    try
        //    {
        //        switch (tip)
        //        {
        //            case "GetCountry":
        //                foreach (var item in countryList)
        //                {
        //                    sonuc.Add(new SelectListItem
        //                    {
        //                        Text = item.Name,
        //                        Value = item.Id.ToString()
        //                    });
        //                }
        //                break;
        //            case "GetCity":
        //                foreach (var item in cityList)
        //                {
        //                    sonuc.Add(new SelectListItem
        //                    {
        //                        Text = item.Name,
        //                        Value = item.Id.ToString()
        //                    });
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        basariliMi = false;
        //        sonuc = new List<SelectListItem>();
        //        sonuc.Add(new SelectListItem
        //        {
        //            Text = "Something went wrong",
        //            Value = "Default"
        //        });
        //    }

        //    List<SelectListItem> result = new List<SelectListItem>();
        //    bool isOke = true;
        //    try
        //    {
        //        switch (tip)
        //        {
        //            case "GetCategory":
        //                foreach (var item in categoryList)
        //                {
        //                    sonuc.Add(new SelectListItem
        //                    {
        //                        Text = item.Name,
        //                        Value = item.Id.ToString()
        //                    });
        //                }
        //                break;
        //            case "GetSubcategory":
        //                foreach (var item in subcategoryList)
        //                {
        //                    sonuc.Add(new SelectListItem
        //                    {
        //                        Text = item.Name,
        //                        Value = item.Id.ToString()
        //                    });
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        isOke = false;
        //        result = new List<SelectListItem>();
        //        result.Add(new SelectListItem
        //        {
        //            Text = "Something went wrong",
        //            Value = "Default"
        //        });
        //    }
        //    return Json(new { ok = isOke, text = sonuc });
        //}
    }
}
