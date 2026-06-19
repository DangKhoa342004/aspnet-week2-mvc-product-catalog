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

	public async Task CreateOrderAsync(OrderCreateViewModel model)
	{
    	await using var transaction = await _context.Database.BeginTransactionAsync();
    	try
    	{
        	var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.ProductId);
        	if (product == null) throw new Exception("Product not found");
        	if (product.Stock < model.Quantity) throw new Exception("Not enough stock");

        	var order = new Order
        	{
            	CreatedAt = DateTime.Now,
            	TotalAmount = product.Price * model.Quantity
        	};
        	await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

        	var item = new OrderItem
        	{
            	OrderId = order.Id,
            	ProductId = product.Id,
            	Quantity = model.Quantity,
            	UnitPrice = product.Price
        	};
        	_context.OrderItems.Add(item);
        	product.Stock -= model.Quantity;

        	await _orderRepository.SaveChangesAsync();
        	await transaction.CommitAsync();
    	}
    	catch
    	{
        	await transaction.RollbackAsync();
        	throw;
    	}
	}
}
