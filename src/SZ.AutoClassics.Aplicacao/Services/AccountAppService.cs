using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Aplicacao.ViewObjects;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class AccountAppService : IAccountAppService
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IApplicationUserRepository _userRepository;
	private readonly IMapper _mapper;
	public AccountAppService(UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		IApplicationUserRepository userRepository,
		IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<string> EfetuarLogin(LoginViewModel loginVM)
	{
		var user = await _userManager.FindByEmailAsync(loginVM.UserEmail);

		if (user != null)
		{
			if (user.Bloqueado)
				return "Seu usuário está bloqueado!";

			var result = await _signInManager.PasswordSignInAsync(user, loginVM.Senha, false, false);

			if (!result.Succeeded)
				return "Falha ao realizar o login!";

			return string.Empty;
		}

		return "Usuário não encontrado";
	}

	public async Task Logout()
	{
		await _signInManager.SignOutAsync();
	}

	public ApplicationUserViewModel ObterUsuarioLogado(ClaimsPrincipal usuarioLogado)
	{
		return _mapper.Map<ApplicationUserViewModel>(_userRepository.ObterPorId(_userManager.GetUserId(usuarioLogado)));
	}

	public ApplicationUserViewModel ObterUsuarioPorId(string id)
	{
		return _mapper.Map<ApplicationUserViewModel>(_userRepository.ObterPorId(id));
	}

	public UsuariosFiltrosViewObject ObterTodosUsuarios(ClaimsPrincipal usuarioLogado, int pagina, int quantidadeRegistros)
	{
		var usuarioLogadoId = _userManager.GetUserId(usuarioLogado);
		var usuarios = _mapper.Map<IEnumerable<ApplicationUserViewModel>>(_userRepository.ObterTodosPaginado(pagina, quantidadeRegistros).Where(p => !p.Bloqueado && p.Id != usuarioLogadoId));
		var totalRegistros = _userRepository.ObterTodosUsuariosTotalRegistros();

		return new UsuariosFiltrosViewObject { Usuarios = usuarios, TotalRegistros = totalRegistros };
	}

	public UsuariosFiltrosViewObject ObterUsuariosBloqueados(int pagina, int quantidadeRegistros)
	{
		var usuarios = _mapper.Map<IEnumerable<ApplicationUserViewModel>>(_userRepository.ObterUsuariosBloqueadosPaginado(pagina, quantidadeRegistros));
		var totalRegistros = _userRepository.ObterUsuariosBloqueadosTotalRegistros();

		return new UsuariosFiltrosViewObject { Usuarios = usuarios, TotalRegistros = totalRegistros };
	}

	public async Task<string> RegistrarUsuario(RegistroViewModel registroVM)
	{
		var user = new ApplicationUser() { UserName = registroVM.UserName, Email = registroVM.UserEmail, PhoneNumber = registroVM.NumeroTelefone };
		var result = await _userManager.CreateAsync(user, registroVM.Senha);

		if (!result.Succeeded)
			return "Falha ao registrar o usuário";

		await _userManager.AddToRoleAsync(user, "Membro");
		return string.Empty;
	}

	public async Task<(bool, string)> EditarUsuario(ApplicationUserViewModel usuarioVM)
	{
		var usuario = _userRepository.ObterPorId(usuarioVM.Id);
		usuario.AtualizarUsuario(usuarioVM.UserName, usuarioVM.Email, usuarioVM.PhoneNumber);
		var result = await _userManager.UpdateAsync(usuario);

		if (!result.Succeeded)
			return (false, "Ocorreu um erro ao editar seu usuário");

		if (usuarioVM.Senha != null)
		{
			var senhaValida = await _userManager.CheckPasswordAsync(usuario, usuarioVM.UltimaSenha);

			if (!senhaValida)
				return (false, "Senha incorreta");

			var senhaResult = await _userManager.ChangePasswordAsync(usuario, usuarioVM.UltimaSenha, usuarioVM.Senha);

			if (!senhaResult.Succeeded)
				return (false, "Ocorreu um erro ao alterar a senha");

			await _signInManager.SignInAsync(usuario, isPersistent: false);
		}

		return (true, string.Empty);
	}

	public string ObterUsuarioLogadoId(ClaimsPrincipal usuarioLogado) => _userManager.GetUserId(usuarioLogado);

	public async Task<bool> BloquearUsuario(string id)
	{
		var usuario = _userRepository.ObterPorId(id);
		usuario.Bloquear();

		var resultado = await _userManager.UpdateAsync(usuario);

		return resultado.Succeeded;
	}

	public async Task<bool> DesbloquearUsuario(string id)
	{
		var usuario = _userRepository.ObterPorId(id);
		usuario.Desbloquear();

		var resultado = await _userManager.UpdateAsync(usuario);

		return resultado.Succeeded;
	}
}
