using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AboutProductCounter : ViewComponent
    {
        readonly IProductService _productService;
        public AboutProductCounter(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                var productCount = _productService.ProductCounter();
                ViewData["productCount"] = productCount >= 0 ? productCount : 0;
            }
            catch (Exception)
            {
                ViewData["productCount"] = 0;
            }
            return View();
        }
    }
}
