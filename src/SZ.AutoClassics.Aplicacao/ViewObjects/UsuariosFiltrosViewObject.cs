using SZ.AutoClassics.Aplicacao.ViewModels;

namespace SZ.AutoClassics.Aplicacao.ViewObjects;

public class UsuariosFiltrosViewObject
{
	public IEnumerable<ApplicationUserViewModel> Usuarios { get; set; }

	public int Pagina { get; set; }
	public int TotalRegistros { get; set; }
}
