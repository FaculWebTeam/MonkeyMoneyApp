using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MonkeyMoneyApp.Repository.Interface;
using System.Linq;
using System.Threading.Tasks;

[Route("[controller]")]
public class BancoController : Controller
{
    private readonly IBancoRepository _repository;
    private readonly UserManager<IdentityUser> _userManager;

    public BancoController(IBancoRepository repository, UserManager<IdentityUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var bancos = await _repository.GetBancosByUserId(userId);
        return View(bancos);
    }

    [Authorize]
    [HttpGet("GetByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        var userId = _userManager.GetUserId(User);
        var bancos = await _repository.GetBancoByName(name, userId);
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
        if (banco == null)
        {
            return BadRequest("Banco inválido.");
        }
        banco.UserId = _userManager.GetUserId(User);
        ModelState.Remove("UserId");
        if (!ModelState.IsValid)
        {
            return View(banco);
        }
        await _repository.Post(banco, banco.UserId);
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = _userManager.GetUserId(User);
        var banco = await _repository.GetBancoById(id, userId);
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
        var userId = _userManager.GetUserId(User);
        banco.UserId = userId;
        ModelState.Remove("UserId");
        if (!ModelState.IsValid)
        {
            return View(banco);
        }
        await _repository.Put(id, banco, userId);
        TempData["SuccessMessage"] = "Banco atualizado com sucesso!";
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = _userManager.GetUserId(User);
        var banco = await _repository.GetBancoById(id, userId);
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
        var userId = _userManager.GetUserId(User);
        var response = await _repository.Delete(id, userId);
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
