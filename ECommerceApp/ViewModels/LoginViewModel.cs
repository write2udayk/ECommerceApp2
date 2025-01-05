using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;
using ECommerceApp.Services;
using ECommerceApp.Views;

public partial class LoginViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string emailError;

    [ObservableProperty]
    private string passwordError;

    public LoginViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    private bool ValidateLogin()
    {
        bool isValid = true;

        // Clear previous errors
        EmailError = string.Empty;
        PasswordError = string.Empty;

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

        return isValid;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            if (!ValidateLogin())
                return;

            var user = await _databaseService.GetUserByEmailAsync(Email);
            string hashedPassword = HashPassword(Password);

            if (user != null && user.Password == hashedPassword)
            {
                await Shell.Current.GoToAsync("///MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", "Login failed", "OK");
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task GoToRegister()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}