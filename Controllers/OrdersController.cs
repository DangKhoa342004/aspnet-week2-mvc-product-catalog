using Microsoft.AspNetCore.Mvc;
using AspNetWeek2.Mvc.Services;
using AspNetWeek2.Mvc.ViewModels;
using System;
using System.Threading.Tasks;

namespace AspNetWeek2.Mvc.Controllers;

public class OrdersController : Controller
{
    private readonly IOrderService _orderService;
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orders = await _orderService.GetAllAsync();
        return View(orders);
    }

   [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _orderService.CreateOrderAsync(model);

            TempData["SuccessMessage"] = "Tạo đơn hàng thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Lỗi khi tạo đơn hàng: " + ex.Message);
            return View(model);
        }
    }
}