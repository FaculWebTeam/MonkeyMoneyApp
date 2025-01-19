using ApiMonkeyMoney.Models;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _context;

        public TransacaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> Delete(int id, string userId)
        {
            var transacao = await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (transacao == null)
            {
                return null;
            }
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<List<Transacao>> GetTransacoesByUserId(string userId)
        {
            return await _context.Transacoes.Include(t => t.Banco).Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<List<Transacao>> GetTransacaoByTitle(string title, string userId)
        {
            return await _context.Transacoes
                                 .Where(t => t.UserId == userId && EF.Functions.Like(t.Titulo, $"%{title}%"))
                                 .ToListAsync();
        }

        public async Task<Transacao> GetTransacaoById(int id, string userId)
        {
            return await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<Transacao> Post(Transacao transacao, string userId)
        {
            transacao.UserId = userId;
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> Update(int id, Transacao transacao, string userId)
        {
            if (id != transacao.Id)
            {
                return null;
            }

            var existeTransacao = await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (existeTransacao == null)
            {
                return null;
            }

            _context.Entry(existeTransacao).CurrentValues.SetValues(transacao);

            await _context.SaveChangesAsync();

            var transacaoAtualizada = await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            return transacaoAtualizada;
        }
    }
}
