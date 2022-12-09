using EPiServer.Web.Mvc;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Blog.BlogPage
{
    public class BlogPageController : PageController<BlogPage>
    {
        public ActionResult Index(BlogPage currentContent)
        {
            var model = new BlogPageViewModel(currentContent);
            return View("~/Features/Blog/BlogPage/Index.cshtml", model);
        }
    }
}
