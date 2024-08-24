using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Repository;

public interface IAnuncioSalvoRepository : IRepository<AnuncioSalvo>
{
	Task<IEnumerable<AnuncioSalvo>> ObterPorUsuarioPaginado(string usuarioId, int pagina, int registrosPorPagina);
	Task<bool> VerificarExistencia(Guid anuncioId, string usuarioId);
	Task<AnuncioSalvo> ObterAnuncioSalvoPorUsuario(Guid anuncioId, string usuarioId);
}
