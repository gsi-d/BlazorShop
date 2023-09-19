using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IGerenciaProdutosLocalStorageService
    {
        Task<IEnumerable<ProdutoDTO>> GetCollection();
        Task RemoveCollection();
    }
}
