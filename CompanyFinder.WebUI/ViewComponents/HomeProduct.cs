using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeProduct:ViewComponent
    {
        readonly IProductService _productService;
        public HomeProduct(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAllIncludingRandomProductForHome());
        }
    }
}
