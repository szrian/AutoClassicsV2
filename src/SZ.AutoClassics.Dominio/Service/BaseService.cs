using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Service;

public class BaseService<TEntidade> : IBaseService<TEntidade> where TEntidade : EntidadeBase
{
	private readonly IRepository<TEntidade> _repository;

	public BaseService(IRepository<TEntidade> repository)
	{
		_repository = repository;
	}

	public async Task Adicionar(TEntidade obj)
	{
		await _repository.Adicionar(obj);
	}

	public async Task Atualizar(TEntidade obj)
	{
		await _repository.Atualizar(obj);
	}

	public async Task<int> Commit()
	{
		return await _repository.Commit();
	}

	public async Task Remover(Guid id)
	{
		await _repository.Remover(id);
	}
}
