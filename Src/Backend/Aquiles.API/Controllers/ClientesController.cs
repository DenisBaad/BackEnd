using Aquiles.Application.UseCases.Clientes.AtivarOuInativar;
using Aquiles.Application.UseCases.Clientes.Create;
using Aquiles.Application.UseCases.Clientes.GetAll;
using Aquiles.Application.UseCases.Clientes.Update;
using Aquiles.Communication.Requests.Clientes;
using Aquiles.Communication.Responses.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace Aquiles.API.Controllers;

public class ClientesController : BaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseClientesJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromServices] ICreateClienteUseCase useCase, [FromBody] RequestCreateClientesJson request)
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IList<ResponseClientesJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllClientesUseCase useCase)
    {
        var result = await useCase.Execute();
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Update([FromServices] IUpdateClienteUseCase useCase, [FromRoute] Guid id, [FromBody] RequestCreateClientesJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }

    [HttpPatch]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStatus([FromServices] IAtivarOuInativarClienteUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);
        return NoContent();
    }
}
