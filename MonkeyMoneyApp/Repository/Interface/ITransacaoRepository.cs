using ApiMonkeyMoney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> GetTransacoesByUserId(string userId);
        Task<List<Transacao>> GetTransacaoByTitle(string title, string userId);
        Task<Transacao> GetTransacaoById(int id, string userId);
        Task<Transacao> Post(Transacao transacao, string userId);
        Task<Transacao> Update(int id, Transacao transacao, string userId);
        Task<Transacao> Delete(int id, string userId);
    }
}
