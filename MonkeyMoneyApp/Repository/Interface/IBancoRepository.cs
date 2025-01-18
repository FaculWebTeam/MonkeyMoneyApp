using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface IBancoRepository
    {
        Task<List<Banco>> GetBancos();
        Task<Banco> GetBancoById(int id);
        Task<Banco> Post(Banco banco);
        Task<Banco> Put(int id, [FromBody] Banco banco);
        Task<Banco> Delete(int id);
    }
}
