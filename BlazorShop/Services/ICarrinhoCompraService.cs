using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface ICarrinhoCompraService
    {
        Task<List<CarrinhoItemDTO>> GetItens(string usuarioId);
        Task<CarrinhoItemDTO> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO);
    }
}
