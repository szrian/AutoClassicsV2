using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Interface;

public interface ICidadeAppService : IBaseAppService<CidadeViewModel, Cidade>
{
	Task<IEnumerable<CidadeViewModel>> ObterCidadesPorEstadoId(Guid estadoId);
}
