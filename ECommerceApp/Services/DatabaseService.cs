using SQLite;
using ECommerceApp.Models;
using System.Linq;


namespace ECommerceApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "products.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeAsync().Wait(); // Initialize tables when service is created

        }

        // Get all products with LINQ
        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _database.Table<Product>().ToListAsync();
            
            return products;
        }

        // Search products with LINQ
        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _database.Table<Product>()
                .Where(p => p.Title.ToLower().Contains(searchTerm.ToLower()) ||
                           p.Description.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(p => p.Title)
                .ToListAsync();
        }

        
        // Save product
        public async Task<int> SaveProductAsync(Product product)
        {
            if (product.Id != 0)
            {
                return await _database.UpdateAsync(product);
            }
            else
            {
                return await _database.InsertAsync(product);
            }
        }

        // Delete product with LINQ
        public async Task<int> DeleteProductAsync(Product product)
        {
            return await _database.Table<Product>()
                .Where(p => p.Id == product.Id)
                .DeleteAsync();
        }



        public async Task<bool> CheckDatabaseAsync()
        {
            var count = await _database.Table<Product>().CountAsync();
            System.Diagnostics.Debug.WriteLine($"Database has {count} products");
            return count > 0;
        }


        public async Task InitializeAsync()
        {
            await _database.CreateTableAsync<User>();
            await _database.CreateTableAsync<Product>();
            
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _database.Table<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<int> RegisterUserAsync(User user)
        {
            return await _database.InsertAsync(user);

        }
    }
}
