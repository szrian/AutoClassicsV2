using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.EntityConfig;

public class AnuncioConfig : IEntityTypeConfiguration<Anuncio>
{
	public void Configure(EntityTypeBuilder<Anuncio> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Titulo)
			   .IsRequired()
			   .HasColumnType("varchar(80)");

		builder.Property(p => p.Marca)
			   .IsRequired()
			   .HasColumnType("varchar(80)");

		builder.Property(p => p.Ano)
			   .IsRequired()
			   .HasColumnType("varchar(4)");

		builder.Property(p => p.Cor)
			   .IsRequired()
			   .HasColumnType("varchar(50)");

		builder.Property(p => p.NumeroPortas)
			   .IsRequired();

		builder.Property(p => p.Cambio)
			   .IsRequired()
			   .HasColumnType("varchar(40)");

		builder.Property(p => p.Combustivel)
			   .IsRequired()
			   .HasColumnType("varchar(40)");

		builder.Property(p => p.Carroceria)
			   .IsRequired()
			   .HasColumnType("varchar(40)");

		builder.Property(p => p.ImagemUrl)
			   .IsRequired()
			   .HasColumnType("varchar(4000)");

		builder.Property(p => p.Motor)
			   .IsRequired()
			   .HasColumnType("varchar(80)");

		builder.Property(p => p.Quilometragem)
			   .IsRequired();

		builder.Property(p => p.Preco)
			   .IsRequired()
			   .HasColumnType("decimal(10,2)");

		builder.Property(p => p.Descricao)
			   .IsRequired()
			   .HasColumnType("varchar(500)");

		builder.HasOne(p => p.Estado)
			   .WithMany(q => q.Anuncios)
			   .HasForeignKey(r => r.EstadoId);

		builder.HasOne(p => p.Cidade)
			   .WithMany(q => q.Anuncios)
			   .HasForeignKey(r => r.CidadeId);

		builder.HasOne(p => p.Usuario)
			   .WithMany(q => q.Anuncios)
			   .HasForeignKey(r => r.UsuarioId);

		builder.Ignore(p => p.ValidationResult);
	}
}
