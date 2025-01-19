using ApiMonkeyMoney.Models;
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

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var metas = await _repository.GetMetas();
            return View(metas);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var meta = await _repository.GetMetaById(id);
            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

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
