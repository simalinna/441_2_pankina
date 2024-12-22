using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using ClassLibrary;
using Xunit;

namespace ServerTest
{
    public class UnitTests: IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly WebApplicationFactory<Program> factory;
        public UnitTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("api/initial?citiesCount=5&populationSize=70");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var answersJson = await response.Content.ReadAsStringAsync();
            Assert.Equal("¬ведите значение не более 20", answersJson);
        }
    }
}