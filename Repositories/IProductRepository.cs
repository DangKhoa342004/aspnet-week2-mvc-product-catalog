using AspNetWeek2.Mvc.Models;
using System.Collections.Generic;

namespace AspNetWeek2.Mvc.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<List<Product>> GetAllReadOnlyAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task SaveChangesAsync();
}