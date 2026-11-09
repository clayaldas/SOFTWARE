using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CleanTeeth.API.Tests.Controllers;

[TestClass]
public sealed class DentalOfficesApiTests
{
    private const string Url = "/api/dentaloffices";

    private static ApiFactory _factory = null!;
    private static HttpClient _client = null!;

    public TestContext TestContext { get; set; } = null!;

    [ClassInitialize]
    public static async Task ClassInitialize(TestContext testContext)
    {
        using var context = ApiFactory.CreateContext();
        await context.Database.EnsureDeletedAsync(testContext.CancellationToken);
        await context.Database.EnsureCreatedAsync(testContext.CancellationToken);

        _factory = new ApiFactory();
        _client = _factory.CreateClient();
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [TestMethod]
    public async Task Post_ValidName_Returns201WithLocation()
    {
        // Act
        var response = await _client.PostAsJsonAsync(
            Url, new { name = "Consultorio Norte" }, TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        Assert.IsNotNull(response.Headers.Location);
    }

    [TestMethod]
    public async Task Get_ExistingId_Returns200WithName()
    {
        // Arrange
        var created = await _client.PostAsJsonAsync(
            Url, new { name = "Consultorio Sur" }, TestContext.CancellationToken);
        var id = await created.Content.ReadFromJsonAsync<Guid>(
            TestContext.CancellationToken);

        // Act
        var response = await _client.GetAsync(
            $"{Url}/{id}", TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<JsonElement>(
            TestContext.CancellationToken);
        Assert.AreEqual("Consultorio Sur", body.GetProperty("name").GetString());
    }

    [TestMethod]
    public async Task Get_UnknownId_Returns404WithError()
    {
        // Act
        var response = await _client.GetAsync(
            $"{Url}/{Guid.NewGuid()}", TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<JsonElement>(
            TestContext.CancellationToken);
        Assert.IsTrue(body.TryGetProperty("error", out _));
    }

    [TestMethod]
    public async Task Post_BlankName_Returns400WithErrors()
    {
        // Act
        var response = await _client.PostAsJsonAsync(
            Url, new { name = "   " }, TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<JsonElement>(
            TestContext.CancellationToken);
        Assert.IsTrue(body.TryGetProperty("errors", out _));
    }

    [TestMethod]
    public async Task Post_NameOf151Characters_Returns400()
    {
        // Act
        var response = await _client.PostAsJsonAsync(
            Url, new { name = new string('A', 151) }, TestContext.CancellationToken);

        // Assert
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
