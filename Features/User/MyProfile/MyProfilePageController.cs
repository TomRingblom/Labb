using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Labb.Features.User.MyProfile
{
    public class MyProfilePageController : PageController<MyProfilePage>
    {
        [Authorize]
        public ActionResult Index(MyProfilePage currentContent)
        {
            var user = User;
            var model = new MyProfilePageViewModel(currentContent)
            {
                Email = user?.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = user?.FindFirst(ClaimTypes.GivenName)?.Value,
                LastName = user?.FindFirst(ClaimTypes.Surname)?.Value
            };
            //if (User.Identity.IsAuthenticated)
            //{
            //    foreach(var i in User.Claims)
            //    {
            //        if(i.Type.EndsWith("emailaddress"))
            //            model.Email = i.Value;
            //        if (i.Type.EndsWith("surname"))
            //            model.LastName = i.Value;
            //        if (i.Type.EndsWith("givenname"))
            //            model.FirstName = i.Value;
            //    }
            //}

            return View("~/Features/User/MyProfile/Index.cshtml", model);
        }
    }
}
