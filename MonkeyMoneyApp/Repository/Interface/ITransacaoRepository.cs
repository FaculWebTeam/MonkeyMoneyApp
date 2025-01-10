﻿using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;

namespace MonkeyMoneyApp.Repository.Interface
{
    public interface ITransacaoRepository
    {
        Task<List<Transacao>> GetTransacoes();
        Task<List<Transacao>> GetTransacoesById(int id);
        Task<Transacao> Post([FromBody] Transacao transacao);
        Task<Transacao> Update(int id, [FromBody] Transacao transacao);
        Task<Transacao> Delete(int id);
    }
}
