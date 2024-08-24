using Microsoft.AspNetCore.Identity;

namespace SZ.AutoClassics.Dominio.Models;

public class ApplicationUser : IdentityUser
{
	public bool Bloqueado { get; private set; }

	public void AtualizarUsuario(string nomeUsuario, string email, string telefone)
	{
		UserName = nomeUsuario;
		Email = email;
		PhoneNumber = telefone;
	}

	public void Bloquear() => Bloqueado = true;
	public void Desbloquear() => Bloqueado = false;

	public virtual ICollection<Anuncio> Anuncios { get; set; }
	public virtual ICollection<AnuncioSalvo> AnunciosSalvos { get; set; }
}
