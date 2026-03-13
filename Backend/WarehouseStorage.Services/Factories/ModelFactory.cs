using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.DTOs.DataTransferObjects;

namespace WarehouseStorage.Services.Factories
{
    
    public static class ModelFactory
    {

        public static Product[] CreateMultipleProductDTO(ProductDTO product, int quantity)
        {
            Product[] products = new Product[quantity];
            for (int i = 0; i < quantity; i++)
            {
                products[i] = new Product(
                    DomainFactory.CreateProductName(product.Name),
                    DomainFactory.CreateProductNumber(product.Number),
                    DomainFactory.CreatePrice(product.DefaultPrice),
                    DomainFactory.CreateCurrency(product.DefaultCurrency),
                    Guid.NewGuid());
            }
            return products;
        }
       

        
        public static Product CreateProduct(ProductDTO product)
        {
            product.Id ??= Guid.NewGuid();

            var currencyCode = string.IsNullOrWhiteSpace(product.DefaultCurrency) ? "USD" : product.DefaultCurrency;
            var defaultPrice = product.DefaultPrice <= 0 ? 1m : product.DefaultPrice;

            return new Product(
                DomainFactory.CreateProductName(product.Name),
                DomainFactory.CreateProductNumber(product.Number),
                DomainFactory.CreatePrice(defaultPrice),
                DomainFactory.CreateCurrency(currencyCode),
                product.Id);
        }

        

    }
}