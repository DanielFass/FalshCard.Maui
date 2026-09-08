using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCard.Maui.Models;
using CommunityToolkit.Maui.Views;

namespace FlashCard.Maui.ViewModels;

public partial class AddDeckPopupViewModel(Popup popup) : ObservableObject
{
    [ObservableProperty]
    private string deckName = string.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(DeckName))
        {
            await Shell.Current.CurrentPage.DisplayAlert("Missing name", "Enter a deck name.", "OK");
            return;
        }

        await App.GameRepository.AddDeckAsync(new Deck { Name = DeckName.Trim() });
        popup.Close();
    }

    [RelayCommand]
    private void Cancel() => popup.Close();
}
