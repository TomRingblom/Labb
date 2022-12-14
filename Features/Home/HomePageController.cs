using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Home;

public class HomePageController : PageController<HomePage>
{
    public ActionResult Index(HomePage currentContent) => View("~/Features/Home/Index.cshtml", ContentViewModel.Create(currentContent));

    public ActionResult SignIn()
    {
        var scheme = OpenIdConnectDefaults.AuthenticationScheme;
        var redirectUrl = Url.ActionContext.HttpContext.Request.Scheme + "://" +
                          Url.ActionContext.HttpContext.Request.Host;
        return Challenge(new AuthenticationProperties
        {
            RedirectUri = redirectUrl
        }, scheme);
    }

    public ActionResult SignOut()
    {
        var scheme = OpenIdConnectDefaults.AuthenticationScheme;
        return SignOut(new AuthenticationProperties(), CookieAuthenticationDefaults.AuthenticationScheme, scheme);
    }
}