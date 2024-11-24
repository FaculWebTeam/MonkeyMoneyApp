using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;

namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Transacao>> GetTransacoes()
        {
            return _context.Transacoes.FromSqlRaw("SELECT * FROM Transacoes").ToListAsync();
        }

        [HttpGet("{id}")]
        public Task<List<Transacao>> GetTransacoesById(int id)
        {
            return _context.Transacoes.FromSqlInterpolated($"SELECT * FROM Transacoes WHERE Id = {id}").ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Transacao transacao)
        {
            if (transacao == null)
            {
                return BadRequest("Transação inválida.");
            }
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = transacao.Id }, transacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return BadRequest("id inexistente");
            }

            var existeTransacao = await _context.Transacoes.FindAsync(id);
            if (existeTransacao == null)
            {
                return NotFound("Transação não encontrada.");
            }

            _context.Entry(existeTransacao).CurrentValues.SetValues(transacao);

            await _context.SaveChangesAsync();

            var transacaoAtualizada = await _context.Transacoes.FindAsync(id);
            return Ok(transacaoAtualizada);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return NotFound("Transaçao não encontrada.");
            }
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
