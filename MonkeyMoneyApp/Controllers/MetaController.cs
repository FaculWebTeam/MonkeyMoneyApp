using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyMoneyApp.Repository.Interface;

namespace ApiMonkeyMoney.Controllers
{
    [Route("[controller]")]
    public class MetaController : Controller
    {
        private readonly IMetaRepository _repository;

        public MetaController(IMetaRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var metas = await _repository.GetMetas();
            return View(metas);
        }
        [Authorize]
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var metas = await _repository.GetByName(name);
            if (metas == null || !metas.Any())
            {
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
            if (!ModelState.IsValid)
            {
                return View(meta);
            }

            await _repository.Post(meta);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var meta = await _repository.GetMetaById(id);
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

            if (!ModelState.IsValid)
            {
                return View(meta);
            }

            await _repository.Put(id, meta);
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var meta = await _repository.GetMetaById(id);
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
