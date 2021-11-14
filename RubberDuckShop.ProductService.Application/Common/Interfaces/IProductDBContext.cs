
using Microsoft.EntityFrameworkCore;
using RubberDuckShop.ProductService.Domain.Entities;

namespace RubberDuckShop.ProductService.Application.Common.Interfaces;

public interface IProductDBContext
{
    DbSet<Product> Products { get; }
    DbSet<ProductCategory> ProductCategories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

