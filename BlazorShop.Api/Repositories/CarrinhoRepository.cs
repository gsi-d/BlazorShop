using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Api.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDTO carrinhoItemAtualizaQtdDTO)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItem> DeletaItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItem> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
