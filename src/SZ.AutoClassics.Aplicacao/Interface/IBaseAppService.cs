using System.Linq.Expressions;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Interface;

public interface IBaseAppService<TViewModel, TEntidade> where TEntidade : EntidadeBase
{
	Task<IEnumerable<TViewModel>> ObterTodos();
	Task<IEnumerable<TViewModel>> ObterTodosPaginado(int s, int t);
	Task<IEnumerable<TViewModel>> Buscar(Expression<Func<TViewModel, bool>> predicate);
	Task<TViewModel> ObterPorId(Guid id);
	Task Remover(Guid id);
}
