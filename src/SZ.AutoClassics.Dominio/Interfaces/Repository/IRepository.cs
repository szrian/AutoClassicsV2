using System.Linq.Expressions;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Repository;

public interface IRepository<TEntidade> : IDisposable where TEntidade : EntidadeBase
{
	Task Adicionar(TEntidade obj);
	Task Atualizar(TEntidade obj);
	Task<IEnumerable<TEntidade>> ObterTodos();
	Task<IEnumerable<TEntidade>> ObterTodosPaginado(int s, int t);
	Task<IEnumerable<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate);
	Task<TEntidade> ObterPorId(Guid id);
	Task Remover(Guid id);
	Task<int> Commit();
}
