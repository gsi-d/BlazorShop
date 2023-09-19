using Blazored.LocalStorage;
using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public class GerenciaCarrinhoItensLocalStorageService : IGerenciaCarrinhoItensLocalStorageService
    {
        private const string key = "CarrinhoItemCollection";

        private readonly ILocalStorageService _localStorageService;
        private readonly ICarrinhoCompraService _carrinhoCompraService;

        public GerenciaCarrinhoItensLocalStorageService(ILocalStorageService localStorageService, ICarrinhoCompraService carrinhoCompraService)
        {
            _localStorageService = localStorageService;
            _carrinhoCompraService = carrinhoCompraService;
        }

        public async Task<List<CarrinhoItemDTO>> GetCollection()
        {
            return await this._localStorageService.GetItemAsync<List<CarrinhoItemDTO>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this._localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CarrinhoItemDTO> carrinhoItensDTO)
        {
            await this._localStorageService.SetItemAsync(key, carrinhoItensDTO);
        }

        private async Task<List<CarrinhoItemDTO>> AddCollection()
        {
            var carrinhoCompraCollection = await this._carrinhoCompraService.GetItens(UsuarioLogado.UsuarioId);
            if (carrinhoCompraCollection != null)
                await this._localStorageService.SetItemAsync(key, carrinhoCompraCollection);
            return carrinhoCompraCollection;
        }
    }
}
