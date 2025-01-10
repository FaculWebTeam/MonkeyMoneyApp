using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;

namespace MonkeyMoneyApp.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public TransacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> Delete(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if (transacao == null)
            {
                return null;
            }
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public Task<List<Transacao>> GetTransacoes()
        {
            return _context.Transacoes.FromSqlRaw("SELECT * FROM Transacoes").ToListAsync();
        }

        public Task<List<Transacao>> GetTransacoesById(int id)
        {
            return _context.Transacoes.FromSqlInterpolated($"SELECT * FROM Transacoes WHERE Id = {id}").ToListAsync();
        }

        public async Task<Transacao> Post([FromBody] Transacao transacao)
        {
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return transacao;
        }

        public async Task<Transacao> Update(int id, [FromBody] Transacao transacao)
        {
            var existeTransacao = await _context.Transacoes.FindAsync(id);
            if (existeTransacao == null)
            {
                return null;
            }

            _context.Entry(existeTransacao).CurrentValues.SetValues(transacao);

            await _context.SaveChangesAsync();

            var transacaoAtualizada = await _context.Transacoes.FindAsync(id);
            return transacaoAtualizada;
        }
    }
}
