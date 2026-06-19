namespace AspNetWeek2.Mvc.ViewModels;

public class ProductDetailViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Category { get; set; } = "";

    public decimal UnitPrice { get; set; }

    public int Stock { get; set; }

    public string PriceText => $"{UnitPrice:N0} VND";

    public decimal InventoryValue => UnitPrice * Stock;

    public string InventoryValueText => $"{InventoryValue:N0} VND";

}