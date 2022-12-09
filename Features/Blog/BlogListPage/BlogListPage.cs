using Labb.Features.Shared;
using System.ComponentModel.DataAnnotations;

namespace Labb.Features.Blog.BlogListPage
{
    [ContentType(
        DisplayName = "Page: Blog List",
        GUID = "b0115b46-db70-4862-98b8-2fd65a9627f3",
        Description = "This is a page to list all blog posts")]
    [AvailableContentTypes(Include = new[] { typeof(BlogPage.BlogPage)})]
    [ImageUrl("/icons/cms/pages/container.png")]
    public class BlogListPage : LabbPageData
    {
        //[Display(
        //    GroupName = SystemTabNames.Content,
        //    Order = 10)]
        //public virtual PageReference? Blogs { get; set; }
    }
}
