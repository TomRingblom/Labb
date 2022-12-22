using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Labb.Features.Blocks.CardBlock
{
    public class CardBlockComponent : AsyncBlockComponent<CardBlock>
    {
        private readonly IPageRouteHelper _pageRouteHelper;

        public CardBlockComponent(IPageRouteHelper pageRouteHelper)
        {
            _pageRouteHelper = pageRouteHelper;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(CardBlock currentContent)
        {
            var contentLink = (currentContent as IContent).ContentLink;
            var model = new CardBlockViewModel(currentContent)
            {
                ImageUrl = currentContent.Image != null ? Url.ContentUrl(currentContent.Image) : currentContent.ImageUrl,
                DeleteUrl = Url.ContentUrl(_pageRouteHelper.Page.ContentLink) + $"DeleteBlock?block={contentLink.ID}"
            };

            return await Task.FromResult(View("~/Features/Blocks/CardBlock/Default.cshtml", model));
        }
    }
}
