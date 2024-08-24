using SZ.AutoClassics.Aplicacao.ViewModels;

namespace SZ.AutoClassics.Aplicacao.ViewObjects;

public class AnunciosFiltrosViewObject
{
	public IEnumerable<AnuncioViewModel> Anuncios { get; set; }
	public FiltrosAnunciosViewObject Filtros { get; set; }
}
