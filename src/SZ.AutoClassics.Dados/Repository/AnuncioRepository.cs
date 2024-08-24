using Dapper;
using Microsoft.EntityFrameworkCore;
using SZ.AutoClassics.Dados.Context;
using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Models;
using SZ.AutoClassics.Dominio.Objects;

namespace SZ.AutoClassics.Dados.Repository;

public class AnuncioRepository : Repository<Anuncio>, IAnuncioRepository
{
	private readonly AppDbContext _context;

	public AnuncioRepository(AppDbContext context) : base(context)
	{
		_context = context;
	}

	public IEnumerable<Anuncio> ObterAnunciosFiltrado(FiltrosAnuncioObject filtros)
	{
		var where = MontaFiltrosConsulta(filtros);
		var consulta = string.Format("SELECT * FROM Anuncios WHERE Ativo = 1 AND Excluido = 0 {0} ORDER BY DataCriacao DESC", where);

		var anuncios = _context.Database.GetDbConnection().Query<Anuncio>(consulta, new
		{
			uUF = filtros.UF,
			uMarca = filtros.Marca,
			uCor = filtros.Cor,
			uAno = filtros.Ano,
			uNumeroPortas = filtros.NumeroPortas,
			uPrecoMin = filtros.PrecoMin,
			uPrecoMax = filtros.PrecoMax,
			uQuilometragem = filtros.Quilometragem,
			uMotor = filtros.Motor,
			uCambio = filtros.Cambio,
			uCarroceria = filtros.Carroceria,
			uRadio = filtros.Radio,
			uArCondicionado = filtros.ArCondicionado,
			uTetoSolar = filtros.TetoSolar,
			uAirBag = filtros.AirBag,
			uTravasEletricas = filtros.TravasEletricas
		});

		return anuncios;
	}

	public IEnumerable<Anuncio> ObterAnunciosPorUsuarioId(string usuarioId, int pagina, int quantidadeRegistros)
	{
		return Buscar(p => p.Ativo && p.UsuarioId == usuarioId).Result.Skip(pagina).Take(quantidadeRegistros);
	}

	public async Task<IEnumerable<Anuncio>> ObterUltimosAnuncios()
	{
		return await _context.Set<Anuncio>().AsNoTracking().Where(p => p.Ativo).Take(6).ToListAsync();
	}

	private string MontaFiltrosConsulta(FiltrosAnuncioObject filtros)
	{
		var where = filtros.UF != null ? " AND EstadoId = (SELECT Id FROM Estados WHERE UF LIKE '%' + @uUF + '%')" : "";
		where += filtros.Marca != null ? " AND Marca LIKE '%' + @uMarca + '%'" : "";
		where += filtros.Cor != null ? " AND Cor LIKE '%' + @uCor + '%'" : "";
		where += filtros.Ano != null ? " AND Ano LIKE '%' + @uAno + '%'" : "";
		where += filtros.NumeroPortas != 0 ? " AND NumeroPortas = @uNumeroPortas" : "";
		where += filtros.PrecoMax > filtros.PrecoMin ? " AND Preco BETWEEN @uPrecoMin AND @uPrecoMax" : "";
		where += filtros.Quilometragem > 0 ? " AND Quilometragem BETWEEN 0 AND @uQuilometragem" : "";
		where += filtros.Motor != null ? " AND Motor LIKE '%' + @uMotor + '%'" : "";
		where += filtros.Cambio != null ? " AND Cambio LIKE '%' + @uCambio + '%'" : "";
		where += filtros.Carroceria != null ? " AND Carroceria LIKE '%' + @uCarroceria + '%'" : "";
		where += filtros.Radio == true ? " AND Radio = @uRadio" : "";
		where += filtros.ArCondicionado == true ? " AND ArCondicionado = @uArCondicionado" : "";
		where += filtros.TetoSolar == true ? " AND TetoSolar = @uTetoSolar" : "";
		where += filtros.AirBag == true ? " AND AirBag = @uAirBag" : "";
		where += filtros.TravasEletricas == true ? " AND TravasEletricas = @TravasEletricas" : "";

		return where;
	}
}
