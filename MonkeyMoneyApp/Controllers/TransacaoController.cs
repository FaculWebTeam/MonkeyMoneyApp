using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyMoneyApp.Repository.Interface;

namespace ApiMonkeyMoney.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ITransacaoRepository _repository;
        private readonly IBancoRepository _bancoRepository;

        public TransacaoController(ITransacaoRepository repository, IBancoRepository bancoRepository)
        {
            _repository = repository;
            _bancoRepository = bancoRepository;
        }
        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var transacoes = await _repository.GetTransacoes();
            return View(transacoes);
        }
        [Authorize]
        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var transacoes = await _repository.GetBancoByTitle(title);
            if (transacoes == null || !transacoes.Any())
            {
                return NotFound();
            }

            return View("Index", transacoes);
        }
        [Authorize]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Bancos = await _bancoRepository.GetBancos();
            return View();
        }
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Transacao transacao)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = await _bancoRepository.GetBancos();
                return View(transacao);
            }

            await _repository.Post(transacao);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var transacao = await _repository.GetTransacoesById(id);
            if (transacao == null)
            {
                return NotFound();
            }

            ViewBag.Bancos = await _bancoRepository.GetBancos();
            return View(transacao);
        }
        [Authorize]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Transacao transacao)
        {
            if (id != transacao.Id)
            {
                return BadRequest("ID inválido.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = await _bancoRepository.GetBancos();
                return View(transacao);
            }

            await _repository.Update(id, transacao);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var transacao = await _repository.GetTransacoesById(id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }
        [Authorize]
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var response = await _repository.Delete(id);
            if (response != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Erro na deleção");
            }
        }
    }
}
