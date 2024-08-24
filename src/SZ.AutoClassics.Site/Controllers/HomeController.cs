using Microsoft.AspNetCore.Mvc;
using SZ.AutoClassics.Aplicacao.Interface;

namespace SZ.AutoClassics.Site.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IAnuncioAppService _anuncioAppService;

	public HomeController(ILogger<HomeController> logger, IAnuncioAppService anuncioAppService)
	{
		_logger = logger;
		_anuncioAppService = anuncioAppService;
	}

	public async Task<IActionResult> Index()
	{
		var ultimosAnuncios = await _anuncioAppService.ObterUltimosAnuncios();

		return View(ultimosAnuncios);
	}

	public async Task<IActionResult> Privacy()
	{
		return View();
	}
}
