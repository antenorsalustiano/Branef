using BRANEF.Application.Business;
using BRANEF.Application.Business.Clientes.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BRANEF.Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ClienteController : Controller
	{
		private readonly IMediator _mediator;

		public ClienteController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("paginated")]
		public async Task<IActionResult> GetPaginated([FromQuery] GetClientesPaginatedQuery query)
		{
			var result = await _mediator.Send(query);

			if (!result.Ok)
				return BadRequest(result.Message);

			return Ok(new
			{
				Data = result.Data,
				TotalCount = result.TotalCount,
				Page = result.Page,
				PageSize = result.PageSize
			});
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] GetClienteQuery query)
		{
			var result = await _mediator.Send(query);
			return result.Ok ? Ok(result.Dto) : BadRequest(result.Message);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddClienteCommmand command)
		{
			var result = await _mediator.Send(command);
			return result.Ok ?
					CreatedAtAction(nameof(Get), new { id = result.Dto!.Id }, result.Dto) :
					BadRequest(result.Message);
		}

		[HttpPut]
		public async Task<IActionResult> Update(UpdateClienteCommmand command)
		{
			var result = await _mediator.Send(command);
			return result.Ok ? Ok(result.Dto) : BadRequest(result.Message);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(DeleteClienteCommand command)
		{
			var result = await _mediator.Send(command);
			return result.Ok ? Ok() : BadRequest(result.Message);
		}
	}
}
