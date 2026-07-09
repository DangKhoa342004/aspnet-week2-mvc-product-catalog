using AspNetWeek2.Mvc.ViewModels;

namespace AspNetWeek2.Mvc.Services;

public interface IProductService
{
    Task<List<ProductListItemViewModel>> GetActiveProductsAsync();
    Task<ProductDetailViewModel?> GetDetailAsync(int id);
    Task CreateAsync(ProductCreateViewModel model);
    Task<bool> SoftDeleteAsync(int id);
    Task<List<ProductTrashItemViewModel>> GetTrashAsync();
    Task<bool> RestoreAsync(int id);
}