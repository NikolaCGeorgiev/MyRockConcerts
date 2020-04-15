namespace MyRockConcerts.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Web.ViewComponents.Models;

    public class SuccessAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            var viewModel = new SuccessAlertViewComponentViewModel()
            {
                SuccessMessage = message,
            };

            return this.View(viewModel);
        }
    }
}
