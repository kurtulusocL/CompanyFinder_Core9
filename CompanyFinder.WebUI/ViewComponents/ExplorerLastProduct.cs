using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerLastProduct : ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerLastProduct(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAllIncludingLastProductsForExplorer());
        }
    }
}
