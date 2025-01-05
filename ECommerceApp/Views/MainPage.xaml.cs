using ECommerceApp.ViewModels;

namespace ECommerceApp.Views
{
    public partial class MainPage : ContentPage
    {
     
        public MainPage(ProductViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBar = (SearchBar)sender;
            var viewModel = (ProductViewModel)BindingContext;
            viewModel.SearchProductsCommand.Execute(searchBar.Text);
        }

    }

}
