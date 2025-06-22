using System.Threading.Tasks;
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
    public class ExplorerController : Controller
    {
        readonly ICompanyService _companyService;
        readonly IProductService _productService;
        readonly INewsService _newsService;
        readonly ICompanyPartnershipService _companyPartnershipService;
        readonly ICompanyFormService _companyFormService;
        public ExplorerController(ICompanyService companyService, IProductService productService, INewsService newsService, ICompanyPartnershipService companyPartnershipService, ICompanyFormService companyFormService)
        {
            _companyService = companyService;
            _productService = productService;
            _newsService = newsService;
            _companyPartnershipService = companyPartnershipService;
            _companyFormService = companyFormService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdvancedSearch()
        {
            return View();
        }
        public async Task<IActionResult> AdvancedProductSearchResult(string key, int page = 1)
        {
            var result = await _productService.GetAllIncludingProductAdvancedSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> AdvancedCompanySearchResult(string key, int page = 1)
        {
            var result = await _companyService.GetAllIncludingCompanyAdvancedSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }
        public IActionResult _CompanySearch(string key)
        {
            return View();
        }
        public async Task<IActionResult> CompanySearchResult(string key, int page = 1)
        {
            var result = await _companyService.GetAllIncludingSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }
        public IActionResult _ProductSearch(string key)
        {
            return PartialView();
        }
        public async Task<IActionResult> ProductSearchResult(string key, int page = 1)
        {
            var result = await _productService.GetAllIncludingSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }
        public IActionResult _PartnershipSearch(string key)
        {
            return PartialView();
        }
        public async Task<IActionResult> PartnershipSearchResult(string key, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingSearchResultasync(key);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> NewsList(int page = 1)
        {
            var result = await _newsService.GetAllAsync();
            return View(result.ToPagedList(page, 24));
        }
        public async Task<IActionResult> NewsDetail(int? id)
        {
            return View(await _newsService.GetByIdAsync(id));
        }
        public async Task<IActionResult> LikeNews(int? id)
        {
            var result = await _newsService.LikeAsync(id);
            if (result)
                return RedirectToAction(nameof(NewsDetail), new { id = id });
            return RedirectToAction(nameof(NewsDetail), new { id = id });
        }
        public IActionResult NewsHitRead(int? id)
        {
            return PartialView(_newsService.HitRead(id));
        }
        public IActionResult _CompanyFormSearch(string key)
        {
            return PartialView();
        }
        public async Task<IActionResult> CompanyFormSearchResult(string key, int page = 1)
        {
            var result = await _companyFormService.GetAllIncludingSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }

        public async Task<IActionResult> CompanyMatchResult(string key)
        {
            return View(await _companyService.GetAllIncludingCompanyMatchResultsAsync(key));
        }

        public async Task<IActionResult> AdvancedPartnershipSearchResult(string key, int page = 1)
        {
            var result = await _companyPartnershipService.GetAllIncludingAdvancedSearchResultAsync(key);
            return View(result.ToPagedList(page, 36));
        }
        public async Task<IActionResult> WeeklyMostPopulerCompanies(int page = 1)
        {
            var result = await _companyService.GetAllIncludingWeeklyPopularCompaniesAsync();
            return View(result.ToPagedList(page, 24));
        }
        public async Task<IActionResult> WeeklyMostPopulerProducts(int page = 1)
        {
            var result = await _productService.GetAllIncludingWeeklyPopularProductsAsync();
            return View(result.ToPagedList(page, 24));
        }
    }
}