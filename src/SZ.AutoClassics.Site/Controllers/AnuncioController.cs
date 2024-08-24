using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Aplicacao.ViewObjects;

namespace SZ.AutoClassics.Site.Controllers;

public class AnuncioController : Controller
{
	private readonly IAnuncioAppService _anuncioAppService;
	private readonly IEstadoAppService _estadoAppService;
	private readonly ICidadeAppService _cidadeAppService;
	private readonly IAccountAppService _accountAppService;

	private const int REGISTROS_POR_PAGINA = 20;
	public AnuncioController(IAnuncioAppService anuncioAppService,
							 IEstadoAppService estadoAppService,
							 ICidadeAppService cidadeAppService,
							 IAccountAppService accountAppService)
	{
		_anuncioAppService = anuncioAppService;
		_estadoAppService = estadoAppService;
		_cidadeAppService = cidadeAppService;
		_accountAppService = accountAppService;
	}

	public async Task<IActionResult> Index(int pagina, FiltrosAnunciosViewObject filtros)
	{
		filtros.Pagina = pagina > 0 ? pagina : 0;

		var anunciosFiltrosViewObject = new AnunciosFiltrosViewObject();
		anunciosFiltrosViewObject.Anuncios = await _anuncioAppService.ObterTodosPaginado(filtros.Pagina, REGISTROS_POR_PAGINA);
		anunciosFiltrosViewObject.Filtros = filtros;

		return View(anunciosFiltrosViewObject);
	}

