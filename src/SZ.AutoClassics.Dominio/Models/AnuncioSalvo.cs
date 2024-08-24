namespace SZ.AutoClassics.Dominio.Models;

public class AnuncioSalvo : EntidadeBase
{
    public AnuncioSalvo()
    {
        
    }
    public AnuncioSalvo(Guid anuncioId, string usuarioId)
    {
        AnuncioId = anuncioId;
        UsuarioId = usuarioId;
    }

    public Guid AnuncioId { get; private set; }
	public string UsuarioId { get; private set; }

	public virtual Anuncio Anuncio { get; set; }
	public virtual ApplicationUser Usuario { get; set; }
}
