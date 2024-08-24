using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.EntityConfig;

public class EstadoConfig : IEntityTypeConfiguration<Estado>
{
	public void Configure(EntityTypeBuilder<Estado> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Descricao)
			   .IsRequired()
			   .HasColumnType("varchar(100)");

		builder.Property(p => p.UF)
			   .IsRequired()
			   .HasColumnType("varchar(60)");
	}
}
