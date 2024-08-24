using System.Security.Claims;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Interface;

public interface IAnuncioAppService : IBaseAppService<AnuncioViewModel, Anuncio>
{
	Task<AnuncioViewModel> Adicionar(AnuncioViewModel anuncioVM);
	Task<AnuncioViewModel> Atualizar(AnuncioViewModel anuncioVM);
	Task Excluir(Guid anuncioId, ClaimsPrincipal usuarioLogado);
	Task<AnuncioViewModel> ObterAnuncioDetalhado(Guid anuncioId);
	Task<IEnumerable<AnuncioViewModel>> ObterUltimosAnuncios();
	//IEnumerable<Anuncio> ObterAnunciosFiltrado(FiltrosAnunciosViewModel filtro);
	IEnumerable<AnuncioViewModel> ObterAnunciosPorUsuarioId(string id, int pagina, int quantidadeRegistros);
}
