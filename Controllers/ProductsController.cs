using AspNetWeek2.Mvc.ViewModels;
using AspNetWeek2.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetWeek2.Mvc.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProductListAsync();
        return View(products);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var viewModel = await _productService.GetProductDetailAsync(id);

        if (viewModel == null)
        {
            return NotFound($"Không tìm thấy sản phẩm có id = {id}");
        }

        return View(viewModel);
    }

    public IActionResult Welcome()
    {
        return Content("Welcome to ASP.NET Core MVC Lab02");
    }

    public IActionResult GoToList()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Force404()
    {
        return NotFound("Đây là response 404 demo từ action Force404.");
    }
}