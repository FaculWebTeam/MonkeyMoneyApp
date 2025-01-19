using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface IBancoRepository
    {
        Task<List<Banco>> GetBancosByUserId(string userId);
        Task<List<Banco>> GetBancoByName(string name, string userId);
        Task<Banco> GetBancoById(int id, string userId);
        Task<Banco> Post(Banco banco, string userId);
        Task<Banco> Put(int id, Banco banco, string userId);
        Task<Banco> Delete(int id, string userId);
    }
}
