using CommunityToolkit.Maui.Views;
using FlashCard.Maui.ViewModels;

namespace FlashCard.Maui.Views;

public partial class AddWordsPopup : Popup
{
    private readonly AddWordsPopupViewModel viewModel;

    public AddWordsPopup()
    {
        InitializeComponent();
        viewModel = new AddWordsPopupViewModel(this);
        BindingContext = viewModel;
        _ = viewModel.LoadDecksAsync();
    }
}
