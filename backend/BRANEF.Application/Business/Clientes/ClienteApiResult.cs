using BRANEF.Application.Dto;

namespace BRANEF.Application.Business.Clientes;

public record ClienteApiResult(bool Ok, string? Message = null, ClienteDto? Dto = null);
