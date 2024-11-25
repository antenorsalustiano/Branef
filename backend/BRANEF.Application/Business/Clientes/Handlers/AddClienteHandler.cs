using MediatR;
using BRANEF.Domain.Entities;
using BRANEF.Domain.Interfaces.IRepository;
using BRANEF.Shared.Extensions;
using BRANEF.Application.Mappers;
using BRANEF.Application.Business.Clientes.Base;

namespace BRANEF.Application.Business.Clientes.Handlers;

public record AddClienteCommmand(string Nome, int EmpresaPorteId) : IRequest<ClienteApiResult>;

public class AddClienteHandler : ClienteHandlerBase, IRequestHandler<AddClienteCommmand, ClienteApiResult>
{
    private readonly IClienteRepository _clienteRepository;

    public AddClienteHandler(IClienteRepository clienteRepository) : base(clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteApiResult> Handle(AddClienteCommmand request, CancellationToken cancellationToken)
    {
        var validate = await Validate(0, request.Nome, request.EmpresaPorteId);

        if (!validate.IsValid)
            return new ClienteApiResult(false, validate.ErrorMessage);

        var result = await _clienteRepository.Add(new Cliente(request.Nome, request.EmpresaPorteId));

        return new ClienteApiResult(true, Dto: ClienteMapper.ToDto(result));
    }
}