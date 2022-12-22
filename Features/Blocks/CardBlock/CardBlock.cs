using EPiServer.Web;
using System.ComponentModel.DataAnnotations;
// ReSharper disable Mvc.TemplateNotResolved

namespace Labb.Features.Blocks.CardBlock
{
    [ContentType(DisplayName = "Card Block", GUID = "26a8b40e-b427-4854-a196-67f2e3fe5913")]
    public class CardBlock : BlockData
    {
        [Display(GroupName = SystemTabNames.Content, Order = 10)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference? Image { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 15)]
        public virtual string? ImageUrl { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 20)]
        public virtual string? Title { get; set; }

        [Display(GroupName = SystemTabNames.Content, Order = 30)]
        public virtual string? Body { get; set; }
    }
}
