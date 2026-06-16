using Microsoft.AspNetCore.Mvc.Testing;

namespace EnergyMonitorAPI.Tests;

public class EquipamentosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EquipamentosControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Equipamentos_ReturnsHttpStatusCode200()
    {
        // Arrange
        var request = "/api/auth/register";

        // Act
        var response = await _client.PostAsync(request,
            new StringContent(
                "{\"nome\":\"Teste\",\"email\":\"teste@teste.com\",\"senha\":\"123456\"}",
                System.Text.Encoding.UTF8,
                "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }
}