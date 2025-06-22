using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdvancedPartnershipSearch:ViewComponent
    {
        readonly IProductCategoryService _productCategoryService;
        public AdvancedPartnershipSearch(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Categories = _productCategoryService.GetAllIncludingProductCategoriesForProductAdvancedSearch();
            return View();
        }
    }
}
