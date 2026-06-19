using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;
using AspNetWeek2.Mvc.Repositories;
using AspNetWeek2.Mvc.Options;
using Microsoft.Extensions.Options;

namespace AspNetWeek2.Mvc.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly AppSettings _settings;

    public ProductService(IProductRepository productRepository, IOptions<AppSettings> options)
    {
        _productRepository = productRepository;
        _settings = options.Value;
    }

    public async Task<List<ProductListItemViewModel>> GetProductListAsync()
    {
        var products = await _productRepository.GetAllReadOnlyAsync();
        return products.Select(p => new ProductListItemViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Stock = p.Stock,
            CategoryName = p.Category != null ? p.Category.Name : "N/A"
        }).ToList();
    }
}