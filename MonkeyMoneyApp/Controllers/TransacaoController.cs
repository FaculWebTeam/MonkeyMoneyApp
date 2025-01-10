using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;

namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoRepository _repository;

        public TransacaoController(ITransacaoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public Task<List<Transacao>> GetTransacoes()
        {
            return _repository.GetTransacoes();
        }

        [HttpGet("{id}")]
        public Task<List<Transacao>> GetTransacoesById(int id)
        {
            return _repository.GetTransacoesById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transacao transacao)
        {
            if (transacao == null)
            {
                return BadRequest("Transação inválida.");
            }
            await _repository.Post(transacao);

            return CreatedAtAction(nameof(Post), new { id = transacao.Id }, transacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return BadRequest("id inexistente");
            }

            var transacaoAtualizada = await _repository.Update(id, transacao);
            if (transacaoAtualizada != null)
            {
                return Ok(transacaoAtualizada);
            }
            else
            {
                return BadRequest("Erro ao atualizar transacao");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transacao = await _repository.Delete(id);
            if (transacao != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
