using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyWeb.DTO;
using MyWeb.Models;

namespace MyWeb.Data;

public partial class SqlStoredContext : DbContext
{
    public SqlStoredContext()
    {
    }

    public SqlStoredContext(DbContextOptions<SqlStoredContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }

}
