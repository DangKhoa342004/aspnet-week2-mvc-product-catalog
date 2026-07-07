using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetWeek2.Mvc.Repositories;

public class OrderRepository : IOrderRepository
{
	private readonly AppDbContext _context;

	public OrderRepository(AppDbContext context)
	{
		_context = context;
	}

	public Task<List<Order>> GetAllAsync()
		=> _context.Orders
				   .Include(o => o.OrderItems)
					   .ThenInclude(oi => oi.Product)
				   .ToListAsync();

	public Task<List<Order>> GetAllReadOnlyAsync()
        => _context.Orders
                   .Include(o => o.OrderItems)
                       .ThenInclude(oi => oi.Product)
                   .AsNoTracking()
                   .ToListAsync();

	public Task<Order?> GetByIdAsync(int id)
		=> _context.Orders
				   .Include(o => o.OrderItems)
					   .ThenInclude(oi => oi.Product)
				   .FirstOrDefaultAsync(o => o.Id == id);

	public async Task AddAsync(Order order)
		=> await _context.Orders.AddAsync(order);

	public Task SaveChangesAsync()
		=> _context.SaveChangesAsync();

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
        	_context.Orders.Add(order);
            await _context.SaveChangesAsync();

        	var item = new OrderItem
        	{
            	OrderId = order.Id,
            	ProductId = product.Id,
            	Quantity = model.Quantity,
            	UnitPrice = product.Price
        	};
        	_context.OrderItems.Add(item);
        	product.Stock -= model.Quantity;

        	await _context.SaveChangesAsync();
        	await transaction.CommitAsync();
    	}
    	catch
    	{
        	await transaction.RollbackAsync();
        	throw;
    	}
	}
}
