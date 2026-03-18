using WarehouseStorage.Services.Repositories.Repositories;

namespace WarehouseStorage.Tests
{   
    public class ProductRepositoryTests
    {
        private readonly WarehouseDbContext _context;
 
        
        public ProductRepositoryTests(WarehouseDbContext context)
        {
            _context = context;
        }
    }
}
