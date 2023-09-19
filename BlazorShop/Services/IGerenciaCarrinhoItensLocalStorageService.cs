using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IGerenciaCarrinhoItensLocalStorageService
    {
        Task<List<CarrinhoItemDTO>> GetCollection();
        Task SaveCollection(List<CarrinhoItemDTO> carrinhoItensDTO);
        Task RemoveCollection();
    }
}
