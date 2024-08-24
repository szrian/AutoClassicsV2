using AutoMapper;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Aplicacao.ViewObjects;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class AnuncioSalvoAppService : BaseAppService<AnuncioSalvoViewModel, AnuncioSalvo>, IAnuncioSalvoAppService
{
	private readonly IAnuncioAppService _anuncioAppService;
	private readonly IAnuncioSalvoService _anuncioSalvoService;
	private readonly IAnuncioSalvoRepository _anuncioSalvoRepository;
	public AnuncioSalvoAppService(IBaseService<AnuncioSalvo> baseService,
		IRepository<AnuncioSalvo> repository,
		IMapper mapper,
		IAnuncioAppService anuncioAppService,
		IAnuncioSalvoService anuncioSalvoService,
		IAnuncioSalvoRepository anuncioSalvoRepository) 
		: base(baseService, repository, mapper)
	{
		_anuncioAppService = anuncioAppService;
		_anuncioSalvoService = anuncioSalvoService;
		_anuncioSalvoRepository = anuncioSalvoRepository;
	}

	public async Task<AnunciosFiltrosViewObject> ObterPorUsuarioPaginado(string usuarioId, int pagina, int registrosPorPagina)
	{
		var anunciosSalvos = await _anuncioSalvoRepository.ObterPorUsuarioPaginado(usuarioId, pagina, registrosPorPagina);
		var anuncios = new List<AnuncioViewModel>();

		foreach (var anuncioSalvo in anunciosSalvos)
			anuncios.Add(await _anuncioAppService.ObterPorId(anuncioSalvo.AnuncioId));

		var anunciosFiltrosVO = new AnunciosFiltrosViewObject
		{
			Anuncios = anuncios,
			Filtros = new FiltrosAnunciosViewObject
			{
				Pagina = pagina,
				TotalRegistros = anuncios.Count()
			}
		};

        return anunciosFiltrosVO;
	}

	public Task SalvarAnuncio(Guid anuncioId, string usuarioId)
		=> _anuncioSalvoService.SalvarAnuncio(new AnuncioSalvo(anuncioId, usuarioId));

	public async Task RemoverAnuncio(Guid anuncioId, string usuarioId)
	{
		var anuncioSalvo = await _anuncioSalvoRepository.ObterAnuncioSalvoPorUsuario(anuncioId, usuarioId);

		if (anuncioSalvo != null)
			await _anuncioSalvoRepository.Remover(anuncioSalvo.Id);
	}
}
