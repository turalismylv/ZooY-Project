using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class HelpLineViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
