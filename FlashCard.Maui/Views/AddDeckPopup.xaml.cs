using CommunityToolkit.Maui.Views;
using FlashCard.Maui.ViewModels;

namespace FlashCard.Maui.Views;

public partial class AddDeckPopup : Popup
{
    public AddDeckPopup()
    {
        InitializeComponent();
        BindingContext = new AddDeckPopupViewModel(this);
    }
}
