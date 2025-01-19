using ApiMonkeyMoney.Models;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository
{
    public class MetaRepository : IMetaRepository
    {
        private readonly ApplicationDbContext _context;

        public MetaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Meta> Delete(int id, string userId)
        {
            var meta = await _context.Metas.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (meta == null)
            {
                return null;
            }
            _context.Metas.Remove(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public async Task<Meta> GetMetaById(int id, string userId)
        {
            return await _context.Metas.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
        }

        public async Task<List<Meta>> GetMetasByUserId(string userId)
        {
            return await _context.Metas.Where(m => m.UserId == userId).ToListAsync();
        }

        public async Task<List<Meta>> GetByName(string name, string userId)
        {
            return await _context.Metas
                                 .Where(m => m.UserId == userId && EF.Functions.Like(m.Nome, $"%{name}%"))
                                 .ToListAsync();
        }

        public async Task<Meta> Post(Meta meta, string userId)
        {
            meta.UserId = userId;
            await _context.Metas.AddAsync(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public async Task<Meta> Put(int id, Meta meta, string userId)
        {
            var existeMeta = await _context.Metas.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (existeMeta == null)
            {
                return null;
            }

            _context.Entry(existeMeta).CurrentValues.SetValues(meta);

            await _context.SaveChangesAsync();

            var metaAtualizada = await _context.Metas.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            return metaAtualizada;
        }
    }
}
