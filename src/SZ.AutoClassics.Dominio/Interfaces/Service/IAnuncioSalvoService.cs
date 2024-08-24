using SZ.AutoClassics.Dominio.Models;

namespace SZ.AutoClassics.Dominio.Interfaces.Service;

public interface IAnuncioSalvoService
{
	Task SalvarAnuncio(AnuncioSalvo anuncioSalvo);
}
