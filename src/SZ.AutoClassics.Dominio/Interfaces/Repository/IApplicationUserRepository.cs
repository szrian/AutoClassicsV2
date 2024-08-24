using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Repository;

public interface IApplicationUserRepository
{
	IEnumerable<ApplicationUser> ObterTodosPaginado(int pagina, int quantidadeRegistros);
	IEnumerable<ApplicationUser> ObterUsuariosBloqueadosPaginado(int pagina, int quantidadeRegistros);
	int ObterTodosUsuariosTotalRegistros();
	int ObterUsuariosBloqueadosTotalRegistros();
	ApplicationUser ObterPorId(string id);
}
