using BRANEF.Application.Business.Clientes.Base;
using BRANEF.Application.Mappers;
using BRANEF.Domain.Interfaces.IRepository;
using MediatR;

namespace BRANEF.Application.Business.Clientes.Handlers;

public record UpdateClienteCommmand(int ClienteId, string Nome, int EmpresaPorteId) : IRequest<ClienteApiResult>;

public class UpdateClienteHandler : ClienteHandlerBase, IRequestHandler<UpdateClienteCommmand, ClienteApiResult>
{
    private readonly IClienteRepository _clienteRepository;

    public UpdateClienteHandler(IClienteRepository clienteRepository) : base(clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteApiResult> Handle(UpdateClienteCommmand request, CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.GetById(request.ClienteId);
        if (cliente is null)
            return new ClienteApiResult(false, "Registro não encontrado");

        var validate = await Validate(request.ClienteId, request.Nome, request.EmpresaPorteId);
        if (!validate.IsValid)
            return new ClienteApiResult(false, validate.ErrorMessage);

        cliente.Nome = request.Nome;
        cliente.EmpresaPorteId = request.EmpresaPorteId;

        var result = await _clienteRepository.Update(cliente);

        return new ClienteApiResult(true, "Registro atualizado", ClienteMapper.ToDto(result));
    }
}