using ECommerceApp.ViewModels;

namespace ECommerceApp.Views;

public partial class CartPage : ContentPage
{
    private CartViewModel _viewModel;

    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        _viewModel.UpdateQuantityCommand.Execute(null);
    }
}