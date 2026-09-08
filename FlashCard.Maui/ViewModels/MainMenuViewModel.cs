using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using FlashCard.Maui.Models;
using FlashCard.Maui.Views;
using System.Collections.ObjectModel;

namespace FlashCard.Maui.ViewModels;

public partial class MainMenuViewModel : ObservableObject
{
    public ObservableCollection<Deck> Decks { get; } = new();

    [RelayCommand]
    private async Task AddDeckAsync()
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(new AddDeckPopup());
        await LoadDecksAsync();
    }

    [RelayCommand]
    private async Task AddWordsAsync()
    {
        await Shell.Current.CurrentPage.ShowPopupAsync(new AddWordsPopup());
        await LoadDecksAsync();
    }

    [RelayCommand]
    private async Task DeleteDeckAsync(Deck? deck)
    {
        if (deck is null || !await Shell.Current.CurrentPage.DisplayAlert("Delete deck?", $"Delete {deck.Name}?", "Yes", "No"))
            return;

        await App.GameRepository.DeleteDeckAsync(deck.Id);
        await LoadDecksAsync();
    }

    [RelayCommand]
    private async Task OpenDeckAsync(Deck? deck)
    {
        if (deck is not null)
            await Shell.Current.Navigation.PushAsync(new StudyPage(deck));
    }

    public async Task LoadDecksAsync()
    {
        Decks.Clear();
        foreach (var deck in await App.GameRepository.GetDecksAsync())
        {
            deck.CardCount = await App.GameRepository.GetCardCountForDeckAsync(deck.Id);
            Decks.Add(deck);
        }
    }
}
