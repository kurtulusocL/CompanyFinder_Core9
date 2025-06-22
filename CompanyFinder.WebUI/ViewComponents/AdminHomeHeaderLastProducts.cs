using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class AdminHomeHeaderLastProducts:ViewComponent
    {
        readonly IProductService _productService;
        public AdminHomeHeaderLastProducts(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productService.GetAllIncludingProductsForAdminHeader());
        }
    }
}
