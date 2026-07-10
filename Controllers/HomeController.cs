using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetWeek2.Mvc.Data;

namespace AspNetWeek2.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    
    }

    public async Task<IActionResult> Index()
        {
            var totalProducts = await _context.Products.IgnoreQueryFilters().AsNoTracking().CountAsync();
            var activeProducts = await _context.Products.AsNoTracking().CountAsync();
            var deletedProducts = await _context.Products.IgnoreQueryFilters().AsNoTracking().CountAsync(e => e.IsDeleted);

            ViewBag.Total = totalProducts;
            ViewBag.Active = activeProducts;
            ViewBag.Deleted = deletedProducts;

            return View();
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
