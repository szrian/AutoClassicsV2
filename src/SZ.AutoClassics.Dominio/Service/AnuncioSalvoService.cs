using SZ.AutoClassics.Dominio.Interfaces.Repository;
using SZ.AutoClassics.Dominio.Interfaces.Service;
using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Service;

public class AnuncioSalvoService : IAnuncioSalvoService
{
    private readonly IAnuncioSalvoRepository _anuncioSalvoRepository;
    public AnuncioSalvoService(IAnuncioSalvoRepository anuncioSalvoRepository)
    {
        _anuncioSalvoRepository = anuncioSalvoRepository;
    }

    public async Task SalvarAnuncio(AnuncioSalvo anuncioSalvo)
	{
        var jaEstaSalvo = await _anuncioSalvoRepository.VerificarExistencia(anuncioSalvo.AnuncioId, anuncioSalvo.UsuarioId);

        if (!jaEstaSalvo)
            await _anuncioSalvoRepository.Adicionar(anuncioSalvo);
	}
}
