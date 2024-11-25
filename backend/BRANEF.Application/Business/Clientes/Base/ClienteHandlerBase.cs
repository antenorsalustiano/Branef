using BRANEF.Domain.Entities;
using BRANEF.Domain.Interfaces.IRepository;

namespace BRANEF.Application.Business.Clientes.Base
{
    public class ClienteHandlerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteHandlerBase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<(bool IsValid, string? ErrorMessage)> Validate(int clienteId, string nome, int porteId)
        {
            if (!ValidatePorte(porteId))
                return (false, "Porte da empresa deve ter valor entre 1 a 3");

            if (!await ValidateUniqueNome(clienteId, nome))
            {
                return (false, "Nome de cliente já existente");
            }

            return (true, null);
        }

        public bool ValidatePorte(int porteId) => Cliente.EmpresaPorteIsValid(porteId);

        private async Task<bool> ValidateUniqueNome(int clienteId, string nome) => (await _clienteRepository.FilterBy(q => q.Nome.ToUpper() == nome.ToUpper() && q.Id != clienteId)).FirstOrDefault() is null;
    }
}