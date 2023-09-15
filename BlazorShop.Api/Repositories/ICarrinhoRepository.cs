using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Repositories
{
    public interface ICarrinhoRepository
    {
        Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO);
        Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDTO carrinhoItemAtualizaQtdDTO);
        Task<CarrinhoItem> DeletaItem(int id);
        Task<CarrinhoItem> GetItem(int id);
        Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId);
    }
}
