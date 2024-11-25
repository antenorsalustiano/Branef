using BRANEF.Application.Mappers;
using BRANEF.Domain.Interfaces.IRepository;
using MediatR;

namespace BRANEF.Application.Business.Clientes.Handlers;

public record GetClienteQuery(int Id) : IRequest<ClienteApiResult>;

public class GetClienteHandler : IRequestHandler<GetClienteQuery, ClienteApiResult>
{
    private readonly IClienteRepository _clienteRepository;

    public GetClienteHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteApiResult> Handle(GetClienteQuery request, CancellationToken cancellationToken)
    {
        var result = await _clienteRepository.GetById(request.Id);

        if (result is null)
            return new ClienteApiResult(false, "Registro não encontrado");

        return new ClienteApiResult(true, null, ClienteMapper.ToDto(result));
    }
}