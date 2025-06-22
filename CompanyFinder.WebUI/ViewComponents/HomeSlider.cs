using CompanyFinder.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CompanyFinder.WebUI.ViewComponents
{
    public class HomeSlider : ViewComponent
    {
        readonly ISliderService _sliderService;
        public HomeSlider(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_sliderService.GetAllSlidersForHome());
        }
    }
}
