using projeto_final_bloco_02.Model;
using projeto_final_bloco_02.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace projeto_final_bloco_02.Controller
{
    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
         _produtoService = produtoService;
         _produtoValidator = produtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo(String titulo)
        {
            return Ok (await _produtoService.GetByTitulo(titulo));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Create(produto);

            if (Resposta == null)
            {
                return BadRequest("Produto não encontrado!");
            }

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.Id <= 0)
                return BadRequest("Id do produto inválido");

            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Update(produto);

            if (Resposta == null)
                return NotFound("Produto não encontrado!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var produto = await _produtoService.GetById(id);

            if (produto == null)
                return NotFound("Produto não encontrado!");

            await _produtoService.Delete(produto);
            return NoContent();
        }

    }
}
