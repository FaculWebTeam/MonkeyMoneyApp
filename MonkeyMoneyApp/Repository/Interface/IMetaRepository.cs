using ApiMonkeyMoney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface IMetaRepository
    {
        Task<List<Meta>> GetMetasByUserId(string userId);
        Task<List<Meta>> GetByName(string name, string userId);
        Task<Meta> GetMetaById(int id, string userId);
        Task<Meta> Post(Meta meta, string userId);
        Task<Meta> Put(int id, Meta meta, string userId);
        Task<Meta> Delete(int id, string userId);
    }
}
