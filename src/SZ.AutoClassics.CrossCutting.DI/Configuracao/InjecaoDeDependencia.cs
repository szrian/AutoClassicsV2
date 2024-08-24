using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SZ.AutoClassics.Aplicacao.Interface;
using SZ.AutoClassics.Aplicacao.Services;
using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dados.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;
using SZ.AutoClassics.Dominio.Service;

namespace SZ.AutoClassics.CrossCutting.DI.Configuracao;

public static class InjecaoDeDependencia
{
	public static IServiceCollection ResolverDependencias(this IServiceCollection services)
	{
		services.AddScoped<AppDbContext>();
		services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();

		services.AddAuthorization(options =>
		{
			options.AddPolicy("Admin", politica =>
			{
				politica.RequireRole("Admin");
			});
		});

		#region"Aplicação"

		services.AddScoped(typeof(IBaseAppService<,>), typeof(BaseAppService<,>));
		services.AddScoped<IAnuncioAppService, AnuncioAppService>();
		services.AddScoped<IAnuncioSalvoAppService, AnuncioSalvoAppService>();
		services.AddScoped<IEstadoAppService, EstadoAppService>();
		services.AddScoped<ICidadeAppService, CidadeAppService>();
		services.AddScoped<IAccountAppService, AccountAppService>();

		#endregion

		#region"Domínio"

		services.AddFluentValidation(config =>
		{
			config.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
		});

		services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
		services.AddScoped<IAnuncioService, AnuncioService>();
		services.AddScoped<IAnuncioSalvoService, AnuncioSalvoService>();

		#endregion

		#region"Dados"

		services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
		services.AddTransient<IAnuncioRepository, AnuncioRepository>();
		services.AddTransient<IAnuncioSalvoRepository, AnuncioSalvoRepository>();
		services.AddTransient<ICidadeRepository, CidadeRepository>();
		services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();

		#endregion

		return services;
	}
}
