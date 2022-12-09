using EPiServer.Web;
using Labb.Features.Shared;
using System.ComponentModel.DataAnnotations;

namespace Labb.Features.Blog.BlogPage
{
    [ContentType(
        GUID = "cafa74c8-c6bb-4050-9bf4-edc67b532528",
        DisplayName = "Page: Blog",
        Description = "This is a blog page")]
    [ImageUrl("/icons/cms/pages/article.png")]
    public class BlogPage : LabbPageData
    {
        [UIHint(UIHint.Image)]
        [Display(
            Name = "Image",
            GroupName = SystemTabNames.Content,
            Order = 10)]
        public virtual ContentReference? Image { get; set; }

        [Display(
            Name = "Title",
            GroupName = SystemTabNames.Content,
            Order = 20)]
        [CultureSpecific]
        public virtual string? Title { get; set; }

        [Display(
            Name = "Author",
            GroupName = SystemTabNames.Content,
            Order = 30)]
        [CultureSpecific]
        public virtual string? Author { get; set; }

        [Display(
            Name = "Preamble",
            GroupName = SystemTabNames.Content,
            Order = 35)]
        
        [UIHint(UIHint.Textarea)]
        [CultureSpecific]
        public virtual string? Preamble { get; set; }

        [Display(
            Name = "Post",
            GroupName = SystemTabNames.Content,
            Order = 40)]
        [CultureSpecific]
        public virtual XhtmlString? Post { get; set; }
    }
}
