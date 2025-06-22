using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerProductCompanyLocation:ViewComponent
    {
        readonly IProductService _productService;
        public ExplorerProductCompanyLocation(IProductService productService)
        {
            _productService = productService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_productService.GetIncludingProductByCompanyId(id));
        }
    }
}
