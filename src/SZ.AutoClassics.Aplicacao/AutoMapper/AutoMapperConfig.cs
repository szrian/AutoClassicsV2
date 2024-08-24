using AutoMapper;
using SZ.AutoClassics.Aplicacao.ViewModels;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Aplicacao.AutoMapper;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
		CreateMap<AnuncioViewModel, Anuncio>().ReverseMap();
		CreateMap<EstadoViewModel, Estado>().ReverseMap();
		CreateMap<CidadeViewModel, Cidade>().ReverseMap();
		CreateMap<ApplicationUserViewModel, ApplicationUser>().ReverseMap();
	}
}
