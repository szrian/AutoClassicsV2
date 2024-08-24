using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZ.AutoClassics.Dominio.Models;

public class Cidade : EntidadeBase
{
	public Cidade()
	{
	}

	public Cidade(string descricao, Guid estadoId)
	{
		Descricao = descricao;
		EstadoId = estadoId;
	}

	public string Descricao { get; private set; }
	public Guid EstadoId { get; private set; }


	public virtual Estado Estado { get; set; }
	public virtual List<Anuncio> Anuncios { get; set; }
}
