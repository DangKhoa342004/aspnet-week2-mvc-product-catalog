using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;
using AspNetWeek2.Mvc.Repositories;
using AspNetWeek2.Mvc.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspNetWeek2.Mvc.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly AppDbContext _context;

	public OrderService(IOrderRepository orderRepository,AppDbContext context)
	{
		_orderRepository = orderRepository;
		_context = context;
	}

	public Task<List<Order>> GetAllAsync()
		=> _orderRepository.GetAllAsync();

	public Task<Order?> GetByIdAsync(int id)
		=> _orderRepository.GetByIdAsync(id);

	public Task CreateAsync(Order order)
		=> _orderRepository.AddAsync(order);

	public Task CreateOrderAsync(OrderCreateViewModel model)
	{
    	return _orderRepository.CreateOrderAsync(model);
	}
}
