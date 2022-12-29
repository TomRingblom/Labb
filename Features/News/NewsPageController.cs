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
            return View(new NewsPageViewModel(currentPage));
        }

        public async Task<IActionResult> AddBlock(NewsPage currentPage)
        {
            var randomCharacter = await GetRandomCharacter();
            var cardBlock = CreateCardBlockWithCharacter(randomCharacter);
            var newBlock = SetNameOnBlockSaveAndPublish(cardBlock, randomCharacter);
            var page = AddBlockToPageContentArea(currentPage, newBlock);

            SaveAndPublishPage(page);
            
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

        private void SaveAndPublishPage(NewsPage page)
        {
            if (User.Identity is { IsAuthenticated: true }) 
                _contentRepository.Save(page, SaveAction.Publish);
            else 
                _contentRepository.Save(page, SaveAction.Publish, AccessLevel.NoAccess);
        }

        private CardBlock CreateCardBlockWithCharacter(CharacterRoot character)
        {
            var cardBlock = _contentRepository.GetDefault<CardBlock>(ContentReference.GlobalBlockFolder);
            cardBlock.Title = character.Name;
            cardBlock.Body = $"This is {character.Name} and he/she lives in {character.Location?.Name}";
            cardBlock.ImageUrl = character.Image;

            return cardBlock;
        }

        private IContent SetNameOnBlockSaveAndPublish(CardBlock cardBlock, CharacterRoot character)
        {
            if (cardBlock is IContent newBlock)
            {
                newBlock.Name = character.Name;
                _contentRepository.Save(newBlock, SaveAction.Publish, AccessLevel.NoAccess);
                return newBlock;
            }

            return (IContent)cardBlock;
        }

        private NewsPage AddBlockToPageContentArea(NewsPage newsPage, IContent newBlock)
        {
            var page = (NewsPage)newsPage.CreateWritableClone();
            page.Area ??= new ContentArea();

            page.Area.Items.Add(new ContentAreaItem
            {
                ContentLink = (newBlock).ContentLink
            });

            return page;
        }
    }
}
