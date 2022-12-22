using Labb.Features.Shared;

namespace Labb.Features.Blocks.CardBlock
{
    public class CardBlockViewModel : BlockViewModel<CardBlock>
    {
        public CardBlockViewModel(CardBlock currentBlock) : base(currentBlock)
        {

        }

        public string ImageUrl { get; set; }
        public string DeleteUrl { get; set; }
    }
}
