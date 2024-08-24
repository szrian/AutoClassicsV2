namespace SZ.AutoClassics.Dominio.Models;

public class EntidadeBase
{
	public EntidadeBase()
	{
		Id = Guid.NewGuid();
		Inicializar();
	}

	public Guid Id { get; private set; }

	public bool Ativo { get; private set; }
	public bool Excluido { get; private set; }

	protected void Inicializar()
	{
		Ativo = true;
		Excluido = false;
	}

	protected void Inativar() => Ativo = false;
	protected void Ativar() => Ativo = true;
	protected void ExcluirLogicamente() => Excluido = true;
}
