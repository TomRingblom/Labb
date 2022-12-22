using Labb.Features.Shared;
using System.ComponentModel.DataAnnotations;

namespace Labb.Features.News
{
    [ContentType(
        DisplayName = "Page: News Page",
        Description = "This is a news page",
        GUID = "e208f55c-98d3-49d3-ab04-3ac28a08ea7b")]
    [ImageUrl("/icons/cms/pages/container.png")]
    public class NewsPage : LabbPageData
    {
        [Display(Name = "Test Area", Order = 10)]
        public virtual ContentArea? Area { get; set; }
    }
}
