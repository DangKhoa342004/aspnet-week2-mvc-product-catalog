using System.Collections.Generic;

namespace AspNetWeek2.Mvc.ViewModels;

public class DataHealthViewModel
{
    public bool CanConnectToDatabase { get; set; }
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int TotalOrders { get; set; }
    public string DatabaseProvider { get; set; } = string.Empty;
}