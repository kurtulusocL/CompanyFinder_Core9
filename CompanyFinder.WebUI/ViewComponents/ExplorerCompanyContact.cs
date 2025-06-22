using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class ExplorerCompanyContact : ViewComponent
    {
        readonly ICompanyContactService _companyContactService;
        public ExplorerCompanyContact(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_companyContactService.GetCompanyContactByCompanyId(id));
        }
    }
}
