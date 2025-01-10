using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;


namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly IMetaRepository _repository;

        public MetaController(IMetaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public Task<List<Meta>> GetMetas()
        {
            return _repository.GetMetas();
        }

        [HttpGet("{id}")]
        public Task<List<Meta>> GetMetaById(int id)
        {
            return _repository.GetMetaById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Meta meta)
        {
            if (meta == null)
            {
                return BadRequest("Meta inválida");
            }
            var metaPost = await _repository.Post(meta);

            return CreatedAtAction(nameof(Post), new { id = meta.Id }, meta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Meta meta)
        {
            var metaAtualizada = await _repository.Put(id, meta);
            if (metaAtualizada != null)
            {
                return Ok(metaAtualizada);
            }
            else
            {
                return BadRequest("Erro ao atualizar meta");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meta = await _repository.Delete(id);
            if (meta != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Erro ao deletar meta");
            }
        }
    }
}
