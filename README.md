# ECommerceApp - .NET MAUI Shopping Application

A cross-platform e-commerce application built with .NET MAUI, featuring user authentication, product management, and shopping cart functionality.

## Features

- **User Authentication**
  - User registration with email validation
  - Secure login with password hashing
  - Input validation and error handling

- **Product Management**
  - View product listings
  - Search products
  - Add new products
  - Delete products

- **Shopping Cart**
  - Add products to cart
  - View cart items
  - Manage cart contents

## Project Structure
ECommerceApp/
├── Models/
│ ├── CartItem.cs
│ ├── Product.cs
│ └── User.cs
├── Services/
│ └── DatabaseService.cs
├── ViewModels/
│ ├── CartViewModel.cs
│ ├── LoginViewModel.cs
│ ├── ProductViewModel.cs
│ └── RegisterViewModel.cs
├── Views/
│ ├── CartPage.xaml
│ ├── LoginPage.xaml
│ ├── MainPage.xaml
│ └── RegisterPage.xa



## Technologies Used

- .NET MAUI
- SQLite for local database
- MVVM architecture
- CommunityToolkit.Mvvm
- SHA256 for password hashing

## Getting Started

1. **Prerequisites**
   - Visual Studio 2022 with .NET MAUI workload
   - .NET 7.0 or later

2. **Installation**
   ```bash
   git clone https://github.com/yourusername/ECommerceApp.git
   cd ECommerceApp
   ```

3. **Build and Run**
   - Open the solution in Visual Studio
   - Build the solution
   - Select your target platform
   - Run the application

## Features Implementation

### User Authentication
- Email format validation
- Password strength requirements
- Secure password storage with hashing

### Product Management
- CRUD operations for products
- Search functionality
- Price filtering

### Database
- SQLite local database
- Async operations
- Data persistence

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Acknowledgments

- .NET MAUI Documentation
- SQLite.NET Documentation
- Community Toolkit MVVM

