using Aquiles.Application.UseCases.Login.DoLogin;
using Aquiles.Communication.Requests.Login;
using Microsoft.AspNetCore.Mvc;

namespace Aquiles.API.Controllers;

public class LoginController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] RequestLoginJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
}
