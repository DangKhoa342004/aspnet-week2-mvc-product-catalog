using AspNetWeek2.Mvc.Models;
using AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWeek2.Mvc.Controllers;

public class ProductsController : Controller
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    public IActionResult Index()
    {
        var products = _productService.GetAll()
            .Select(ToListItemViewModel)
            .ToList();

        return View(products);
    }

    public IActionResult Detail(int id)
    {
        var product = _productService.GetById(id);

        if (product == null)
        {
            return NotFound($"Không tìm thấy sản phẩm có id = {id}");
        }

        var viewModel = ToDetailViewModel(product);

        return View(viewModel);
    }

    public IActionResult Stats()
    {
        var stats = _productService.GetStats();

        return View(stats);
    }

    public IActionResult Welcome()
    {
        return Content("Welcome to ASP.NET Core MVC Lab02");
    }

    public IActionResult ProductJson()
    {
        var products = _productService.GetAll()
            .Select(product => new
            {
                product.Id,
                product.Sku,
                product.Name,
                product.Category,
                product.UnitPrice,
                product.Quantity
            });

        return Json(products);
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là response 404 demo từ action Force404.");
    }

    private static ProductListItemViewModel ToListItemViewModel(Product product)
    {
        return new ProductListItemViewModel
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Category = product.Category,
            UnitPrice = product.UnitPrice,
            Quantity = product.Quantity,
            MinStock = product.MinStock
        };
    }

    private static ProductDetailViewModel ToDetailViewModel(Product product)
    {
        return new ProductDetailViewModel
        {
            Id = product.Id,
            Sku = product.Sku,
            Name = product.Name,
            Category = product.Category,
            Supplier = product.Supplier,
            UnitPrice = product.UnitPrice,
            Quantity = product.Quantity,
            MinStock = product.MinStock,
            LastUpdatedAt = product.LastUpdatedAt
        };
    }
}