using Microsoft.EntityFrameworkCore;
using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.Repository;

public class AnuncioSalvoRepository : Repository<AnuncioSalvo>, IAnuncioSalvoRepository
{
	private readonly AppDbContext _appDbContext;
	public AnuncioSalvoRepository(AppDbContext context) : base(context)
	{
		_appDbContext = context;
	}

	public async Task<bool> VerificarExistencia(Guid anuncioId, string usuarioId)
	{
		return await _appDbContext.AnunciosSalvos
			.Where(p => p.AnuncioId == anuncioId && p.UsuarioId == usuarioId)
			.AnyAsync();
	}

	public async Task<IEnumerable<AnuncioSalvo>> ObterPorUsuarioPaginado(string usuarioId, int pagina, int registrosPorPagina)
	{
		return await _appDbContext.AnunciosSalvos
			.Skip(pagina)
			.Take(registrosPorPagina)
			.Where(p => p.UsuarioId == usuarioId)
			.ToListAsync();
	}

    public Task<AnuncioSalvo> ObterAnuncioSalvoPorUsuario(Guid anuncioId, string usuarioId)
    {
		return _appDbContext.AnunciosSalvos
			.Where(p => p.AnuncioId == anuncioId && p.UsuarioId == usuarioId)
			.FirstOrDefaultAsync();
    }
}
