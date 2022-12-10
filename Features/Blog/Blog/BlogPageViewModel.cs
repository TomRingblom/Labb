using Labb.Features.Shared;

namespace Labb.Features.Blog.Blog
{
    public class BlogPageViewModel : ContentViewModel<BlogPage>
    {
        public BlogPageViewModel(BlogPage currentPage) : base(currentPage)
        {
        }
    }
}
