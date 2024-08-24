using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.EntityConfig;

internal class AnuncioSalvoConfig : IEntityTypeConfiguration<AnuncioSalvo>
{
	public void Configure(EntityTypeBuilder<AnuncioSalvo> builder)
	{
		builder.HasKey(p => p.Id);

		builder.Property(p => p.Id).HasColumnOrder(1);
		builder.Property(p => p.AnuncioId).IsRequired().HasColumnOrder(2);
		builder.Property(p => p.UsuarioId).IsRequired().HasColumnOrder(3);

		builder.HasOne(p => p.Usuario)
			.WithMany(q => q.AnunciosSalvos)
			.HasForeignKey(r => r.UsuarioId);

		builder.HasOne(p => p.Anuncio)
			.WithMany(q => q.AnunciosSalvos)
			.HasForeignKey(r => r.AnuncioId);
	}
}
