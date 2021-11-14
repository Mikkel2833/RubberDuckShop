namespace RubberDuckShop.ProductService.Application.DTOs;
    public class ProductCategoryDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<ProductDTO>? Products { get; set; }
}
