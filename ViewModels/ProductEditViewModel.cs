using System.ComponentModel.DataAnnotations;

public class ProductEditViewModel : ProductCreateViewModel
{
    public int Id { get; set; }
    public string RowVersion { get; set; } = string.Empty;
}