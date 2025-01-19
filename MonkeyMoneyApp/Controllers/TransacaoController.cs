using ApiMonkeyMoney.Models;
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

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var transacoes = await _repository.GetTransacoes();
            return View(transacoes);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var transacao = await _repository.GetTransacoesById(id);
            if (transacao == null)
            {
                return NotFound();
            }

            return View(transacao);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Bancos = await _bancoRepository.GetBancos();
            return View();
        }

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
