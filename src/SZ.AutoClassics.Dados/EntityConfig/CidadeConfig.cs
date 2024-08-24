using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.EntityConfig;

public class CidadeConfig : IEntityTypeConfiguration<Cidade>
{
	public void Configure(EntityTypeBuilder<Cidade> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Descricao)
			   .IsRequired()
			   .HasColumnType("varchar(100)");

		builder.HasOne(p => p.Estado)
			   .WithMany(q => q.Cidades)
			   .HasForeignKey(p => p.EstadoId);
	}
}
