using AutoMapper;
using System.Security.Claims;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class AnuncioAppService : BaseAppService<AnuncioViewModel, Anuncio>, IAnuncioAppService
{
	private readonly IAnuncioRepository _anuncioRepository;
	private readonly IAnuncioService _anuncioService;
	private readonly IEstadoAppService _estadoAppService;
	private readonly ICidadeAppService _cidadeAppService;
	private readonly IAccountAppService _accountAppService;
	private readonly IRepository<Anuncio> _repository;
	private readonly IMapper _mapper;
	public AnuncioAppService(IBaseService<Anuncio> baseService, IRepository<Anuncio> repository, IMapper mapper,
							 IAnuncioRepository anuncioRepository,
							 IEstadoAppService estadoAppService,
							 ICidadeAppService cidadeAppService,
							 IAnuncioService anuncioService,
							 IAccountAppService accountAppService)
							 : base(baseService, repository, mapper)
	{
		_repository = repository;
		_mapper = mapper;
		_anuncioRepository = anuncioRepository;
		_anuncioService = anuncioService;
		_estadoAppService = estadoAppService;
		_cidadeAppService = cidadeAppService;
		_accountAppService = accountAppService;
	}

	public async Task<AnuncioViewModel> Adicionar(AnuncioViewModel anuncioVM)
	{
		anuncioVM.Ativo = true;
		anuncioVM.DataCriacao = DateTime.Now;

		var anuncioRetorno = await _anuncioService.Adicionar(_mapper.Map<Anuncio>(anuncioVM));

		return _mapper.Map<AnuncioViewModel>(anuncioRetorno);
	}

	public async Task<AnuncioViewModel> Atualizar(AnuncioViewModel anuncioVM)
	{
		var anuncio =  await _anuncioService.Atualizar(_mapper.Map<Anuncio>(anuncioVM));
		return _mapper.Map<AnuncioViewModel>(anuncio);
	}

	public async Task Excluir(Guid anuncioId, ClaimsPrincipal usuarioLogado)
	{
		var usuarioLogadoId = _accountAppService.ObterUsuarioLogadoId(usuarioLogado);

		await _anuncioService.Remover(anuncioId, usuarioLogadoId, usuarioLogado.IsInRole("Admin"));
	}

	public async Task<AnuncioViewModel> ObterAnuncioDetalhado(Guid anuncioId)
	{
		var anuncioVM = await ObterPorId(anuncioId);
		anuncioVM.CaminhoImagens = TratarCaminhoDasImagens(anuncioVM.ImagemUrl.Split(";"));
		anuncioVM.Uf = _estadoAppService.ObterPorId(anuncioVM.EstadoId).Result.UF;
		anuncioVM.CidadeAnuncio = _cidadeAppService.ObterPorId(anuncioVM.CidadeId).Result.Descricao;
		anuncioVM.Usuario = _accountAppService.ObterUsuarioPorId(anuncioVM.UsuarioId);

		return anuncioVM;
	}

	public IEnumerable<AnuncioViewModel> ObterAnunciosPorUsuarioId(string id, int pagina, int quantidadeRegistros)
	{
		return _mapper.Map<IEnumerable<AnuncioViewModel>>(_anuncioRepository.ObterAnunciosPorUsuarioId(id, pagina, quantidadeRegistros));
	}

	public async Task<IEnumerable<AnuncioViewModel>> ObterUltimosAnuncios()
	{
		return _mapper.Map<IEnumerable<AnuncioViewModel>>(await _anuncioRepository.ObterUltimosAnuncios());
	}

	private List<string> TratarCaminhoDasImagens(string[] imagens)
	{
		var urlImagens = new List<string>();

		foreach (var imagem in imagens)
			if (imagem != string.Empty)
				urlImagens.Add(imagem);

		return urlImagens;
	}
}
