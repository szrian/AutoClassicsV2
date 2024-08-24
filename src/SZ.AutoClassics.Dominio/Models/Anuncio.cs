using FluentValidation.Results;
using SZ.AutoClassics.Dominio.Validator;

namespace SZ.AutoClassics.Dominio.Models;

public class Anuncio : EntidadeBase
{
	public Anuncio()
	{
	}

	public Anuncio(string titulo, string marca, string ano, string cor, int numeroPortas, string cambio,
				   string combustivel, string carroceria, string imagemUrl, string motor, int quilometragem, decimal preco, 
				   bool arCondicionado, bool travasEletricas, bool airBag, bool tetoSolar, bool radio, string descricao, 
				   Guid estadoId, Guid cidadeId, string usuarioId)
	{
		Titulo = titulo;
		Marca = marca;
		Ano = ano;
		DataCriacao = DateTime.Now;
		Cor = cor;
		NumeroPortas = numeroPortas;
		Cambio = cambio;
		Combustivel = combustivel;
		Carroceria = carroceria;
		ImagemUrl = imagemUrl;
		Motor = motor;
		Quilometragem = quilometragem;
		Preco = preco;
		ArCondicionado = arCondicionado;
		TravasEletricas = travasEletricas;
		AirBag = airBag;
		TetoSolar = tetoSolar;
		Radio = radio;
		Descricao = descricao;
		EstadoId = estadoId;
		CidadeId = cidadeId;
		UsuarioId = usuarioId;
	}

	public string Titulo { get; private set; }
	public string Marca { get; private set; }
	public string Ano { get; private set; }
	public DateTime DataCriacao { get; private set; }
	public string Cor { get; private set; }
	public int NumeroPortas { get; private set; }
	public string Cambio { get; private set; }
	public string Combustivel { get; private set; }
	public string Carroceria { get; private set; }
	public string ImagemUrl { get; private set; }
	public string Motor { get; private set; }
	public int Quilometragem { get; private set; }
	public decimal Preco { get; private set; }
	public bool ArCondicionado { get; private set; }
	public bool TravasEletricas { get; private set; }
	public bool AirBag { get; private set; }
	public bool TetoSolar { get; private set; }
	public bool Radio { get; private set; }
	public string Descricao { get; private set; }
	public Guid EstadoId { get; private set; }
	public Guid CidadeId { get; private set; }
	public string UsuarioId { get; private set; }
	public ValidationResult ValidationResult { get; private set; }

	public virtual Estado Estado { get; set; }
	public virtual Cidade Cidade { get; set; }
	public virtual ApplicationUser Usuario { get; set; }
	public virtual ICollection<AnuncioSalvo> AnunciosSalvos { get; set; }

	public void Validar()
	{
		ValidationResult = new AnuncioValidator().Validate(this);
	}
}
