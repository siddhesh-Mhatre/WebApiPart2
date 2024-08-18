using MyWeb.Models;
using System.Collections.Generic;
using MyWeb.DTO;

namespace MyWeb.Repo
{
    public interface IRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void ProcessSale(int productId, int quantity);

        int CheckStock(int productId);
        decimal GetTotalStockValue(int productId);

        List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice);

        List<ProductDetail> GetProductDetailsByStockStatus(int productId);
    }
}
