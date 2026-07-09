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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var exists = await _context.Products
                .IgnoreQueryFilters()
                .AnyAsync(p => p.SKU == model.SKU);

        if (exists)
        {
            ModelState.AddModelError(nameof(model.SKU), "SKU này đã tồn tại.");
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name,
            SKU = model.SKU,
            Price = model.Price,
            StockQuantity = model.StockQuantity,
            Description = model.Description,
            CreatedAt = DateTime.Now
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Product created. ProductId={ProductId}, SKU={SKU}", product.Id, product.SKU);

        TempData["Success"] = "Đã thêm sản phẩm thành công.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
    {
        if (id != model.Id) return NotFound();
        if (!ModelState.IsValid) return View(model);

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = model.Name;
        product.SKU = model.SKU;
        product.Price = model.Price;
        product.StockQuantity = model.StockQuantity;
        product.Description = model.Description;
        product.UpdatedAt = DateTime.Now;

        _context.Entry(product).Property("RowVersion").OriginalValue =
            Convert.FromBase64String(model.RowVersion);

        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation("Product updated. ProductId={ProductId}", id);
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            ModelState.AddModelError(string.Empty,
                "Dữ liệu đã được người khác cập nhật. Vui lòng tải lại trang và thử lại.");
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        product.IsDeleted = true;
        product.DeletedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        _logger.LogWarning("Product soft deleted. ProductId={ProductId}", id);

        TempData["Success"] = "Đã xóa mềm sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Trash()
    {
        var deletedProducts = await _context.Products
            .IgnoreQueryFilters()
            .Where(p => p.IsDeleted)
            .AsNoTracking()
            .Select(p => new ProductTrashItemViewModel
            {
                Id = p.Id, Name = p.Name, DeletedAt = p.DeletedAt
            })
            .ToListAsync();

        return View(deletedProducts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Restore(int id)
    {
        var product = await _context.Products
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);

        if (product == null) return NotFound();

        product.IsDeleted = false;
        product.DeletedAt = null;
        product.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        _logger.LogInformation("Product restored. ProductId={ProductId}", id);
        return RedirectToAction(nameof(Trash));
    }
}