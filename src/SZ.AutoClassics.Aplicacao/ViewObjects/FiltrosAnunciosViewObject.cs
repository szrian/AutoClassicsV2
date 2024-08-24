using System.ComponentModel.DataAnnotations;

namespace SZ.AutoClassics.Aplicacao.ViewObjects;

public class FiltrosAnunciosViewObject
{
	public string UF { get; set; }
	public string Marca { get; set; }
	public string Cor { get; set; }
	public string Ano { get; set; }
	public string Motor { get; set; }
	public string Cambio { get; set; }

	[Display(Name = "Carroceria")]
	public string Carroceria { get; set; }
	public decimal PrecoMin { get; set; }
	public decimal PrecoMax { get; set; }
	public int NumeroPortas { get; set; }
	public int Quilometragem { get; set; }
	public int Pagina { get; set; }
	public int TotalRegistros { get; set; }
	public bool Radio { get; set; }
	public bool ArCondicionado { get; set; }
	public bool TetoSolar { get; set; }
	public bool AirBag { get; set; }
	public bool TravasEletricas { get; set; }
}
