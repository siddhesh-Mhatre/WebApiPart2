using MyWeb.Repo;
using MyWeb.Models;
using MyWeb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using MyWeb.DTO;

namespace MyWeb.Services
{
    public class Services : IRepository
    {
        private readonly SqlStoredContext _context;

        public Services(SqlStoredContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Database.ExecuteSqlRaw("EXEC AddProduct {0}, {1}, {2}, {3}", product.ProductName, product.ProductDescription, product.Price, product.StockQuantity);
        }

        public int CheckStock(int productId)
        {
            var stockLevelParam = new SqlParameter
            {
                ParameterName = "@StockLevel",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            _context.Database.ExecuteSqlRaw("EXEC ChekStock @ProductID = {0}, @StockLevel = @StockLevel OUTPUT", productId, stockLevelParam);

            return (int)stockLevelParam.Value;
        }

        public Product GetProductById(int id)
        {
            var data = _context.Products.FromSqlRaw("EXEC GetProductById {0}", id).ToList();
            return data.FirstOrDefault();
        }

        public List<Product> GetProducts()
        {
            return _context.Products.FromSqlRaw("EXEC GetAllProducts").ToList();
        }



        public decimal GetTotalStockValue(int productId)
        {
            var totalValueParam = new SqlParameter
            {
                ParameterName = "@TotalValue",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Precision = 10,
                Scale = 2,
                Direction = System.Data.ParameterDirection.Output
            };
            _context.Database.ExecuteSqlRaw("SELECT @TotalValue=dbo.GetTotalStockValue(@ProductID)",totalValueParam, new SqlParameter("@ProductID", productId));
            return (decimal)totalValueParam.Value;
        }

        public void ProcessSale(int productId, int quantity)
        {
            try
            {
                _context.Database.ExecuteSqlRaw("EXEC ProcessSale {0}, {1}", productId, quantity);
            }
            catch (Exception ex)
            {
                // Log the exception if needed (e.g., using a logging framework)
                // Rethrow the exception or handle it appropriately
                throw new Exception("An error occurred while processing the sale: " + ex.Message);
            }
        }


        public List<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = _context.Products.FromSqlRaw("SELECT * FROM dbo.GetProductsByPriceRange({0},{1})",minPrice,maxPrice).ToList();
            return products;
        }

 
        public List<ProductDetail> GetProductDetailsByStockStatus(int productId)
        {
            var productDetails = _context.Set<ProductDetail>()
                .FromSqlRaw("SELECT * FROM dbo.GetProductsDetailByStockStatus({0})", productId)
                .ToList();

            return productDetails;
        }
    }
}
