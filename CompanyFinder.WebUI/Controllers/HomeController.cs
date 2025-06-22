using CompanyFinder.Business.Attributes;
using CompanyFinder.Business.Services.Abstract;
using CompanyFinder.Core.SiteMap;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.Controllers
{
    [ExceptionHandler]
    public class HomeController : Controller
    {
        readonly IAboutService _aboutService;
        readonly IBlogService _blogService;
        readonly IBlogCategoryService _blogCategoryService;
        readonly ICityService _cityService;
        readonly ICountryService _countryService;
        readonly ICommentService _commentService;
        readonly ICommentAnswerService _commentAnswerService;
        readonly ICompanyCategoryService _companyCategoryService;
        readonly ICompanySubcategoryService _companySubcategoryService;
        readonly IContactService _contactService;
        readonly IFrequentlyService _frequentlyService;
        readonly ILayoutInfoService _layoutInfoService;
        readonly ILogoService _logoService;
        readonly IOurServicesService _ourServicesService;
        readonly INewsService _newsService;
        readonly IProductCategoryService _productCategoryService;
        readonly IProductSubcategoryService _productSubcategoryService;
        readonly IProductService _productService;
        readonly ISliderService _sliderService;
        readonly ISocialMediaService _socialMediaService;
        readonly IAdService _adService;
        readonly ICompanyFormService _companyFormService;
        readonly IFormCategoryService _formCategoryService;
        public HomeController(IAboutService aboutService, IBlogService blogService, IBlogCategoryService blogCategoryService, ICityService cityService, ICountryService countryService, ICommentService commentService, ICommentAnswerService commentAnswerService, ICompanyCategoryService companyCategoryService, ICompanySubcategoryService companySubcategoryService, IContactService contactService, IFrequentlyService frequentlyService, ILayoutInfoService layoutInfoService, ILogoService logoService, IOurServicesService ourServicesService, INewsService newsService, IProductCategoryService productCategoryService, IProductSubcategoryService productSubcategoryService, IProductService productService, ISliderService sliderService, ISocialMediaService socialMediaService, IAdService adService, ICompanyFormService companyFormService, IFormCategoryService formCategoryService)
        {
            _aboutService = aboutService;
            _blogService = blogService;
            _blogCategoryService = blogCategoryService;
            _cityService = cityService;
            _countryService = countryService;
            _commentService = commentService;
            _commentAnswerService = commentAnswerService;
            _companyCategoryService = companyCategoryService;
            _companySubcategoryService = companySubcategoryService;
            _contactService = contactService;
            _frequentlyService = frequentlyService;
            _layoutInfoService = layoutInfoService;
            _logoService = logoService;
            _ourServicesService = ourServicesService;
            _newsService = newsService;
            _productCategoryService = productCategoryService;
            _productSubcategoryService = productSubcategoryService;
            _productService = productService;
            _sliderService = sliderService;
            _socialMediaService = socialMediaService;
            _adService = adService;
            _companyFormService = companyFormService;
            _formCategoryService = formCategoryService;
        }

        public IActionResult Index()
        {           
            return View();
        }
        public IActionResult NotFound(int? code)
        {
            if (code.HasValue && code == 404)
            {
                return View(nameof(NotFound));
            }
            return View();
        }

        public IActionResult ServerError(int? code)
        {
            if (code.HasValue && code == 500)
            {
                return View(nameof(ServerError));
            }
            return View();
        
        }
        public IActionResult BadRequest(int? code)
        {
            if (code.HasValue && code == 400)
            {
                return View(nameof(BadRequest));
            }
            return View();
        }
        public async Task<IActionResult> Product()
        {
            return View(await _productService.GetAllIncludingForPublicUserAsync());
        }
        public async Task<IActionResult> ProductDetail(int? id)
        {
            return View(await _productService.GetByIdAsync(id));
        }
        public IActionResult SmilarHitRead(int? id)
        {
            return View(_adService.SimilarHitRead(id));
        }

        [Route("sitemap.xml")]
        public IActionResult SitemapXml()
        {
            string baseUrl = "https://localhost:44378/";
            var siteMapBuilder = new SitemapBuilder();
            siteMapBuilder.AddUrl(baseUrl, createdDate: DateTime.UtcNow, changeFrequency: ChangeFrequency.Weekly, priority: 1.0);

            var about = _aboutService.GetAllSiteMap();
            foreach (var item in about)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Subtitle, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var blog = _blogService.GetAllSiteMap();
            foreach (var item in blog)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.Subtitle != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Subtitle, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.BlogCategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.ImageUrl, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var blogCategory = _blogCategoryService.GetAllSiteMap();
            foreach (var item in blogCategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var city = _cityService.GetAllSiteMap();
            foreach (var item in city)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.CountryId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Country.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var country = _countryService.GetAllSiteMap();
            foreach (var item in country)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var comment = _commentService.GetAllSiteMap();
            foreach (var item in comment)
            {
                if (item.Subject != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Subject, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.Text, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.ProductId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Product.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var commentAnswer = _commentAnswerService.GetAllSiteMap();
            foreach (var item in commentAnswer)
            {
                if (item.Title != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.Text, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.CommentId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Comment.Text, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var companyForm = _companyFormService.GetAllSitemap();
            foreach (var item in companyForm)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.FormCategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var companyCategory = _companyCategoryService.GetAllSiteMap();
            foreach (var item in companyCategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var companySubategory = _companySubcategoryService.GetAllSiteMap();
            foreach (var item in companySubategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.CompanyCategoryId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.CompanyCategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var contact = _contactService.GetAllSiteMap();
            foreach (var item in contact)
            {
                siteMapBuilder.AddUrl(baseUrl + item.City, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Country, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.PrincipalEmail, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.BusinessEmail, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var formCategory = _formCategoryService.GetAllSitemap();
            foreach (var item in formCategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var frequently = _frequentlyService.GetAllSiteMap();
            foreach (var item in frequently)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var layoutInfo = _layoutInfoService.GetAllSiteMap();
            foreach (var item in layoutInfo)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Author, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Keyword, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Content, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Language, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var logo = _logoService.GetAllSiteMap();
            foreach (var item in logo)
            {
                if (item.Title != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.ImageUrl, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var ourService = _ourServicesService.GetAllSiteMap();
            foreach (var item in ourService)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var news = _newsService.GetAllSiteMap();
            foreach (var item in news)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Subtitle, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.Detail != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Detail, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Hit.ToString(), createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Like.ToString(), createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.ImageUrl, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var productCategory = _productCategoryService.GetAllSiteMap();
            foreach (var item in productCategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var productSubategory = _productSubcategoryService.GetAllSiteMap();
            foreach (var item in productSubategory)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.ProductCategoryId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.ProductCategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var product = _productService.GetAllSiteMap();
            foreach (var item in product)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Desc, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.OtherText != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.OtherText, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.IsCommentable, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.IsQuestionable, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.IsAvailable, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.AvailableDate != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.AvailableDate.Value.ToShortDateString(), createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }

                siteMapBuilder.AddUrl(baseUrl + item.ProductCategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                if (item.ProductSubcategoryId != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.ProductSubcategory.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
            }

            var slider = _sliderService.GetAllSiteMap();
            foreach (var item in slider)
            {
                if (item.Title != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Title, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                if (item.Text != null)
                {
                    siteMapBuilder.AddUrl(baseUrl + item.Text, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                }
                siteMapBuilder.AddUrl(baseUrl + item.ImageUrl, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }

            var socialMedia = _socialMediaService.GetAllSiteMap();
            foreach (var item in socialMedia)
            {
                siteMapBuilder.AddUrl(baseUrl + item.Name, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.Url, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
                siteMapBuilder.AddUrl(baseUrl + item.ImageUrl, createdDate: item.CreatedDate, changeFrequency: null, priority: 0.9);
            }
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }
    }
}