using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Blog.BlogListPage
{
    public class BlogListPageController : PageController<BlogListPage>
    {
        private readonly IContentRepository _contentRepository;

        public BlogListPageController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public ActionResult Index(BlogListPage currentContent)
        {
            var model = new BlogListPageViewModel(currentContent)
            {
                Blogs = _contentRepository.GetChildren<BlogPage.BlogPage>(currentContent.ContentLink)
            };
            //var blogs = _contentRepository.GetChildren<BlogPage.BlogPage>(currentContent.ContentLink);
            return View("~/Features/Blog/BlogListPage/Index.cshtml", model);
        }
    }
}
