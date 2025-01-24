using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MonkeyMoneyApp.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMonkeyMoney.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ITransacaoRepository _repository;
        private readonly IBancoRepository _bancoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public TransacaoController(ITransacaoRepository repository, IBancoRepository bancoRepository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _bancoRepository = bancoRepository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var transacoes = await _repository.GetTransacoesByUserId(userId);
            return View(transacoes);
        }

        [Authorize]
        [HttpGet("GetByTitle")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            var userId = _userManager.GetUserId(User);
            var transacoes = await _repository.GetTransacaoByTitle(title, userId);
            if (transacoes == null || !transacoes.Any())
            {
                ViewBag.Mensagem = "Nenhuma transação encontrada com o título pesquisado.";
                return NotFound();
            }

            return View("Index", transacoes);
        }

        [Authorize]
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var userId = _userManager.GetUserId(User);
            ViewBag.Bancos = await _bancoRepository.GetBancosByUserId(userId);
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Transacao transacao)
        {
            var userId = _userManager.GetUserId(User);
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
            {
                var user = _userManager.GetUserId(User);
                ViewBag.Bancos = await _bancoRepository.GetBancosByUserId(user);
                return View(transacao);
            }
            await _repository.Post(transacao, userId);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transacao = await _repository.GetTransacaoById(id, userId);
            if (transacao == null)
            {
                return NotFound();
            }

            ViewBag.Bancos = await _bancoRepository.GetBancosByUserId(userId);
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
            var userId = _userManager.GetUserId(User);
            transacao.UserId = userId;
            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
            {
                ViewBag.Bancos = await _bancoRepository.GetBancosByUserId(userId);
                return View(transacao);
            }
            await _repository.Update(id, transacao, userId);
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var transacao = await _repository.GetTransacaoById(id, userId);
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
            var userId = _userManager.GetUserId(User);
            var response = await _repository.Delete(id, userId);
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
