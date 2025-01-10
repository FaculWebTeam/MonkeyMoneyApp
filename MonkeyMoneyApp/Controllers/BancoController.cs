using MonkeyMoneyApp.Data;
using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Repository.Interface;


namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoRepository _repository;

        public BancoController(IBancoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public Task<List<Banco>> GetBancos()
        {
            return _repository.GetBancos();
        }

        [HttpGet("{id}")]
        public Task<List<Banco>> GetBancoById(int id)
        {
            return _repository.GetBancoById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Banco banco)
        {
            if (banco == null)
            {
                return BadRequest("Banco inválido.");
            }
            await _repository.Post(banco);

            return CreatedAtAction(nameof(Post), new { id = banco.Id }, banco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Banco banco)
        {
            if (id != banco.Id)
            {
                return BadRequest("id inexistente");
            }

            var bancoAtualizado = await _repository.Put(id, banco);

            return Ok(bancoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _repository.Delete(id);
            if (response != null)
            {
                return Ok(response);
            }
            else 
            {
                return BadRequest("Erro na deleção");
            }
        }
    }
}
