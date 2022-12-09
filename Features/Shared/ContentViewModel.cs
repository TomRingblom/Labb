using EPiServer.ServiceLocation;
using Labb.Features.Home;

namespace Labb.Features.Shared
{
    public class ContentViewModel<TContent> : IContentViewModel<TContent> where TContent : IContent
    {
        private Injected<IContentLoader> _contentLoader;
        private HomePage _startPage;

        public ContentViewModel() : this(default)
        {
        }

        public ContentViewModel(TContent currentContent)
        {
            CurrentContent = currentContent;
        }

        public TContent CurrentContent { get; set; }

        public virtual HomePage StartPage
        {
            get
            {
                if (_startPage == null)
                {
                    ContentReference currentStartPageLink = ContentReference.StartPage;
                    _startPage = _contentLoader.Service.Get<HomePage>(currentStartPageLink);
                }

                return _startPage;
            }
        }
    }

    public static class ContentViewModel
    {
        public static ContentViewModel<T> Create<T>(T content) where T : IContent => new ContentViewModel<T>(content);
    }
}
