using System.ComponentModel.DataAnnotations;

namespace MyWeb.DTO
{
    public class ProductDetail
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string StockStatus { get; set; }
    }
}
