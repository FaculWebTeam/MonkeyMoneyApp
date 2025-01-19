using ApiMonkeyMoney.Models;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository
{
    public class BancoRepository : IBancoRepository
    {
        private readonly ApplicationDbContext _context;

        public BancoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Banco> Delete(int id, string userId)
        {
            var banco = await _context.Bancos.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (banco == null)
            {
                return null;
            }
            _context.Bancos.Remove(banco);
            await _context.SaveChangesAsync();
            return banco;
        }

        public async Task<Banco> GetBancoById(int id, string userId)
        {
            return await _context.Bancos.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
        }

        public async Task<List<Banco>> GetBancosByUserId(string userId)
        {
            return await _context.Bancos.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<List<Banco>> GetBancoByName(string name, string userId)
        {
            return await _context.Bancos
                                 .Where(b => b.UserId == userId && EF.Functions.Like(b.Nome, $"%{name}%"))
                                 .ToListAsync();
        }

        public async Task<Banco> Post(Banco banco, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("UserId não pode ser nulo ou vazio.");
            }
            _context.Bancos.Add(banco);
            await _context.SaveChangesAsync();
            return banco;
        }

        public async Task<Banco> Put(int id, Banco banco, string userId)
        {
            if (id != banco.Id)
            {
                return null;
            }

            var existeBanco = await _context.Bancos.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (existeBanco == null)
            {
                return null;
            }

            _context.Entry(existeBanco).CurrentValues.SetValues(banco);

            await _context.SaveChangesAsync();

            var bancoAtualizado = await _context.Bancos.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            return bancoAtualizado;
        }
    }
}
