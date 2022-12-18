using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using Labb.Features.Home;
using Labb.Features.User.MyProfile;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Shared.Navbar
{
    public class NavbarComponent : ViewComponent
    {
        private readonly IContentRepository _contentRepository;

        public NavbarComponent(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var underRoot = _contentRepository.GetChildren<HomePage>(ContentReference.RootPage).Where(x => x.VisibleInMenu);
            var underStart = _contentRepository.GetChildren<LabbPageData>(ContentReference.StartPage).Where(x => x.VisibleInMenu);
            var navItems = underRoot.Concat(underStart).ToList();
            var myProfile = _contentRepository.GetChildren<MyProfilePage>(ContentReference.StartPage).FirstOrDefault();

            var model = new NavbarViewModel
            {
                NavItems = navItems,
                SignInUrl = Url.ContentUrl(underRoot.FirstOrDefault()?.ContentLink) + "SignIn",
                SignOutUrl = Url.ContentUrl(underRoot.FirstOrDefault()?.ContentLink) + "SignOut",
                MyProfileUrl = Url.ContentUrl(myProfile?.ContentLink)
            };

            return await Task.FromResult(View("~/Features/Shared/Navbar/Default.cshtml", model));
        }
    }
}