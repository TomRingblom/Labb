using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable Mvc.ViewNotResolved

namespace Labb.Features.Blog.Blog
{
    public class BlogPageController : PageController<BlogPage>
    {
        public ActionResult Index(BlogPage currentContent)
        {
            return View(new BlogPageViewModel(currentContent));
        }
    }
}
