using System.ComponentModel.DataAnnotations;

namespace SZ.AutoClassics.Aplicacao.ViewModels;

public class LoginViewModel
{
	[Required(ErrorMessage = "Informe o Email")]
	[Display(Name = "Email")]
	public string UserEmail { get; set; }

	[Required(ErrorMessage = "Informe a Senha")]
	[DataType(DataType.Password)]
	[Display(Name = "Senha")]
	public string Senha { get; set; }
	public string UrlRetorno { get; set; }
}

public class RegistroViewModel
{
	[Required(ErrorMessage = "Informe o Email")]
	[Display(Name = "Email")]
	public string UserEmail { get; set; }

	[Required(ErrorMessage = "Informe o Nome de Usuário")]
	[Display(Name = "Nome de Usuário")]
	public string UserName { get; set; }

	[Required(ErrorMessage = "Informe o Celular")]
	[DataType(DataType.PhoneNumber)]
	[Display(Name = "Celular")]
	public string NumeroTelefone { get; set; }

	[Required(ErrorMessage = "Informe a Senha")]
	[DataType(DataType.Password)]
	[Display(Name = "Senha")]
	public string Senha { get; set; }
}
