using System.Security.Claims;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Aplicacao.ViewObjects;

namespace SZ.AutoClassics.Aplicacao.Interface;

public interface IAccountAppService
{
	Task<string> EfetuarLogin(LoginViewModel loginVM);
	Task<string> RegistrarUsuario(RegistroViewModel registroVM);
	ApplicationUserViewModel ObterUsuarioLogado(ClaimsPrincipal usuarioLogado);
	ApplicationUserViewModel ObterUsuarioPorId(string id);
	UsuariosFiltrosViewObject ObterTodosUsuarios(ClaimsPrincipal usuarioLogado, int pagina, int quantidadeRegistros);
	UsuariosFiltrosViewObject ObterUsuariosBloqueados(int pagina, int quantidadeRegistros);
	string ObterUsuarioLogadoId(ClaimsPrincipal usuarioLogado);
	Task<(bool, string)> EditarUsuario(ApplicationUserViewModel usuarioVM);
	Task<bool> BloquearUsuario(string id);
	Task<bool> DesbloquearUsuario(string id);
	Task Logout();
}
