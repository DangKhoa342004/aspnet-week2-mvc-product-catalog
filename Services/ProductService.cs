using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Repositories;
using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(AppDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ProductListItemViewModel>> GetActiveProductsAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new ProductListItemViewModel
            {
                Id = p.Id, Name = p.Name, Price = p.Price,
                StockQuantity = p.StockQuantity, CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }
}