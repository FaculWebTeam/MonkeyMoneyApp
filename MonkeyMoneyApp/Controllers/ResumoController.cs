using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MonkeyMoneyApp.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;
using ApiMonkeyMoney.ViewModels;

namespace ApiMonkeyMoney.Controllers
{
    [Route("[controller]")]
    public class ResumoController : Controller
    {
        private readonly IMetaRepository _metaRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IBancoRepository _bancoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public ResumoController(IMetaRepository metaRepository, ITransacaoRepository transacaoRepository, IBancoRepository bancoRepository, UserManager<IdentityUser> userManager)
        {
            _metaRepository = metaRepository;
            _transacaoRepository = transacaoRepository;
            _bancoRepository = bancoRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var metas = await _metaRepository.GetMetasByUserId(userId);
            var transacoes = await _transacaoRepository.GetTransacoesByUserId(userId);
            var bancos = await _bancoRepository.GetBancosByUserId(userId);

            var totalValorObjetivo = metas.Sum(m => m.ValorObjetivo);
            var totalValorAtual = metas.Sum(m => m.ValorAtual);
            var totalTransacoes = transacoes.Count;
            var totalBancos = bancos.Count; 

            var resumoViewModel = new ResumoViewModel
            {
                TotalValorObjetivo = totalValorObjetivo,
                TotalValorAtual = totalValorAtual,
                TotalTransacoes = totalTransacoes,
                TotalBancos = totalBancos
            };

            return View(resumoViewModel);
        }
    }
}
