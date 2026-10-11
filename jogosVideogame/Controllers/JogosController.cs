using jogosVideogame.Data;
using jogosVideogame.Models;
using jogosVideogame.Services;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

namespace jogosVideogame.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly JogosService jogosService;

        public JogosController(JogosService jogosService)
        {
            this.jogosService = jogosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tudoJogo = await jogosService.GetAllJogosAsync();
            return Ok(tudoJogo);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdJogos(string id)
        {
            var jogo = await jogosService.GetJogoById(id);
            if (jogo == null)
            {
                return NotFound();
            }
            return Ok(jogo);
        }

        //api/jogos/busca? plataforma = { plataforma }&precoMaximo={preco
        [HttpGet("busca")]
        public async Task<IActionResult> GetBuscarPlataformaPreco([FromQuery] string plataforma, [FromQuery] decimal preco)
        {
            var buscar = await jogosService.GetBuscarPlataformaPreco(plataforma, preco);
            return Ok(buscar);
        }

        //GET /api/jogos/relatorio-estoque
        [HttpGet("relatorio-estoque")]
        public async Task<IActionResult> GetRelatorioEstoque()
        {
            var relatorio = await jogosService.GetRelatorioEstoquesAsync();
            return Ok(relatorio);
        }

        [HttpPost]
        public async Task<IActionResult> AddJogo(Jogo jogo)
        {
            await jogosService.AddJogo(jogo);
            return CreatedAtAction(nameof(GetByIdJogos), new { id = jogo.Id }, jogo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJogo(string id, Jogo jogo)
        {
            if (id != jogo.Id)
            {
                return BadRequest("O id da url é diferente do ID do jogo");
            }

            var existe = await jogosService.GetJogoById(id);
            if (existe == null)
            {
                return NotFound("Jogo não encontrado");
            }

            await jogosService.UpdateJogo(jogo);

            return Ok("atualizado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogo(string id)
        {
            var jogo = await jogosService.GetJogoById(id);
            if (jogo == null)
            {
                return NotFound("Id nao encontrado");
            }
            await jogosService.DeleteJogo(id);
            return NoContent();
        }

    }
}
