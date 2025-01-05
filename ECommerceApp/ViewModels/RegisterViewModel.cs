using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ECommerceApp.Models;
using System.Text.RegularExpressions;
using ECommerceApp.Services;

namespace ECommerceApp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string nameError;

        [ObservableProperty]
        private string emailError;

        [ObservableProperty]
        private string passwordError;

        public RegisterViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        private bool ValidateRegistration()
        {
            bool isValid = true;

            // Clear previous errors
            NameError = string.Empty;
            EmailError = string.Empty;
            PasswordError = string.Empty;

            // Name validation
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Name is required";
                isValid = false;
            }

            // Email validation
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email is required";
                isValid = false;
            }
            else if (!Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                EmailError = "Invalid email format";
                isValid = false;
            }

            // Password validation
            if (string.IsNullOrWhiteSpace(Password))
            {
                PasswordError = "Password is required";
                isValid = false;
            }
            else if (Password.Length < 6)
            {
                PasswordError = "Password must be at least 6 characters";
                isValid = false;
            }

            return isValid;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            try
            {
                if (!ValidateRegistration())
                {
                    return;
                }

                // Check if email already exists
                var existingUser = await _databaseService.GetUserByEmailAsync(Email);
                if (existingUser != null)
                {
                    EmailError = "Email already registered";
                    return;
                }

                // Create new user with hashed password
                var user = new User
                {
                    Name = Name.Trim(),
                    Email = Email.Trim().ToLower(),
                    Password = HashPassword(Password)  // Hash the password
                };

                // Save to database
                await _databaseService.RegisterUserAsync(user);
                await Shell.Current.DisplayAlert("Success", "Registration successful!", "OK");

                // Clear the form
                Name = string.Empty;
                Email = string.Empty;
                Password = string.Empty;

                // Navigate back to login
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Registration failed", "OK");
                System.Diagnostics.Debug.WriteLine($"Registration error: {ex.Message}");
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert hash to string
                return Convert.ToBase64String(hash);
            }
        }
    }
}