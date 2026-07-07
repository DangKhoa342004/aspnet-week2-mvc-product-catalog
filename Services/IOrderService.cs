using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;
using System.Collections.Generic;

namespace AspNetWeek2.Mvc.Services;

public interface IOrderService
{
	Task<List<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task CreateAsync(Order order);
	Task CreateOrderAsync(OrderCreateViewModel model);

}
