using EPiServer.Shell.Security;
using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Register
{
    public class RegisterPageController : PageController<RegisterPage>
    {
        private readonly UIUserManager _userManager;

        public RegisterPageController(UIUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(RegisterPage currentPage)
        {
            var viewModel = new ComposedPageViewModel<RegisterPage, RegisterPageViewModel>
            {
                Page = currentPage,
                ViewModel = new RegisterPageViewModel()
            };
            return View("~/Features/Register/Index.cshtml", viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterPageViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser
        //        {
        //            UserName = model.UserName
        //        };

        //        var addUser = await _userManager. .CreateAsync(user, model.Password);

        //        if (addUser.Succeeded)
        //        {
        //            //do something
        //        }
        //    }
        //    return View("~/Features/Register/Index.cshtml");
        //}
    }
}
