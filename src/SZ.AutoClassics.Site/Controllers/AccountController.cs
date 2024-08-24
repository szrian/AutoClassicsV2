using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;

namespace SZ.AutoClassics.Site.Controllers;

public class AccountController : Controller
{
	private readonly IAccountAppService _accountAppService;
    public AccountController(IAccountAppService accountAppService)
    {
        _accountAppService = accountAppService;
    }
    public IActionResult Login(string urlRetorno)
	{
		return View(new LoginViewModel()
		{
			UrlRetorno = urlRetorno
		});
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel loginVM)
	{
		if (!ModelState.IsValid) return View(loginVM);

		var retorno = await _accountAppService.EfetuarLogin(loginVM);

		if (retorno != string.Empty)
		{
			ModelState.AddModelError(string.Empty, retorno);
			return View(loginVM);
		}

		if (string.IsNullOrEmpty(loginVM.UrlRetorno)) return RedirectToAction("Index", "Home");

		return Redirect(loginVM.UrlRetorno);
	}

	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegistroViewModel registroVM)
	{
		if (!ModelState.IsValid) return View(registroVM);

		var retorno = await _accountAppService.RegistrarUsuario(registroVM);

		if (retorno != string.Empty)
		{
			ModelState.AddModelError(string.Empty, retorno);
			return View(registroVM);
		}

		return RedirectToAction("Login", "Account");
	}

	[Authorize]
	public IActionResult MeuPerfil()
	{
		var usuarioVM = _accountAppService.ObterUsuarioLogado(User);

		return View(usuarioVM);
	}

	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		HttpContext.Session.Clear();
		HttpContext.User = null;
		await _accountAppService.Logout();

		return RedirectToAction("Index", "Home");
	}

	[Authorize]
	[HttpGet]
	public IActionResult Editar(string id)
	{
		var usuarioVM = _accountAppService.ObterUsuarioPorId(id);

		return View(usuarioVM);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Editar(ApplicationUserViewModel usuarioVM, string id)
	{
		if (usuarioVM.Id != id)
			ModelState.AddModelError(string.Empty, "Erro ao identificar o usuário");

		if (!ModelState.IsValid) return View(usuarioVM);

		(var atualizouUsuario, var erro) = await _accountAppService.EditarUsuario(usuarioVM);

		if (!atualizouUsuario)
		{
			ModelState.AddModelError(string.Empty, erro);
			return View(usuarioVM);
		}

		return RedirectToAction("MeuPerfil");
	}
}
