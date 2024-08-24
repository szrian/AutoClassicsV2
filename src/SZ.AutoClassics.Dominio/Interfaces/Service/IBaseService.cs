using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Service;

public interface IBaseService<TEntidade> where TEntidade : EntidadeBase
{
	Task Adicionar(TEntidade obj);
	Task Atualizar(TEntidade obj);
	Task Remover(Guid id);
	Task<int> Commit();
}
