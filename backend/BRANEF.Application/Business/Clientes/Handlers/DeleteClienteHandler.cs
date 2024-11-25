using BRANEF.Domain.Interfaces.IRepository;
using MediatR;

namespace BRANEF.Application.Business.Clientes.Handlers;

public record DeleteClienteCommand(int Id) : IRequest<ClienteApiResult>;

public class DeleteClienteHandler : IRequestHandler<DeleteClienteCommand, ClienteApiResult>
{
    private readonly IClienteRepository _clienteRepository;

    public DeleteClienteHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<ClienteApiResult> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var result = await _clienteRepository.GetById(request.Id);

        if (result is null)
            return new ClienteApiResult(false, "Registro não encontrado");

        await _clienteRepository.Delete(result);

        return new ClienteApiResult(true);
    }
}