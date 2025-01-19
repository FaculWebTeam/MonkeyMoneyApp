using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonkeyMoneyApp.Repository.Interface;

[Route("[controller]")]
public class BancoController : Controller
{
    private readonly IBancoRepository _repository;

    public BancoController(IBancoRepository repository)
    {
        _repository = repository;
    }
    [Authorize]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var bancos = await _repository.GetBancos();
        return View(bancos);
    }
    [Authorize]
    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        var bancos = await _repository.GetBancoByName(name);
        if (bancos == null || !bancos.Any())
        {
            return NotFound();
        }

        return View("Index", bancos); // Retorna a View Index com os resultados da pesquisa
    }
    [Authorize]
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }
    [Authorize]
    [HttpPost("Create")]
    public async Task<IActionResult> Create(Banco banco)
    {
        if (!ModelState.IsValid)
        {
            return View(banco);
        }

        if (banco == null)
        {
            return BadRequest("Banco inválido.");
        }

        await _repository.Post(banco);

        return RedirectToAction("Index");
    }
    [Authorize]
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var banco = await _repository.GetBancoById(id);
        if (banco == null)
        {
            return NotFound();
        }

        return View(banco);
    }
    [Authorize]
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, Banco banco)
    {
        if (id != banco.Id)
        {
            return BadRequest("ID inválido.");
        }

        if (!ModelState.IsValid)
        {
            return View(banco);
        }

        await _repository.Put(id, banco);

        TempData["SuccessMessage"] = "Banco atualizado com sucesso!";
        return RedirectToAction("Index");
    }
    [Authorize]
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var banco = await _repository.GetBancoById(id);
        if (banco == null)
        {
            return NotFound();
        }

        return View(banco);
    }
    [Authorize]
    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        var response = await _repository.Delete(id);
        if (response != null)
        {
            TempData["SuccessMessage"] = "Banco deletado com sucesso!";
            return RedirectToAction("Index");
        }
        else
        {
            return BadRequest("Erro na deleção");
        }
    }
}
