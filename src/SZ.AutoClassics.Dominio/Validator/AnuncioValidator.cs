using FluentValidation;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Validator
{
	public class AnuncioValidator : AbstractValidator<Anuncio>
	{
		public AnuncioValidator()
		{
			RuleFor(p => p.Titulo)
				.NotEmpty()
				.WithMessage("O campo Título é obrigatório!");

			RuleFor(p => p.Marca)
				.NotEmpty()
				.WithMessage("O campo Marca é obrigatório!");

			RuleFor(p => p.Ano)
				.NotEmpty()
				.WithMessage("O campo Ano é obrigatório!");

			RuleFor(p => p.Cor)
				.NotEmpty()
				.WithMessage("O campo Cor é obrigatório!");

			RuleFor(p => p.NumeroPortas)
				.NotEmpty()
				.WithMessage("O número de portas é obrigatório!")
				.LessThan(8)
				.WithMessage("Valor inválido para quantidade de portas");

			RuleFor(p => p.Cambio)
				.NotEmpty()
				.WithMessage("O campo Câmbio é obrigatório!");

			RuleFor(p => p.Combustivel)
				.NotEmpty()
				.WithMessage("O campo Combustivel é obrigatório!");

			RuleFor(p => p.Carroceria)
				.NotEmpty()
				.WithMessage("O campo Carroceria é obrigatório!");

			RuleFor(p => p.Motor)
				.NotEmpty()
				.WithMessage("O campo Motor é obrigatório!");

			RuleFor(p => p.Quilometragem)
				.NotEmpty()
				.WithMessage("O campo Quilometragem é obrigatório!");

			RuleFor(p => p.DataCriacao)
				.NotEmpty()
				.WithMessage("O campo Data Criação é obrigatório!");

			RuleFor(p => p.Preco)
				.NotEmpty()
				.WithMessage("O campo Preço é obrigatório!");

			RuleFor(p => p.Descricao)
				.NotEmpty()
				.WithMessage("O campo Descrição é obrigatório!");

			RuleFor(p => p.ImagemUrl)
				.NotEmpty()
				.WithMessage("Selecione uma imagem!");

			RuleFor(p => p.CidadeId)
				.NotEmpty()
				.WithMessage("Informe a Cidade!");

			RuleFor(p => p.EstadoId)
				.NotEmpty()
				.WithMessage("Informe o Estado!");
		}
	}
}
