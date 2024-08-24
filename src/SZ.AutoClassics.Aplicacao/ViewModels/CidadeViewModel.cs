using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZ.AutoClassics.Aplicacao.ViewModels;

public class CidadeViewModel : ViewModelBase
{
	public string Descricao { get; set; }
	public Guid EstadoId { get; set; }

	public virtual EstadoViewModel Estado { get; set; }
	public virtual List<AnuncioViewModel> Anuncios { get; set; }
}
