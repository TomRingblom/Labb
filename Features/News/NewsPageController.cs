using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.Shell.Security;
using EPiServer.Web.Mvc;
using Labb.Features.Blocks.CardBlock;
using Labb.Features.Character;
using Labb.Features.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Labb.Features.News
{
    public class NewsPageController : PageController<NewsPage>
    {
        private readonly IContentRepository _contentRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiUrl = "https://rickandmortyapi.com/api/character/";

        public NewsPageController(IContentRepository contentRepository, IHttpClientFactory httpClientFactory)
        {
            _contentRepository = contentRepository;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index(NewsPage currentPage)
        {
            return View("~/Features/News/Index.cshtml", new NewsPageViewModel(currentPage));
        }

        public async Task<IActionResult> AddBlock(NewsPage currentPage)
        {
            var randomCharacter = await GetRandomCharacter();
            var page = (NewsPage)currentPage.CreateWritableClone();

            var cardBlock = _contentRepository.GetDefault<CardBlock>(ContentReference.GlobalBlockFolder);
            cardBlock.Title = randomCharacter.Name;
            cardBlock.Body = $"This is {randomCharacter.Name} and he/she lives in {randomCharacter.Location?.Name}";
            cardBlock.ImageUrl = randomCharacter.Image;
            var newBlock = cardBlock as IContent;
            if (newBlock != null)
            {
                newBlock.Name = randomCharacter.Name;

                _contentRepository.Save(newBlock, SaveAction.Publish, AccessLevel.NoAccess);

                page.Area ??= new ContentArea();

                page.Area.Items.Add(new ContentAreaItem
                {
                    ContentLink = (newBlock).ContentLink
                });
            }

            if (User.Identity is {IsAuthenticated: true})
            {
                _contentRepository.Save(page, SaveAction.Publish);
            }
            else
            {
                _contentRepository.Save(page, SaveAction.Publish, AccessLevel.NoAccess);
            }
            

            var model = new NewsPageViewModel(page);
            return View("~/Features/News/Index.cshtml", model);
        }

        public IActionResult DeleteBlock(NewsPage currentPage, int block)
        {
            var page = (NewsPage)currentPage.CreateWritableClone();
            if(page.Area != null)
            {
                foreach(var i in page.Area.Items)
                {
                    if (i.ContentLink.ID != block) continue;
                    page.Area.Items.Remove(i);
                    break;
                }
            }

            _contentRepository.Save(page, SaveAction.Publish, AccessLevel.NoAccess);

            var model = new NewsPageViewModel(page);
            return View("~/Features/News/Index.cshtml", model);
        }

        private async Task<CharacterRoot> GetRandomCharacter()
        {
            var rnd = new Random();
            var randomNumber = rnd.Next(1, 20);
            var client = _httpClientFactory.CreateClient();
            var req = await client.GetAsync(ApiUrl + randomNumber);

            if (!req.IsSuccessStatusCode)
            {

            }

            var res = await req.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CharacterRoot>(res) ?? new CharacterRoot();
        }
    }
}
