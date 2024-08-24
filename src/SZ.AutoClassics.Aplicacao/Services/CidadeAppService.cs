using AutoMapper;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class CidadeAppService : BaseAppService<CidadeViewModel, Cidade>, ICidadeAppService
{
	private readonly IMapper _mapper;
	private readonly ICidadeRepository _cidadeRepository;
	public CidadeAppService(IBaseService<Cidade> baseService,
		IRepository<Cidade> repository,
		IMapper mapper,
		ICidadeRepository cidadeRepository) 
		: 
		base(baseService, repository, mapper)
	{
		_mapper = mapper;
		_cidadeRepository = cidadeRepository;
	}

	public async Task<IEnumerable<CidadeViewModel>> ObterCidadesPorEstadoId(Guid estadoId)
	{
		return _mapper.Map<IEnumerable<CidadeViewModel>>(await _cidadeRepository.ObterCidadesPorEstadoId(estadoId));
	}
}
