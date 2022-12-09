using Labb.Features.Blog.BlogPage;
using Labb.Features.Shared;

namespace Labb.Features.Blog.BlogListPage
{
    public class BlogListPageViewModel : ContentViewModel<BlogListPage>
    {
        public BlogListPageViewModel(BlogListPage currentPage) : base(currentPage)
        {
        }

        public IEnumerable<BlogPage.BlogPage> Blogs { get; set; }
    }
}
