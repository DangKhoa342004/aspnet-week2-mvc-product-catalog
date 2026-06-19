using AspNetWeek2.Mvc.Models;
using System.Collections.Generic;

namespace AspNetWeek2.Mvc.Services;

public interface IOrderService
{
	Task<List<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task CreateAsync(Order order);
}
