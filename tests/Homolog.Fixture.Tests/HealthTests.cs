using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public sealed class HealthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthReturnsExpectedContract()
    {
        using var response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var body = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        Assert.NotNull(body);
        Assert.Equal(2, body.Count);
        Assert.Equal("healthy", body["status"]);
        Assert.Equal("plan18-synthetic-fixture", body["service"]);
    }

    [Fact]
    public async Task UnknownRouteReturnsNotFound()
    {
        using var response = await _client.GetAsync("/not-a-route");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
