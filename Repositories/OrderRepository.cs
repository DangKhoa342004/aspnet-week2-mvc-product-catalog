using AspNetWeek2.Mvc.Data;
using AspNetWeek2.Mvc.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

	public Task<Order?> GetByIdAsync(int id)
		=> _context.Orders
				   .Include(o => o.OrderItems)
					   .ThenInclude(oi => oi.Product)
				   .FirstOrDefaultAsync(o => o.Id == id);

	public async Task AddAsync(Order order)
		=> await _context.Orders.AddAsync(order);

	public Task SaveChangesAsync()
		=> _context.SaveChangesAsync();
}
