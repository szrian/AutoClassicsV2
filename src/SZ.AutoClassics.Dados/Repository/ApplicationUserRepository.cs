using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.Repository;

public class ApplicationUserRepository : IApplicationUserRepository
{
	private readonly AppDbContext _context;
    public ApplicationUserRepository(AppDbContext context)
    {
        _context = context;
    }
    public ApplicationUser ObterPorId(string id) => _context.Users.ToList().Where(p => p.Id == id).FirstOrDefault();

	public IEnumerable<ApplicationUser> ObterTodosPaginado(int pagina, int quantidadeRegistros)
        => _context.Users.ToList().Skip(pagina * quantidadeRegistros).Take(quantidadeRegistros);

    public int ObterTodosUsuariosTotalRegistros() => _context.Users.Count() - 1;

    public IEnumerable<ApplicationUser> ObterUsuariosBloqueadosPaginado(int pagina, int quantidadeRegistros)
        => _context.Users.Where(p => p.Bloqueado).ToList().Skip(pagina * 20).Take(quantidadeRegistros);

	public int ObterUsuariosBloqueadosTotalRegistros() => _context.Users.Where(p => p.Bloqueado).Count();
}
