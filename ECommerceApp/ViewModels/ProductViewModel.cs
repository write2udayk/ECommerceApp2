using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ECommerceApp.Models;
using ECommerceApp.Services;
using ECommerceApp.Views;


namespace ECommerceApp.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly CartViewModel _cartViewModel;
        public ObservableCollection<Product> Products { get; } = new();
        

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string price;

       
        public ProductViewModel(DatabaseService databaseService ,CartViewModel cartViewModel)
        {
            _databaseService = databaseService;
            _cartViewModel = cartViewModel;
            LoadProductsCommand.Execute(null);
        }
       

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            

            try
            {
               
                var products = await _databaseService.GetProductsAsync();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to load products", "OK");
            }
            
        }

        [RelayCommand]
        private async Task AddProductAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Description) ||
                !decimal.TryParse(Price, out decimal priceValue))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill all fields correctly", "OK");
                return;
            }

            try
            {
                var product = new Product
                {
                    Title = Title,
                    Description = Description,
                    Price = priceValue
                };

                var result = await _databaseService.SaveProductAsync(product);
                System.Diagnostics.Debug.WriteLine($"Product saved with result: {result}");

                // Clear form
                Title = string.Empty;
                Description = string.Empty;
                Price = string.Empty;

                // Reload products
                await LoadProductsAsync();

                await Shell.Current.DisplayAlert("Success", "Product added successfully!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to add product: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteProductAsync(Product product)
        {
            if (product == null) return;

            bool answer = await Shell.Current.DisplayAlert(
                "Delete Product",
                $"Are you sure you want to delete {product.Title}?",
                "Yes", "No");

            if (answer)
            {
                await _databaseService.DeleteProductAsync(product);
                await LoadProductsAsync();
            }
        }

        [RelayCommand]
        private async Task SearchProductsAsync(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    // Load all products when search is empty
                    await LoadProductsAsync();
                    return;
                }

                var products = await _databaseService.SearchProductsAsync(searchText);
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Failed to search products", "OK");
            }
        }

        [RelayCommand]
        private async Task AddToCartAsync(Product product)
        {
            try
            {
                var cartItem = new CartItem
                {
                    ProductTitle = product.Title,
                    Price = product.Price,
                    Quantity = 1
                };

                _cartViewModel.CartItems.Add(cartItem);
                await Shell.Current.DisplayAlert("Success", $"{product.Title} added to cart!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task GoToCart()
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }
    }
}
