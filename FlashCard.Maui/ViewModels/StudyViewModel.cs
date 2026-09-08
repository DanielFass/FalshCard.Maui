using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCard.Maui.Models;
using System.Collections.Generic;

namespace FlashCard.Maui.ViewModels;

public partial class StudyViewModel : ObservableObject
{
    private readonly Queue<Flashcard> cards = new();
    private readonly int deckId;

    [ObservableProperty] private Flashcard? currentCard;
    [ObservableProperty] private bool isBackShown;
    [ObservableProperty] private bool isDeckFinished;
    [ObservableProperty] private int cardsRemaining;
    [ObservableProperty] private int totalCards;

    public StudyViewModel(Deck deck) => deckId = deck.Id;

    public async Task LoadAsync()
    {
        cards.Clear();
        foreach (var card in await App.GameRepository.GetCardsForDeckAsync(deckId))
            cards.Enqueue(card);
        TotalCards = cards.Count;
        NextCard();
    }

    [RelayCommand]
    private void Show() => IsBackShown = true;

    [RelayCommand]
    private void Again()
    {
        if (CurrentCard is not null) cards.Enqueue(CurrentCard);
        NextCard();
    }

    [RelayCommand]
    private void Done() => NextCard();

    [RelayCommand]
    private async Task CloseAsync() => await Shell.Current.Navigation.PopAsync();

    private void NextCard()
    {
        CurrentCard = cards.Count > 0 ? cards.Dequeue() : null;
        CardsRemaining = cards.Count;
        IsBackShown = false;
        IsDeckFinished = CurrentCard is null;
    }
}
