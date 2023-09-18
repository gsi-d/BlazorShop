﻿using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using BlazorShop.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories
{
    public class CarrinhoCompraRepository : ICarrinhoCompraRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoCompraRepository(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> CarrinhoItemJaExiste(int carrinhoId, int produtoId)
        {
            return await _context.CarrinhoItens.AnyAsync(x => x.CarrinhoId == carrinhoId && x.ProdutoId == produtoId);
        }

        public async Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            if(await CarrinhoItemJaExiste(carrinhoItemAdicionaDTO.CarrinhoId, carrinhoItemAdicionaDTO.ProdutoId) == false)
            {
                //verifica se o produto existe
                //cria um novo item no carrinho
                var item = await (from produto in _context.Produtos
                                  where produto.Id == carrinhoItemAdicionaDTO.ProdutoId
                                  select new CarrinhoItem
                                  {
                                      CarrinhoId = carrinhoItemAdicionaDTO.CarrinhoId,
                                      ProdutoId = produto.Id,
                                      Quantidade = carrinhoItemAdicionaDTO.Quantidade
                                  }).SingleOrDefaultAsync();

                //se o item existe então incluir o item no carrinho
                if(item is not null)
                {
                    var resultado = await _context.CarrinhoItens.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return resultado.Entity;
                }
            }
            return null;
        }

        public async Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDTO carrinhoItemAtualizaQtdDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<CarrinhoItem> DeletaItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CarrinhoItem> GetItem(int id)
        {
            return await (from carrinho in _context.Carrinhos
                          join carrinhoItem in _context.CarrinhoItens
                          on carrinho.Id equals carrinhoItem.CarrinhoId
                          where carrinhoItem.Id == id
                          select new CarrinhoItem
                          {
                              Id = carrinhoItem.Id,
                              ProdutoId = carrinhoItem.ProdutoId,
                              Quantidade = carrinhoItem.Quantidade,
                              CarrinhoId = carrinhoItem.CarrinhoId
                          })
                          .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId)
        {
            return await (from carrinho in _context.Carrinhos
                          join carrinhoItem in _context.CarrinhoItens
                          on carrinho.Id equals carrinhoItem.CarrinhoId
                          where carrinho.UsuarioId == usuarioId
                          select new CarrinhoItem
                          {
                              Id = carrinhoItem.Id,
                              ProdutoId = carrinhoItem.ProdutoId,
                              Quantidade = carrinhoItem.Quantidade,
                              CarrinhoId = carrinhoItem.CarrinhoId
                          })
                          .ToListAsync();
        }
    }
}