using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdvancedProductSearch : ViewComponent
    {
        readonly IProductSubcategoryService _productSubcategoryService;
        readonly IProductCategoryService _productCategoryService;
        public AdvancedProductSearch(IProductSubcategoryService productSubcategoryService, IProductCategoryService productCategoryService)
        {
            _productSubcategoryService = productSubcategoryService;
            _productCategoryService = productCategoryService;
        }
        public IViewComponentResult Invoke(string key)
        {
            ViewBag.ProductCategories = _productCategoryService.GetAllIncludingProductCategoriesForProductAdvancedSearch();
            ViewBag.ProductSubcategories = _productSubcategoryService.GetAllIncludingProductSubcategoriesForProductAdvancedSearch();
            return View();
        }
    }
}
