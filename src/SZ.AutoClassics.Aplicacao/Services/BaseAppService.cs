using AutoMapper;
using System.Linq.Expressions;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class BaseAppService<TViewModel, TEntidade> : IBaseAppService<TViewModel, TEntidade> where TEntidade : EntidadeBase
{
	private readonly IBaseService<TEntidade> _baseService;
	private readonly IRepository<TEntidade> _repository;
	private readonly IMapper _mapper;

	
	public BaseAppService(IBaseService<TEntidade> baseService, IRepository<TEntidade> repository, IMapper mapper)
	{
		_baseService = baseService;
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<TViewModel>> Buscar(Expression<Func<TViewModel, bool>> predicate)
	{
		var retorno = await _repository.Buscar(_mapper.Map<Expression<Func<TEntidade, bool>>>(predicate));

		return _mapper.Map<IEnumerable<TViewModel>>(retorno);
	}

	public async Task<TViewModel> ObterPorId(Guid id)
	{
		var objEntidade = await _repository.ObterPorId(id);

		return _mapper.Map<TViewModel>(objEntidade);
	}

	public async Task<IEnumerable<TViewModel>> ObterTodos()
	{
		var listaObjEntidade = await _repository.ObterTodos();

		return _mapper.Map<IEnumerable<TViewModel>>(listaObjEntidade);
	}

	public async Task<IEnumerable<TViewModel>> ObterTodosPaginado(int s, int t)
	{
		var listaObjEntidade = await _repository.ObterTodosPaginado(s * t, t);

		return _mapper.Map<IEnumerable<TViewModel>>(listaObjEntidade);
	}

	public async Task Remover(Guid id)
	{
		await _baseService.Remover(id);
	}
}
