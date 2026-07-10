using System.ComponentModel.DataAnnotations;

namespace AspNetWeek2.Mvc.ViewModels;

public class OrderCreateViewModel
{
    [Required(ErrorMessage = "Tên khách hàng không được để trống")]
    [StringLength(100, ErrorMessage = "Tên khách hàng không được vượt quá 100 ký tự")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
    [Range(1, 3, ErrorMessage = "Vui lòng chọn một sản phẩm hợp lệ")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Số lượng không được để trống")]
    [Range(0, 100, ErrorMessage = "Số lượng phải từ 0 đến 100")]
    public int Quantity { get; set; }
}