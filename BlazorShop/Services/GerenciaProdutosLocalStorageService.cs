using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public class GerenciaProdutosLocalStorageService : IGerenciaProdutosLocalStorageService
    {
        private const string key = "ProdutoCollection";

        private readonly ILocalStorageService _localStorageService;
        private readonly IProdutoService _produtoService;

        public GerenciaProdutosLocalStorageService(ILocalStorageService localStorageService, IProdutoService produtoService)
        {
            _localStorageService = localStorageService;
            _produtoService = produtoService;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<IEnumerable<ProdutoDTO>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this._localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProdutoDTO>> AddCollection()
        {
            var produtoCollection = await this._produtoService.GetItens();
            if (produtoCollection != null)
                await this._localStorageService.SetItemAsync(key, produtoCollection);
            return produtoCollection;
        }
    }
}
