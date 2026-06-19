using AspNetWeek2.Mvc.ViewModels;

public interface IProductService
{
    Task<List<ProductListItemViewModel>> GetProductListAsync();
    Task<ProductDetailViewModel?> GetProductDetailAsync(int id);
}
