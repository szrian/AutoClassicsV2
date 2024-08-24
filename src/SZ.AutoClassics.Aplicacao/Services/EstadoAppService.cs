using AutoMapper;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.Services;

public class EstadoAppService : BaseAppService<EstadoViewModel, Estado>, IEstadoAppService
{
	public EstadoAppService(IBaseService<Estado> baseService, 
		IRepository<Estado> repository, 
		IMapper mapper) 
		: base(baseService, repository, mapper)
	{
	}
}
