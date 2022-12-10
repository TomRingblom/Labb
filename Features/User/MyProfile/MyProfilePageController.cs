using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.User.MyProfile
{
    public class MyProfilePageController : PageController<MyProfilePage>
    {
        [Authorize]
        public ActionResult Index(MyProfilePage currentContent)
        {
            var model = new MyProfilePageViewModel(currentContent);
            return View("~/Features/User/MyProfile/Index.cshtml", model);
        }
    }
}