	[HttpGet]
	[Authorize]
	public IActionResult CriarAnuncio()
	{
		var anuncioViewModel = new AnuncioViewModel();
		anuncioViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
		anuncioViewModel.Cidades = new List<CidadeViewModel>();
		anuncioViewModel.UsuarioId = _accountAppService.ObterUsuarioLogadoId(User);

		return View(anuncioViewModel);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CriarAnuncio(AnuncioViewModel anuncioViewModel)
	{
		if (!ModelState.IsValid)
		{
			anuncioViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
			anuncioViewModel.Cidades = new List<CidadeViewModel>();
			anuncioViewModel.UsuarioId = _accountAppService.ObterUsuarioLogadoId(User);

			return View(anuncioViewModel);
		}

		anuncioViewModel.ImagemUrl = UploadArquivo(anuncioViewModel.Imagens);

		anuncioViewModel = await _anuncioAppService.Adicionar(anuncioViewModel);

		if (!anuncioViewModel.ValidationResult.IsValid)
		{
			AdicionarErrosNaModelState(anuncioViewModel.ValidationResult.Errors);
			anuncioViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
			anuncioViewModel.Cidades = new List<CidadeViewModel>();
			anuncioViewModel.UsuarioId = _accountAppService.ObterUsuarioLogadoId(User);

			return View(anuncioViewModel);
		}

		return RedirectToAction("Index");
	}

	[HttpGet]
	public async Task<IActionResult> AnuncioDetalhes(Guid id)
	{
		if (id == Guid.Empty)
		{
			ModelState.AddModelError(String.Empty, "Não foi possível encontrar nenhum anúncio.");

			return View("Validacao", ModelState.Values.SelectMany(m => m.Errors)
													  .Select(e => e.ErrorMessage)
													  .ToList());
		}
		var anuncioViewModel = await _anuncioAppService.ObterAnuncioDetalhado(id);
		VerificarSeAnuncioEhValido(anuncioViewModel);
		ViewBag.UsuarioLogadoId = _accountAppService.ObterUsuarioLogadoId(User);

		if (!ModelState.IsValid)
			return View("Validacao", ModelState.Values.SelectMany(m => m.Errors)
													  .Select(e => e.ErrorMessage)
													  .ToList());

		return View(anuncioViewModel);
	}
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Editar(Guid id)
	{
		if (id == Guid.Empty)
			ModelState.AddModelError(string.Empty, "Ops... Não encontramos este anúncio :(");
		var anuncioViewModel = await _anuncioAppService.ObterPorId(id);
		anuncioViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
		anuncioViewModel.Cidades = _cidadeAppService.ObterTodos().Result.Where(p => p.Id == anuncioViewModel.CidadeId);

		if (anuncioViewModel == null)
			ModelState.AddModelError(string.Empty, "Ops... Não encontramos este anúncio :(");

		return View(anuncioViewModel);
	}

	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Editar(Guid id, AnuncioViewModel anuncioEditadoViewModel)
	{
		if (id != anuncioEditadoViewModel.Id) return NotFound();

		if (!ModelState.IsValid)
		{
			anuncioEditadoViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
			return View(anuncioEditadoViewModel);
		}

		if (anuncioEditadoViewModel.Imagens != null)
		{
			anuncioEditadoViewModel.ImagemUrl = UploadArquivo(anuncioEditadoViewModel.Imagens);
		}

		var anuncioEditado = await _anuncioAppService.Atualizar(anuncioEditadoViewModel);

		if (!anuncioEditado.ValidationResult.IsValid)
		{
			AdicionarErrosNaModelState(anuncioEditado.ValidationResult.Errors);
			anuncioEditado = PopularAnuncioViewModel(anuncioEditado);
			return View(anuncioEditado);
		}

		anuncioEditado = PopularAnuncioViewModel(anuncioEditado);
		return View(anuncioEditado);
	}

	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Excluir(Guid id)
	{
		if (id == Guid.Empty) return NotFound();

		await _anuncioAppService.Excluir(id, User);

		return RedirectToAction("Index");
	}

	[Authorize]
	public async Task<IActionResult> MeusAnuncios(int pagina, FiltrosAnunciosViewObject filtros)
	{
		filtros.Pagina = pagina > 0 ? pagina : 0;
		var anunciosFiltrosViewObject = new AnunciosFiltrosViewObject();
		anunciosFiltrosViewObject.Anuncios = _anuncioAppService.ObterAnunciosPorUsuarioId(_accountAppService.ObterUsuarioLogadoId(User), pagina, REGISTROS_POR_PAGINA);
		anunciosFiltrosViewObject.Filtros = filtros;

		return View(anunciosFiltrosViewObject);
	}

	private string UploadArquivo(List<IFormFile> arquivos)
	{
		var imagens = string.Empty;
		foreach (var arquivo in arquivos)
		{
			var imgPrefixo = Guid.NewGuid() + "_";

			if (arquivo.Length <= 0)
			{
				ModelState.AddModelError(string.Empty, "Nenhuma imagem selecionada");
				return string.Empty;
			}

			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

			if (System.IO.File.Exists(path))
			{
				ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
				return string.Empty;
			}

			using (var stream = new FileStream(path, FileMode.Create))
			{
				arquivo.CopyTo(stream);
			}

			imagens += imgPrefixo + arquivo.FileName + ";";
		}

		return imagens;
	}

	[HttpGet]
	public IActionResult ObterCidadesPorEstadoId(string estadoId)
	{
		var cidades = _cidadeAppService.ObterCidadesPorEstadoId(Guid.Parse(estadoId)).Result;

		return Json(new SelectList(cidades, "Id", "Descricao"));
	}

	private void VerificarSeAnuncioEhValido(AnuncioViewModel anuncioVM)
	{
		if (anuncioVM == null)
			ModelState.AddModelError(String.Empty, "O anúncio não existe.");

		if (!anuncioVM.Ativo)
			ModelState.AddModelError(String.Empty, "Este anúncio não está mais ativo.");
	}

	private void AdicionarErrosNaModelState(List<ValidationFailure> erros)
	{
		foreach (var erro in erros)
			ModelState.AddModelError(string.Empty, erro.ErrorMessage);
	}

	private AnuncioViewModel PopularAnuncioViewModel(AnuncioViewModel anuncioViewModel)
	{
		anuncioViewModel.Estados = _estadoAppService.ObterTodos().Result.OrderBy(p => p.UF);
		anuncioViewModel.Cidades = _cidadeAppService.ObterTodos().Result.Where(p => p.Id == anuncioViewModel.CidadeId);

		return anuncioViewModel;
	}
}
