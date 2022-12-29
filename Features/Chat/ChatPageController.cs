using EPiServer.Shell.Security;
using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Chat
{
    public class ChatPageController : PageController<ChatPage>
    {
        public IActionResult Index(ChatPage currentPage)
        {
            var viewModel = new ComposedPageViewModel<ChatPage, ChatPageViewModel>
            {
                Page = currentPage,
                ViewModel = new ChatPageViewModel()
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
