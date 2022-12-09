using EPiServer.Shell.Security;
using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Login
{
    public class LoginPageController : PageController<LoginPage>
    {
        private readonly UIUserManager _userManager;
        private readonly UISignInManager _signInManager;

        public LoginPageController(UIUserManager userManager, UISignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(LoginPage currentPage)
        {
            var viewModel = new ComposedPageViewModel<LoginPage, LoginPageViewModel>
            {
                Page = currentPage,
                ViewModel = new LoginPageViewModel()
            };
            return View("~/Features/Login/Index.cshtml", viewModel);
        }

        public IActionResult Login(LoginPageViewModel model)
        {
            var signin = _signInManager.SignInAsync(model.Username, model.Password);
            if (signin.IsCompletedSuccessfully)
            {
                //Yay
            }

            return View();
        }
    }
}
