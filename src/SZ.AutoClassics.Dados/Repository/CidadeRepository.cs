using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.Repository;

public class CidadeRepository : Repository<Cidade>, ICidadeRepository
{
	public CidadeRepository(AppDbContext context) : base(context)
	{ }

	public async Task<IEnumerable<Cidade>> ObterCidadesPorEstadoId(Guid estadoId)
	{
		return Buscar(p => p.EstadoId == estadoId).Result.OrderBy(p => p.Descricao);
	}
}
