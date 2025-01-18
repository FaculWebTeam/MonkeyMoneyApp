using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;
using System.Collections;

namespace MonkeyMoneyApp.Repository
{
    public class BancoRepository : IBancoRepository
    {
        private readonly ApplicationDbContext _context;

        public BancoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Banco> Delete(int id)
        {
            var banco = await _context.Bancos.FindAsync(id);
            if (banco == null)
            {
                return null;
            }
            _context.Bancos.Remove(banco);
            await _context.SaveChangesAsync();
            return banco;
        }
        public async Task<Banco> GetBancoById(int id)
        {
            return await _context.Bancos.FindAsync(id);
        }

        public Task<List<Banco>> GetBancos()
        {
            return _context.Bancos.FromSqlRaw("SELECT * FROM Bancos").ToListAsync();
        }

        public async Task<Banco> Post([FromBody] Banco banco)
        {
            await _context.Bancos.AddAsync(banco);
            await _context.SaveChangesAsync();
            return banco;
        }

        public async Task<Banco> Put(int id, Banco banco)
        {
            if (id != banco.Id)
            {
                return null;
            }

            var existeBanco = await _context.Bancos.FindAsync(id);
            if (existeBanco == null)
            {
                return null;
            }

            _context.Entry(existeBanco).CurrentValues.SetValues(banco);

            await _context.SaveChangesAsync();

            var bancoAtualizado = await _context.Bancos.FindAsync(id);
            return bancoAtualizado;
        }
    }
}
