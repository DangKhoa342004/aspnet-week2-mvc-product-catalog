using AspNetWeek2.Mvc.Models;
using System.Collections.Generic;

namespace AspNetWeek2.Mvc.Repositories;

public interface IOrderRepository
{
	Task<List<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task AddAsync(Order order);
	Task SaveChangesAsync();
}
