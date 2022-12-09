using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Home;

public class HomePageController : PageController<HomePage>
{
    public ActionResult Index(HomePage currentContent) => View("~/Features/Home/Index.cshtml", ContentViewModel.Create(currentContent));
}