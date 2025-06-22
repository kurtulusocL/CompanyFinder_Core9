using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProduct : ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerProduct(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAllIncludingProductsForExplorer());
        }
    }
}
