using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Aplicacao.ViewObjects;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Interface;

public interface IAnuncioSalvoAppService : IBaseAppService<AnuncioSalvoViewModel, AnuncioSalvo>
{
	Task<AnunciosFiltrosViewObject> ObterPorUsuarioPaginado(string usuarioId, int pagina, int registrosPorPagina);
	Task SalvarAnuncio(Guid anuncioId, string usuarioId);
	Task RemoverAnuncio(Guid anuncioId, string usuarioId);
}
