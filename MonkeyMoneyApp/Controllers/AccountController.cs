using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MonkeyMoneyApp.Models;
using System.Threading.Tasks;

namespace MonkeyMoneyApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Ação para exibir a página de registro
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        // Ação para processar o registro de um novo usuário
        [HttpPost]
        public async Task<IActionResult> Registro(Registro model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Senha);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Transacao");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // Ação para exibir a página de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Ação para processar o login de um usuário
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Senha, model.GuardarSenha, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Transacao");
                }

                ModelState.AddModelError(string.Empty, "Login inválido");
            }

            return View(model);
        }

        // Ação para fazer logout do usuário
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Ação para exibir a página de redefinir senha
        [HttpGet]
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        // Ação para processar a redefinição de senha
        [HttpPost]
        public async Task<IActionResult> RedefinirSenha(RedefinirSenhaViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar se o e-mail existe
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Remover a senha atual e adicionar a nova
                    var resetResult = await _userManager.RemovePasswordAsync(user);
                    if (resetResult.Succeeded)
                    {
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NovaSenha);
                        if (addPasswordResult.Succeeded)
                        {
                            return RedirectToAction("Login", "Account"); // Redireciona para a página de login
                        }
                        foreach (var error in addPasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Erro ao remover senha anterior.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail não encontrado.");
                }
            }
            return View(model);
        }
    }
}
