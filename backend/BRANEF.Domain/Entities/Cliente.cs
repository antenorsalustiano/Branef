using BRANEF.Domain.Entities.Base;
using BRANEF.Shared.Extensions;

namespace BRANEF.Domain.Entities;

public partial class Cliente : EntidadeBase
{
    public string Nome { get; set; } = null!;

    public int EmpresaPorteId { get; set; }

    public virtual EmpresaPorte EmpresaPorte { get; set; } = null!;

    public Cliente(string nome, int empresaPorteId)
    {
        if (!EmpresaPorteIsValid(empresaPorteId))
            throw new ArgumentException("Porte da empresa deve ter valor entre 1 a 3");

        Nome = nome;
        EmpresaPorteId = empresaPorteId;
    }

    public static bool EmpresaPorteIsValid(int porteId)
    {
        return porteId.In(1, 2, 3);
    }
}
