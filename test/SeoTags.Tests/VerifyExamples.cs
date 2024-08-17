using Microsoft.AspNetCore.Mvc.Testing;
using ParkBee.Assessment.IntegrationTests.Common;
using SeoTags.Sample;
namespace SeoTags.Tests;

public class VerifyExamples(WebApplicationFactory<Program> webApplicationFactory) : IntegrationTestBase(webApplicationFactory)
{
    [Fact]
    public async Task TestSimple()
    {
        var html = await HttpClient.GetStringAsync("/");
        await VerifyHtml(html);
    }

    [Fact]
    public async Task TestJsonLd1()
    {
        var html = await HttpClient.GetStringAsync("/Home/JsonLd1");
        await VerifyHtml(html);
    }

    [Fact]
    public async Task TestJsonLd2()
    {
        var html = await HttpClient.GetStringAsync("/Home/JsonLd2");
        await VerifyHtml(html);
    }
}