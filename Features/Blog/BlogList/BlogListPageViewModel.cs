using Labb.Features.Blog.Blog;
using Labb.Features.Shared;

namespace Labb.Features.Blog.BlogList
{
    public class BlogListPageViewModel : ContentViewModel<BlogListPage>
    {
        public BlogListPageViewModel(BlogListPage currentPage) : base(currentPage)
        {
        }

        public IEnumerable<BlogPage>? Blogs { get; set; }
    }
}
