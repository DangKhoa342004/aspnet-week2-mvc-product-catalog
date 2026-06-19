using System.ComponentModel.DataAnnotations;

namespace AspNetWeek2.Mvc.ViewModels;

public class OrderCreateViewModel
{
    [Required(ErrorMessage = "Tên khách hàng không được để trống")]
    [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
    public string ProductName { get; set; } = "";

    [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
    [Range(1, 1000, ErrorMessage = "Mã sản phẩm phải nhỏ hơn hoặc bằng số sản phẩm hiện có")]
    public int ProductId { get; set; } = 0;

    [Required(ErrorMessage = "Nhóm sản phẩm không được để trống")]
    public string Category { get; set; } = "";

    [Range(1000, 100000000, ErrorMessage = "Giá bán phải từ 1.000 đến 100.000.000")]
    public decimal UnitPrice { get; set; }

    [Range(0, 10000, ErrorMessage = "Số lượng phải từ 0 đến 10.000")]
    public int Quantity { get; set; }
}