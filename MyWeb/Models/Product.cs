using System;
using System.Collections.Generic;

namespace MyWeb.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }
}
