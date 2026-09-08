using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using FlashCard.Maui.Models;
using System.Collections.ObjectModel;

namespace FlashCard.Maui.ViewModels;

public partial class AddWordsPopupViewModel(Popup popup) : ObservableObject
{
    public ObservableCollection<Deck> Decks { get; } = new();

    [ObservableProperty] private Deck? selectedDeck;
    [ObservableProperty] private string frontText = string.Empty;
    [ObservableProperty] private string backText = string.Empty;

    public async Task LoadDecksAsync()
    {
        Decks.Clear();
        foreach (var deck in await App.GameRepository.GetDecksAsync())
            Decks.Add(deck);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (SelectedDeck is null || string.IsNullOrWhiteSpace(FrontText) || string.IsNullOrWhiteSpace(BackText))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Missing information", "Choose a deck and enter both sides of the card.", "OK");
            return;
        }

        await App.GameRepository.AddCardAsync(new Flashcard
        {
            DeckId = SelectedDeck.Id,
            Front = FrontText.Trim(),
            Back = BackText.Trim()
        });
        popup.Close();
    }

    [RelayCommand]
    private void Cancel() => popup.Close();
}
