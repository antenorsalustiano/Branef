using BRANEF.Application.Dto;
using BRANEF.Domain.Entities;
using BRANEF.Domain.Interfaces.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRANEF.Application.Business.Clientes.Handlers
{
	public class GetClientesPaginatedHandler : IRequestHandler<GetClientesPaginatedQuery, PaginatedResult<ClienteDto>>
	{
		private readonly IClienteRepository _clienteRepository;

		public GetClientesPaginatedHandler(IClienteRepository clienteRepository)
		{
			_clienteRepository = clienteRepository;
		}

		public async Task<PaginatedResult<ClienteDto>> Handle(GetClientesPaginatedQuery request, CancellationToken cancellationToken)
		{
			var clientes = await _clienteRepository.GetAll();

			if (!string.IsNullOrEmpty(request.Search))
			{
				clientes = clientes.Where(c => c.Nome.Contains(request.Search)).ToList();
			}

			// Obter a contagem total de registros
			var totalCount = clientes.Count();



			// Aplicar paginação
			var paginatedClientes = clientes
				.Skip((request.Page - 1) * request.PageSize)
				.Take(request.PageSize)
				.Select(c => new ClienteDto(
					c.Id,
					c.Nome,
					c.EmpresaPorteId,
					c.EmpresaPorteId switch
					{
						1 => "Pequeno",
						2 => "Média",
						3 => "Grande",
						_ => "Desconhecido"
					}))
				.ToList();

			// Retornar resultado paginado
			return new PaginatedResult<ClienteDto>
			{
				Data = paginatedClientes,
				TotalCount = totalCount,
				Page = request.Page,
				PageSize = request.PageSize
			};
		}
	}
}
