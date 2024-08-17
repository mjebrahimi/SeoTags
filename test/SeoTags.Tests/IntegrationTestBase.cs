using Microsoft.AspNetCore.Mvc.Testing;
using SeoTags.Sample;
using VerifyTests.DiffPlex;

namespace ParkBee.Assessment.IntegrationTests.Common;

public class IntegrationTestBase(WebApplicationFactory<Program> webApplicationFactory) : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string _snapshotsPath = Path.Combine(GetProjectDirectory(), "_snapshots");

    static IntegrationTestBase()
    {
        VerifyDiffPlex.Initialize(OutputType.Compact);
        DerivePathInfo((_, _, type, method) => new PathInfo(_snapshotsPath, type.Name, method.Name));
    }

    protected WebApplicationFactory<Program> WebApplicationFactory { get; } = webApplicationFactory;
    protected HttpClient HttpClient { get; } = webApplicationFactory.CreateClient();

    public async Task VerifyHtml(string html)
    {
        await Verify(html, "html");
    }

    private static string GetProjectDirectory()
    {
        var currentDirectory = AppContext.BaseDirectory;
        var path = Path.Combine(currentDirectory, "..", "..", "..");
        return Path.GetFullPath(path);
    }
}