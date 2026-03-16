using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {    Task<Product?> GetById(Guid id);
        Task<Product[]> GetAll();
        Task<Product> Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);
    }
}