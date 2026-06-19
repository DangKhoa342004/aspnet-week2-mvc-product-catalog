using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;

namespace AspNetWeek2.Mvc.Controllers;

public class CategoriesController : Controller
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
                                       .Include(c => c.Products)
                                       .AsNoTracking()
                                       .ToListAsync();

        return View(categories);
    }
}