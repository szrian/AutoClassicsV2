using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SZ.AutoClassics.Aplicacao.Interface;

namespace SZ.AutoClassics.Site.Controllers;

[Authorize]
public class AnuncioSalvoController : Controller
{
    private readonly IAnuncioSalvoAppService _anuncioSalvoAppService;
    private readonly IAccountAppService _accountAppService;

    private const int REGISTROS_POR_PAGINA = 10;
    public AnuncioSalvoController(IAnuncioSalvoAppService anuncioSalvoAppService, IAccountAppService accountAppService)
    {
		_anuncioSalvoAppService = anuncioSalvoAppService;
		_accountAppService = accountAppService;
    }

    public async Task<IActionResult> AnunciosSalvos(int pagina = 0)
    {
        var usuarioId = _accountAppService.ObterUsuarioLogadoId(User);
        var anunciosSalvos = await _anuncioSalvoAppService.ObterPorUsuarioPaginado(usuarioId, pagina, REGISTROS_POR_PAGINA);

        return View(anunciosSalvos);
    }

    [HttpPost]
    public async Task SalvarAnuncio(Guid anuncioId)
    {
        var usuarioId = _accountAppService.ObterUsuarioLogadoId(User);
		await _anuncioSalvoAppService.SalvarAnuncio(anuncioId, usuarioId);
    }

    [HttpPost]
    public async Task RemoverAnuncioSalvo(Guid anuncioId)
    {
        var usuarioId = _accountAppService.ObterUsuarioLogadoId(User);
        await _anuncioSalvoAppService.RemoverAnuncio(anuncioId, usuarioId);
    }
}
