namespace RubberDuckShop.ProductService.Application.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductCategoryDTO ProductCategory { get; set; }
}

