using Aquiles.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.Usuarios.Create;
public class CreateUsuarioTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CreateUsuarioTest(CustomWebApplicationFactory factory) 
    {
        _httpClient = factory.CreateClient();
    }
    
    [Fact]
    public async Task Sucesso()
    {
        var request = RequestCreateUsuariosJsonBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("Usuarios", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        responseData.RootElement.GetProperty("nome").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Nome);
    }

    [Fact]
    public async Task Erro_Nome_Vazio()
    {
        var request = RequestCreateUsuariosJsonBuilder.Build();
        request.Nome = string.Empty;

        var response = await _httpClient.PostAsJsonAsync("Usuarios", request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var errors = responseData.RootElement.GetProperty("messages").EnumerateArray();
        errors.Should().ContainSingle().And.Contain(e => (e.GetString() ?? "").Equals(ResourceMensagensDeErro.NOME_USUARIO_EMBRANCO));
    }
}   
