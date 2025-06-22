using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserHomeProducts:ViewComponent
    {
        readonly IProductService _productService;
        public CompanyUserHomeProducts(IProductService productService)
        { 
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_productService.GetAllIncludingCompanyProductsByCompanyId(id));
        }
    }
}
