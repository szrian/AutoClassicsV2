using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.ViewModels;

public class AnuncioViewModel : ViewModelBase
{
	[Display(Name = "UF")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public Guid EstadoId { get; set; }

	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public Guid CidadeId { get; set; }
	public string UsuarioId { get; set; }

	[Display(Name = "Título")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Titulo { get; set; }

	[Display(Name = "Marca")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Marca { get; set; }

	[Display(Name = "Ano do veículo")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Ano { get; set; }

	[Display(Name = "Cor")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Cor { get; set; }

	[Display(Name = "Portas")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public int NumeroPortas { get; set; }

	[Display(Name = "Câmbio")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Cambio { get; set; }

	[Display(Name = "Combustível")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Combustivel { get; set; }

	[Display(Name = "Carroceria")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Carroceria { get; set; }

	[Display(Name = "Motor")]
	public string Motor { get; set; }

	[Display(Name = "KM")]
	public int Quilometragem { get; set; }

	public DateTime DataCriacao { get; set; }

	[Display(Name = "Preço")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public decimal Preco { get; set; }
	public bool ArCondicionado { get; set; }
	public bool TravasEletricas { get; set; }
	public bool AirBag { get; set; }
	public bool TetoSolar { get; set; }
	public bool Radio { get; set; }

	[Display(Name = "Descrição")]
	[Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório")]
	public string Descricao { get; set; }
	public string ImagemUrl { get; set; }
	public string CidadeAnuncio { get; set; }
	public string Uf { get; set; }
	public ApplicationUserViewModel Usuario { get; set; }

	[Display(Name = "Fotos")]
	public List<IFormFile> Imagens { get; set; }
	public List<string> CaminhoImagens { get; set; }
	public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

	public EstadoViewModel Estado { get; set; }
	public IEnumerable<EstadoViewModel> Estados { get; set; }
	public CidadeViewModel Cidade { get; set; }
	public IEnumerable<CidadeViewModel> Cidades { get; set; }
}
