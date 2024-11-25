using BRANEF.Application.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRANEF.Application.Business
{
	public record GetClientesPaginatedQuery(int Page, int PageSize, string? Search = null, string? SortBy = null, bool Descending = false)
	: IRequest<PaginatedResult<ClienteDto>>;
}
