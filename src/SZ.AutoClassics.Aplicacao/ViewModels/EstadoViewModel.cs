using System.ComponentModel.DataAnnotations;

namespace SZ.AutoClassics.Aplicacao.ViewModels;

public class EstadoViewModel : ViewModelBase
{
    public EstadoViewModel()
    { }
    public string Descricao { get; set; }
	[Display(Name = "UF")]
	public string UF { get; set; }

	public virtual List<CidadeViewModel> Cidades { get; set; }
	public virtual List<AnuncioViewModel> Anuncios { get; set; }
}
