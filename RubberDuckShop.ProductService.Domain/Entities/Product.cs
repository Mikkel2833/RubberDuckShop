using RubberDuckShop.ProductService.Domain.Common;

namespace RubberDuckShop.ProductService.Domain.Entities;

public class Product : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }
}

