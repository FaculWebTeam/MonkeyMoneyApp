using ApiMonkeyMoney.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface IMetaRepository
    {
        Task<List<Meta>> GetMetas();
        Task<Meta> GetMetaById(int id);
        Task<Meta> Post(Meta meta);
        Task<Meta> Put(int id, Meta meta);
        Task<Meta> Delete(int id);
    }
}
