using Aquiles.Application.Servicos;
using Aquiles.Application.UseCases.Faturas.Create;
using Aquiles.Application.UseCases.Faturas.GetAll;
using Aquiles.Application.UseCases.Faturas.Update;
using Aquiles.Communication.Requests.Faturas;
using Aquiles.Communication.Responses.Faturas;
using Microsoft.AspNetCore.Mvc;

namespace Aquiles.API.Controllers;
[ServiceFilter(typeof(AquilesAuthorize))]
public class FaturaController : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromServices] ICreateFaturaUseCase useCase, [FromBody] RequestCreateFaturaJson request)
    {
        await useCase.Execute(request);
        return Created(string.Empty, null);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IList<ResponseFaturaJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromServices] IGetAllFaturaUseCase useCase, Guid? idPlano, Guid? clienteId)
    {
        var result = await useCase.Execute(idPlano, clienteId);
        return Ok(result);
    }
    
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Atualizar([FromServices] IUpdateFaturaUseCase useCase, [FromRoute] Guid id, [FromBody] RequestCreateFaturaJson fatura)
    {
        await useCase.Execute(fatura, id);
        return NoContent();
    }
}
