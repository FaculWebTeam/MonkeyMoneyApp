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
    public class MetaController : Controller
    {
        private readonly IMetaRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public MetaController(IMetaRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var metas = await _repository.GetMetasByUserId(userId);
            return View(metas);
        }

        [Authorize]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var userId = _userManager.GetUserId(User);
            var metas = await _repository.GetByName(name, userId);
            if (metas == null || !metas.Any())
            {
                ViewBag.Mensagem = "Nenhuma meta encontrada com o título pesquisado.";
                return View("Index", new List<Meta>());
            }

            return View("Index", metas);
        }

        [Authorize]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Meta meta)
        {
            var userId = _userManager.GetUserId(User);
            meta.UserId = userId;
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
            {
                return View(meta);
            }
            await _repository.Post(meta, userId);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var meta = await _repository.GetMetaById(id, userId);
            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        [Authorize]
        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Meta meta)
        {
            if (id != meta.Id)
            {
                return BadRequest("ID inválido.");
            }
            var userId = _userManager.GetUserId(User);
            meta.UserId = userId;
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
            {
                return View(meta);
            }
            await _repository.Put(id, meta, userId);
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var meta = await _repository.GetMetaById(id, userId);
            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
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
