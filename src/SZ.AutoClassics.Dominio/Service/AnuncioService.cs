using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Service;

public class AnuncioService : IAnuncioService
{
	private readonly IAnuncioRepository _anuncioRepository;
	public AnuncioService(IAnuncioRepository anuncioRepository)
	{
		_anuncioRepository = anuncioRepository;
	}

	public async Task<Anuncio> Adicionar(Anuncio anuncio)
	{
		anuncio.Validar();

		if (!anuncio.ValidationResult.IsValid) return anuncio;

		await _anuncioRepository.Adicionar(anuncio);

		return anuncio;
	}

	public async Task<Anuncio> Atualizar(Anuncio anuncio)
	{
		anuncio.Validar();

		if (!anuncio.ValidationResult.IsValid) return anuncio;

		await _anuncioRepository.Atualizar(anuncio);

		return anuncio;
	}

	public async Task<int> Commit() => await _anuncioRepository.Commit();
	

	public async Task Remover(Guid anuncioId, string usuarioLogadoId, bool ehAdmin)
	{
		var anuncio = await _anuncioRepository.ObterPorId(anuncioId);

		if (anuncio != null && (anuncio.UsuarioId == usuarioLogadoId || ehAdmin))
			await _anuncioRepository.Remover(anuncioId);
	}
}
