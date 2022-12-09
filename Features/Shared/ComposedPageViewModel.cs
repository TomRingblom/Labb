namespace Labb.Features.Shared;

public class ComposedPageViewModel<TPage, TViewModel> where TPage : PageData
{
    public TPage? Page { get; set; }
    public TViewModel? ViewModel { get; set; }
}