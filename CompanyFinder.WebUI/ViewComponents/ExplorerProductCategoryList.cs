using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductCategoryList:ViewComponent
    {
        readonly IProductCategoryService _productCategoryService;
        public ExplorerProductCategoryList(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productCategoryService.GetAllIncludingProductCategoriesForProductExplorer());
        }
    }
}
