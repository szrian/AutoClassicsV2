using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;

namespace SZ.AutoClassics.Site.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize("Admin")]
public class AdminController : Controller
{
	private readonly IAccountAppService _accountAppService;
	private const int REGISTROS_POR_PAGINA = 20;

	public AdminController(IAccountAppService accountAppService)
	{
		_accountAppService = accountAppService;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public IActionResult ListarUsuarios(int pagina = 0)
	{
		var usuariosFiltrosViewObject = _accountAppService.ObterTodosUsuarios(User, pagina, REGISTROS_POR_PAGINA);
		usuariosFiltrosViewObject.Pagina = pagina;

		return View(usuariosFiltrosViewObject);
	}

	[HttpGet]
	public IActionResult ListarUsuariosBloqueados(int pagina = 0)
	{
		var usuariosFiltrosViewObject = _accountAppService.ObterUsuariosBloqueados(pagina, REGISTROS_POR_PAGINA);
		usuariosFiltrosViewObject.Pagina = pagina;

		return View(usuariosFiltrosViewObject);
	}

	[HttpGet]
	public IActionResult BloquearUsuario(string id)
	{
		if (id == string.Empty || id == null)
			ModelState.AddModelError(string.Empty, "Este usuário não existe");

		var usuario = _accountAppService.ObterUsuarioPorId(id);

		if (usuario == null)
			ModelState.AddModelError(string.Empty, "Usuário não encontrado");

		return View(usuario);
	}

	[HttpPost]
	public async Task<IActionResult> BloquearUsuario(ApplicationUserViewModel usuarioVM, string id)
	{
		if (usuarioVM.Id != id)
			ModelState.AddModelError(string.Empty, "Erro ao bloquear usuário");

		var resultado = await _accountAppService.BloquearUsuario(id);

		if (!resultado)
			ModelState.AddModelError(string.Empty, "Não foi possível bloquear o usuário");

		return RedirectToAction("ListarUsuarios");
	}

	public IActionResult DesbloquearUsuario(string id)
	{
		if (id == string.Empty || id == null)
			ModelState.AddModelError(string.Empty, "Este usuário não existe");

		var usuario = _accountAppService.ObterUsuarioPorId(id);

		if (usuario == null)
			ModelState.AddModelError(string.Empty, "Usuário não encontrado");

		return View(usuario);
	}

	[HttpPost]
	public async Task<IActionResult> DesbloquearUsuario(ApplicationUserViewModel usuarioVM, string id)
	{
		if (usuarioVM.Id != id)
			ModelState.AddModelError(string.Empty, "Erro ao desbloquear usuário");

		var resultado = await _accountAppService.DesbloquearUsuario(id);

		if (!resultado)
			ModelState.AddModelError(string.Empty, "Não foi possível desbloquear o usuário");

		return RedirectToAction("ListarUsuariosBloqueados");
	}
}
