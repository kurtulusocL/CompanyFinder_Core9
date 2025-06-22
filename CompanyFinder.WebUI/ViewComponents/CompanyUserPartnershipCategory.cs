using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserPartnershipCategory : ViewComponent
    {
        readonly IProductCategoryService _productCategoryService;
        public CompanyUserPartnershipCategory(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_productCategoryService.GetAllIncludingProductCategoryForCompanyPartnership());
        }
    }
}
