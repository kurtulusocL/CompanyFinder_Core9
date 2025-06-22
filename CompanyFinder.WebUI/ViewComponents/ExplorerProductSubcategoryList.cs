using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductSubcategoryList:ViewComponent
    {
        readonly IProductSubcategoryService _productSubcategoryService;
        public ExplorerProductSubcategoryList(IProductSubcategoryService productSubcategoryService)
        {
            _productSubcategoryService = productSubcategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productSubcategoryService.GetAllIncludingProductSubcategoriesForProductExplorer());
        }
    }
}
