// using AspNetWeek2.Mvc.Models;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using System;

// namespace AspNetWeek2.Mvc.Repositories;

// public class FakeProductRepository : IProductRepository
// {
//     public Task<List<Product>> GetAllReadOnlyAsync()
//     {
//         var data = new List<Product>
//         {
//             new Product { Id = 1, Name = "Mouse", Price = 250000, Stock = 10 },
//             new Product { Id = 2, Name = "Broken", Price = 0, Stock = 0 }
//         };
//         return Task.FromResult(data);
//     }
//     public Task<List<Product>> GetAllAsync() 
//         => Task.FromResult(new List<Product>());
//     public Task<Product?> GetByIdAsync(int id) 
//         => Task.FromResult<Product?>(null);
//     public Task AddAsync(Product product) 
//         => Task.CompletedTask;
//     public Task SaveChangesAsync() 
//         => Task.CompletedTask;
// }