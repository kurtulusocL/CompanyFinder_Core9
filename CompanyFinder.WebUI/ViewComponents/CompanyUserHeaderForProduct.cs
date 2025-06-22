using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHeaderForProduct:ViewComponent
    {
        readonly IProductService _productService;
        public CompanyUserHeaderForProduct(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_productService.GetIncludingProductForHeaderByCompanyId(id));
        }
    }
}
