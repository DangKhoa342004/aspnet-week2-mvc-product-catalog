using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Repositories;
using System.Collections.Generic;

namespace AspNetWeek2.Mvc.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;

	public OrderService(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	public Task<List<Order>> GetAllAsync()
		=> _orderRepository.GetAllAsync();

	public Task<Order?> GetByIdAsync(int id)
		=> _orderRepository.GetByIdAsync(id);

	public Task CreateAsync(Order order)
		=> _orderRepository.AddAsync(order);
}
