using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using PrintingServer.API.Tests;
using PrintingServer.Application.Extentions;
using Xunit;

namespace PrintingServer.API.Controllers.Tests;

public class ClientControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _applicationFactory;

    public ClientControllerTests(WebApplicationFactory<Program> applicationFactory)
    {
        _applicationFactory = applicationFactory.WithWebHostBuilder(buil=>
        buil.ConfigureTestServices(services =>
        {
            services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceCollectionExtentions).Assembly));
        }));
    }

    [Fact()]
    public async void GetAll_Valid_Return200()
    {
        var client = _applicationFactory.CreateClient();
        var result = await client.GetAsync("/api/Client/All");
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact()]
    public void GetAllActiveTest()
    {

    }

    [Fact()]
    public void SearchTest()
    {

    }

    [Fact()]
    public void GetByIDTest()
    {

    }

    [Fact()]
    public void AllowedReportsTest()
    {

    }

    [Fact()]
    public void CreateTest()
    {

    }

    [Fact()]
    public void UpdateTest()
    {

    }

    [Fact()]
    public void ActivateTest()
    {

    }

    [Fact()]
    public void DeActivateTest()
    {

    }

    [Fact()]
    public void DeleteTest()
    {

    }
}