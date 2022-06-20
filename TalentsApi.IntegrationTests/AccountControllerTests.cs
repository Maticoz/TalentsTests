using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TalentsApi.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace TalentsApi.IntegrationTests
{
    public class AccountControllerTests
    {
        private HttpClient _client;
        private readonly List<Type> _controllerTypes;
        private readonly WebApplicationFactory<Program> _factory;
        public AccountControllerTests()
        {
            _controllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            _factory = new WebApplicationFactory<Program>();

            _factory = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    _controllerTypes.ForEach(c => services.AddScoped(c));
                });
            });
            _client = _factory.CreateDefaultClient();
        }

        [Fact]
        public async Task TestController()
        {
            var webAppFac = new WebApplicationFactory<Program>();
            var httpClient = webAppFac.CreateDefaultClient();


            string url = "api/test"; // /api/test, /api/test/, api/test/

            var response = await httpClient.GetAsync(url);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}