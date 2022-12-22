using EPiServer.Shell;
using EPiServer.Web.Mvc;
using Labb.Features.Blog.Blog;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable Mvc.ViewNotResolved

namespace Labb.Features.Blog.BlogList
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
                Blogs = _contentRepository.GetChildren<BlogPage>(currentContent.ContentLink)
            };

            return View(model);
        }
    }
}
