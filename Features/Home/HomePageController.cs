using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Home;

public class HomePageController : PageController<HomePage>
{
    public IActionResult Index(HomePage currentPage)
    {
        var viewModel = new ComposedPageViewModel<HomePage, HomePageViewModel>
        {
            Page = currentPage,
            ViewModel = new HomePageViewModel()
        };
        return View($"~/Features/Home/Index.cshtml", viewModel);
    }
}