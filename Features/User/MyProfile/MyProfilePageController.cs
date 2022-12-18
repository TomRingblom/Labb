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
            if (User.Identity.IsAuthenticated)
            {
                foreach(var i in User.Claims)
                {
                    if(i.Type.EndsWith("emailaddress"))
                        model.Email = i.Value;
                    if (i.Type.EndsWith("surname"))
                        model.LastName = i.Value;
                    if (i.Type.EndsWith("givenname"))
                        model.FirstName = i.Value;
                }
            }

            return View("~/Features/User/MyProfile/Index.cshtml", model);
        }
    }
}
