using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Repository;

public interface IAnuncioRepository : IRepository<Anuncio>
{
	Task<IEnumerable<Anuncio>> ObterUltimosAnuncios();
	//IEnumerable<Anuncio> ObterAnunciosFiltrado(FiltrosAnunciosViewModel filtro);
	IEnumerable<Anuncio> ObterAnunciosPorUsuarioId(string usuarioId, int pagina, int quantidadeRegistros);
}
