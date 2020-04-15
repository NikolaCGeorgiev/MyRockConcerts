namespace MyRockConcerts.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using MyRockConcerts.Web.ViewComponents.Models;

    public class ErrorAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            var viewModel = new ErrorAlertViewComponentViewModel()
            {
                ErrorMessage = message,
            };

            return this.View(viewModel);
        }
    }
}
