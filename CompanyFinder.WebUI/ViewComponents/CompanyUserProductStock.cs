using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class CompanyUserProductStock : ViewComponent
    {
        readonly IStockService _stockService;
        public CompanyUserProductStock(IStockService stockService)
        {
            _stockService = stockService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_stockService.GetProductForCompanyUserSrockByProductId(id));
        }
    }
}
