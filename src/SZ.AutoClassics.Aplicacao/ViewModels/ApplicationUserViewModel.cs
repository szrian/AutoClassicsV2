using System.ComponentModel.DataAnnotations;

namespace SZ.AutoClassics.Aplicacao.ViewModels;

public class ApplicationUserViewModel
{
	public string Id { get; set; }

	[Display(Name = "Usuário")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string UserName { get; set; }

	[Display(Name = "Email")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; }

	[Display(Name = "Telefone")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	[DataType(DataType.PhoneNumber)]
	public string PhoneNumber { get; set; }
	public bool Bloqueado { get; set; }

	[Display(Name = "Senha Atual")]
	[DataType(DataType.Password)]
	public string UltimaSenha { get; set; }

	[Display(Name = "Nova Senha")]
	[DataType(DataType.Password)]
	public string Senha { get; set;}

	public int Pagina { get; set; }
	public int QuantidadeRegistros { get; set; }
}
