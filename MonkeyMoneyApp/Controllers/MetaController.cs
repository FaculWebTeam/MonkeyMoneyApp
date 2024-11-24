using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;


namespace ApiMonkeyMoney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Meta>> GetMetas()
        {
            return _context.Metas.FromSqlRaw("SELECT * FROM Metas").ToListAsync();
        }

        [HttpGet("{id}")]
        public Task<List<Meta>> GetMetaById(int id)
        {
            return _context.Metas.FromSqlInterpolated($"SELECT * FROM Metas WHERE Id = {id}").ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Meta meta)
        {
            if (meta == null)
            {
                return BadRequest("Meta inválida.");
            }
            await _context.Metas.AddAsync(meta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = meta.Id }, meta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Meta meta)
        {
            var existeMeta = await _context.Metas.FindAsync(id);
            if(existeMeta == null)
            {
                return NotFound("Meta não encontrada.");
            }
            _context.Entry(existeMeta).CurrentValues.SetValues(meta);
            await _context.SaveChangesAsync();

            var metaAtualizada = await _context.Metas.FindAsync(id);
            return Ok(metaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return NotFound("Meta não encontrada.");
            }
            _context.Remove(meta);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
