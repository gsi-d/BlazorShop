using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        public HttpClient _httpClient;
        public ILogger<ProdutoService> _logger;

        public ProdutoService(HttpClient httpClient, ILogger<ProdutoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetItens()
        {
            try
            {
                var produtosDto = await _httpClient.GetFromJsonAsync<IEnumerable<ProdutoDTO>>("api/produtos");
                return produtosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao acessar produtos: api/produtos");
                throw;
            }
        }
        
        public async Task<ProdutoDTO> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/produtos/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                        return default(ProdutoDTO);
                    return await response.Content.ReadFromJsonAsync<ProdutoDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Erro ao obter produto pelo id={id} - {message}");
                    throw new Exception($"Status code: {response.StatusCode} - {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter produto pelo id={id}");
                throw;
            }
        }
    }
}
