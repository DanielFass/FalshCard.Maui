using FlashCard.Maui.ViewModels;

namespace FlashCard.Maui.Views;

public partial class MainMenuPage : ContentPage
{
    private readonly MainMenuViewModel viewModel = new();

    public MainMenuPage()
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.Decks.CollectionChanged += (_, _) => EmptyMessage.IsVisible = viewModel.Decks.Count == 0;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadDecksAsync();
        EmptyMessage.IsVisible = viewModel.Decks.Count == 0;
    }

    private async void OnDeckSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.Deck deck)
            await viewModel.OpenDeckCommand.ExecuteAsync(deck);
        ((CollectionView)sender).SelectedItem = null;
    }
}
