using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface IMetaRepository
    {
        Task<List<Meta>> GetMetas();
        Task<List<Meta>> GetMetaById(int id);
        Task<Meta> Post([FromBody] Meta meta);
        Task<Meta> Put(int id, [FromBody] Meta meta);
        Task<Meta> Delete(int id);
    }
}
