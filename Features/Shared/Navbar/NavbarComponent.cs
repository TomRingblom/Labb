using EPiServer.Web.Mvc;
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
            var root = _contentRepository.GetChildren<LabbPageData>(ContentReference.RootPage).Where(x => x.VisibleInMenu);
            var start = _contentRepository.GetChildren<LabbPageData>(ContentReference.StartPage).Where(x => x.VisibleInMenu);
            var navItems = root.Concat(start).ToList();
            return View("~/Features/Shared/Navbar/_Navbar.cshtml", navItems);
        }
    }
}
