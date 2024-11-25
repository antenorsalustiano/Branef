using BRANEF.Domain.Entities.Base;

namespace BRANEF.Domain.Entities;

public partial class EmpresaPorte: EntidadeBase
{

    public string Descricao { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
