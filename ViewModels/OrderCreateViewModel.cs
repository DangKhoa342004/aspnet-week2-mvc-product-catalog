using System.ComponentModel.DataAnnotations;

namespace AspNetWeek2.Mvc.ViewModels;

public class OrderCreateViewModel
{
    [Required(ErrorMessage = "Vui lòng chọn sản phẩm")]
    public int ProductId { get; set; }

    [Range(1, 10000, ErrorMessage = "Số lượng phải từ 1 đến 10.000")]
    public int Quantity { get; set; }
}