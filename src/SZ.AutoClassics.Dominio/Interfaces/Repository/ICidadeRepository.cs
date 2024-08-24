using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Repository;

public interface ICidadeRepository : IRepository<Cidade>
{
	Task<IEnumerable<Cidade>> ObterCidadesPorEstadoId(Guid estadoId);
}
