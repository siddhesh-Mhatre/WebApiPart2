using Microsoft.AspNetCore.Mvc;
using MyWeb.Repo;
using MyWeb.Models;
using MyWeb.DTO;

namespace MyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepository _repository;

        public ProductController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _repository.AddProduct(product);
            return Ok("Product added successfully");
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            var data = _repository.GetProducts();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var data = _repository.GetProductById(id);
            return Ok(data);
        }

        [HttpPost]
        [Route("ProcessSale")]
        public IActionResult ProcessSale([FromBody] Sale sale)
        {
            try
            {
                _repository.ProcessSale(sale.ProductId, sale.Quantity);
                return Ok("Sale processed successfully");
            }
            catch (Exception ex)
            {
                // Return a bad request with the error message
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckStock/{productId}")]
        public IActionResult CheckStock(int productId)
        {
            try
            {
                int stockLevel = _repository.CheckStock(productId);
                return Ok(new { ProductId = productId, StockLevel = stockLevel });
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetTotalStockValue/{productId}")]
        public IActionResult GetTotalStockValue(int productId)
        {
            try
            {
                decimal totalValue = _repository.GetTotalStockValue(productId);
                return Ok(new { ProductId = productId, TotalValue = totalValue });
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductsByPriceRange")]
        public IActionResult GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            try
            {
                var products = _repository.GetProductsByPriceRange(minPrice, maxPrice);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetProductsDetailByStockStatus/{productId}")]
        public IActionResult GetProductsDetailByStockStatus(int productId)
        {
            try
            {
                var products = _repository.GetProductDetailsByStockStatus(productId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }
    }
}
