using MonkeyMoneyApp.Data;
using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BancoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Banco>> GetBancos()
        {
            return _context.Bancos.FromSqlRaw("SELECT * FROM Bancos").ToListAsync();
        }

        [HttpGet("{id}")]
        public Task<List<Banco>> GetBancoById(int id)
        {
            return _context.Bancos.FromSqlInterpolated($"SELECT * FROM Bancos WHERE Id = {id}").ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Banco banco)
        {
            if (banco == null)
            {
                return BadRequest("Banco inválido.");
            }
            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = banco.Id }, banco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Banco banco)
        {
            if (id != banco.Id)
            {
                return BadRequest("id inexistente");
            }

            var existeBanco = await _context.Bancos.FindAsync(id);
            if (existeBanco == null)
            {
                return NotFound("Banco não encontrado.");
            }

            _context.Entry(existeBanco).CurrentValues.SetValues(banco);

            await _context.SaveChangesAsync();

            var bancoAtualizado = await _context.Bancos.FindAsync(id);
            return Ok(bancoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var banco = await _context.Bancos.FindAsync(id);
            if (banco == null)
            {
                return NotFound("Banco não encontrado. ");
            }
            _context.Bancos.Remove(banco);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
