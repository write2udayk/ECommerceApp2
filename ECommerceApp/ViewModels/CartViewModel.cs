using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ECommerceApp.Models;

namespace ECommerceApp.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public ObservableCollection<CartItem> CartItems { get; } = new();

        [ObservableProperty]
        private decimal cartTotal;

        public CartViewModel()
        {
            // Subscribe to collection changes
            CartItems.CollectionChanged += (s, e) => UpdateCartTotals();
        }

        [RelayCommand]
        private void RemoveFromCart(CartItem item)
        {
            if (item != null)
            {
                CartItems.Remove(item);
                UpdateCartTotals();
            }
        }

        [RelayCommand]
        private void UpdateQuantity()
        {
            UpdateCartTotals();
        }

        private void UpdateCartTotals()
        {
            CartTotal = CartItems.Sum(item => item.Price * item.Quantity);
            System.Diagnostics.Debug.WriteLine($"Cart Total Updated: ${CartTotal}"); // Debug line
        }
    }
}