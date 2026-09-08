using FlashCard.Maui.Models;
using FlashCard.Maui.ViewModels;

namespace FlashCard.Maui.Views;

public partial class StudyPage : ContentPage
{
    private readonly StudyViewModel viewModel;

    public StudyPage(Deck deck)
    {
        InitializeComponent();
        viewModel = new StudyViewModel(deck);
        BindingContext = viewModel;
        viewModel.PropertyChanged += ViewModelPropertyChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadAsync();
        UpdateState();
    }

    private async void ViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        UpdateState();
        if (e.PropertyName == nameof(StudyViewModel.IsBackShown) && viewModel.IsBackShown)
            await BackLabel.FadeTo(1, 250);
    }

    private void UpdateState()
    {
        ShowButton.IsVisible = !viewModel.IsBackShown && !viewModel.IsDeckFinished;
        AnswerButtons.IsVisible = viewModel.IsBackShown;
        FinishedSection.IsVisible = viewModel.IsDeckFinished;
    }
}
