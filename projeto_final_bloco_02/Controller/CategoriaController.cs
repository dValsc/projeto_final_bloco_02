using projeto_final_bloco_02.Model;
using projeto_final_bloco_02.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace projeto_final_bloco_02.Controller
{
    [Route("~/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;

        public CategoriaController(
            ICategoriaService categoriaService,
            IValidator<Categoria> categoriaValidator
            )
        {
            _categoriaService = categoriaService;
            _categoriaValidator = categoriaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _categoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _categoriaService.GetById(id);

            if (Resposta is null)
            {
                return NotFound("Categoria não encontrada!");
            }

            return Ok(Resposta);
        }

        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult> GetByDescricao(string descricao)
        {
            return Ok(await _categoriaService.GetByDescricao(descricao));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Categoria categoria)
        {
            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);

            var Resposta = await _categoriaService.Create(categoria);
            return CreatedAtAction(nameof(GetById), new { id = Resposta.Id }, Resposta);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
                return BadRequest("O Id da Categoria é inválida!");

            var validarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!validarCategoria.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarCategoria);

            var Resposta = await _categoriaService.Update(categoria);

            if (Resposta is null)
                return NotFound("Categoria não encontrada!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaCategoria = await _categoriaService.GetById(id);

            if (BuscaCategoria is null)
                return NotFound("Tema não encontrado!");

            await _categoriaService.Delete(BuscaCategoria);

            return NoContent();

        }
    }
}
