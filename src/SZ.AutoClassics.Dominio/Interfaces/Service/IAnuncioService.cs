using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Service;

public interface IAnuncioService
{
	Task<Anuncio> Adicionar(Anuncio anuncio);
	Task<Anuncio> Atualizar(Anuncio anuncio);
	Task Remover(Guid anuncioId, string usuarioLogadoId, bool ehAdmin);
	Task<int> Commit();
}
