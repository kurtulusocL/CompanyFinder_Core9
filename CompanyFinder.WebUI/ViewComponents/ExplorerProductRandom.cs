using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductRandom:ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerProductRandom(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAllIncludingRandomProducts());
        }
    }
}
