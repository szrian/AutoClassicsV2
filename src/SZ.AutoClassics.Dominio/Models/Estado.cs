namespace SZ.AutoClassics.Dominio.Models;

public class Estado : EntidadeBase
{
	public Estado()
	{ }

	public Estado(string descricao, string uF)
	{
		Descricao = descricao;
		UF = uF;
	}

	public string Descricao { get; private set; }
	public string UF { get; private set; }

	public virtual List<Cidade> Cidades { get; set; }
	public virtual List<Anuncio> Anuncios { get; set; }
}
