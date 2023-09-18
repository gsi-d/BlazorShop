using BlazorShop.Api.Context;
using BlazorShop.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.Api.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetItens()
        {
            var produto = await _context.Produtos.Include(x => x.Categoria).ToListAsync();

            return produto;
        }

        public async Task<Produto> GetItem(int id)
        {
            var produto = await _context.Produtos.Include(x => x.Categoria).SingleOrDefaultAsync(x => x.Id == id);

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetItensPorCategoria(int id)
        {
            var produto = await _context.Produtos.Include(x => x.Categoria).Where(x => x.CategoriaId == id).ToListAsync();

            return produto;
        }

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }
    }
}
