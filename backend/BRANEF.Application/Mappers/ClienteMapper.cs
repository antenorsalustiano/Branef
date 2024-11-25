using BRANEF.Application.Dto;
using BRANEF.Domain.Entities;

namespace BRANEF.Application.Mappers
{
    public static class ClienteMapper
    {
        public static ClienteDto ToDto(Cliente cliente)
        {
            return new ClienteDto(cliente.Id, cliente.Nome, cliente.EmpresaPorteId);
        }
    }
}
