using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dados.Repository;

public class Repository<TEntidade> : IRepository<TEntidade> where TEntidade : EntidadeBase, new()
{
	private readonly AppDbContext _context;

	public Repository(AppDbContext context)
	{
		_context = context;
		_context.Database.GetDbConnection();
	}
	public async Task Adicionar(TEntidade obj)
	{
		await _context.Set<TEntidade>().AddAsync(obj);
		await Commit();
	}

	public async Task Atualizar(TEntidade obj)
	{
		_context.Set<TEntidade>().Update(obj);
		await Commit();
	}

	public async Task<IEnumerable<TEntidade>> Buscar(Expression<Func<TEntidade, bool>> predicate)
	{
		var obj = await _context.Set<TEntidade>().AsNoTracking().Where(predicate).ToListAsync();

		return obj;
	}

	public async Task<int> Commit()
	{
		return await _context.SaveChangesAsync();
	}

	public async Task<TEntidade> ObterPorId(Guid id)
	{
		var obj = await _context.Set<TEntidade>().FindAsync(id);

		return obj;
	}

	public async Task<IEnumerable<TEntidade>> ObterTodos()
	{
		var obj = await _context.Set<TEntidade>().ToListAsync();

		return obj;
	}

	public async Task<IEnumerable<TEntidade>> ObterTodosPaginado(int s, int t)
	{
		var obj = await _context.Set<TEntidade>().Skip(s).Take(t).ToListAsync();

		return obj;
	}

	public async Task Remover(Guid id)
	{
		var obj = await ObterPorId(id);
		_context.Set<TEntidade>().Remove(obj);

		await Commit();
	}

	public void Dispose()
	{
		_context.Dispose();
	}
}
