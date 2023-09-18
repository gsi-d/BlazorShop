using BlazorShop.Api.Entities;
using BlazorShop.Api.Mappings;
using BlazorShop.Api.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace BlazorShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        private readonly ICarrinhoCompraRepository carrinhoCompraRepository;
        private readonly IProdutoRepository produtoRepository;
        private ILogger<CarrinhoCompraController> logger;

        public CarrinhoCompraController(ICarrinhoCompraRepository carrinhoCompraRepository, IProdutoRepository produtoRepository, ILogger<CarrinhoCompraController> logger)
        {
            this.carrinhoCompraRepository = carrinhoCompraRepository;
            this.produtoRepository = produtoRepository;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{usuarioId}/GetItens")]
        public async Task<ActionResult<IEnumerable<CarrinhoItemDTO>>> GetItens(string usuarioId)
        {
            try
            {
                var carrinhoItens = await carrinhoCompraRepository.GetItens(usuarioId);
                if (carrinhoItens == null)
                {
                    return NoContent();
                }

                var produtos = await produtoRepository.GetItens();
                if (produtos == null)
                {
                    throw new Exception("Não existem produtos...");
                }

                var carrinhoItensDTO = carrinhoItens.CarrinhoItensToDTO(produtos);
                return Ok(carrinhoItensDTO);

            }
            catch (Exception ex)
            {
                logger.LogError("## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDTO>> GetItem(int id)
        {
            try
            {
                var carrinhoItem = await carrinhoCompraRepository.GetItem(id);
                if (carrinhoItem == null)
                {
                    return NotFound("item não encontrado"); //404 status code
                }

                var produto = await produtoRepository.GetItem(carrinhoItem.ProdutoId);
                if (produto == null)
                {
                    return NotFound("item não existe na fonte de dados");
                }

                var carrinhoItemDTO = carrinhoItem.CarrinhoItemToDTO(produto);
                return Ok(carrinhoItemDTO);
            }
            catch (Exception ex)
            {
                logger.LogError($"## Erro ao obter o item ={id} do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItemDTO>> PostItem([FromBody] CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            try
            {
                var novoCarrinhoItem = await carrinhoCompraRepository.AdicionaItem(carrinhoItemAdicionaDTO);
                if(novoCarrinhoItem == null)
                {
                    return NoContent(); //Status 204
                }

                var produto = await produtoRepository.GetItem(novoCarrinhoItem.ProdutoId);
                if(produto == null)
                {
                    throw new Exception($"Produto não localizado (Id:{carrinhoItemAdicionaDTO.ProdutoId})");
                }

                var novoCarrinhoItemDTO = novoCarrinhoItem.CarrinhoItemToDTO(produto);

                return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoItemDTO.Id, novoCarrinhoItemDTO });
            }
            catch (Exception ex)
            {
                logger.LogError($"## Erro ao criar um novo item no carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
