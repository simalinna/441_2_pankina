using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using ClassLibrary;
using static System.Net.Mime.MediaTypeNames;
using Lab4.Controllers;

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

            var response = await client.GetAsync("/api/solver/initial?citiesCount=120&populationSize=70");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var answersJson = await response.Content.ReadAsStringAsync();
            Assert.Equal(" оличество городов должно быть не более 100!", answersJson);
        }


        [Fact]
        public async Task Test2()
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/solver/initial?citiesCount=20&populationSize=70");

            var populationJson = await response.Content.ReadAsStringAsync();
            var populationObject = JsonConvert.DeserializeObject<Population>(populationJson);

            Assert.Equal(1, populationObject.generationsCounter);
            Assert.Equal(20, populationObject.distances.GetLength(0));
        }


        [Fact]
        public async Task Test3()
        {
            var client = factory.CreateClient();

            int populationSize = 70;
            int[,] distances = new int[10, 10] {
                { 0, 120, 86, 32, 93, 40, 88, 110, 94, 93 },
                { 120, 0, 113, 133, 97, 118, 141, 70, 103, 72 },
                { 86, 113, 0, 117, 69, 65, 103, 120, 63, 46 },
                { 32, 133, 117, 0, 65, 60, 72, 83, 84, 92 },
                { 93, 97, 69, 65, 0, 93, 70, 104, 57, 88 },
                { 40, 118, 65, 60, 93, 0, 76, 72, 89, 70 },
                { 88, 141, 103, 72, 70, 76, 0, 73, 80, 104 },
                { 110, 70, 120, 83, 104, 72, 73, 0, 122, 127 },
                { 94, 103, 63, 84, 57, 89, 80, 122, 0, 76 },
                { 93, 72, 46, 92, 88, 70, 104, 127, 76, 0 },
            };

            Population population = new Population(populationSize, distances);

            PopulationDeserialized populationDeserialized = new PopulationDeserialized(population);

            var response = await client.PostAsJsonAsync("/api/solver/next?", populationDeserialized);

            var populationJson = await response.Content.ReadAsStringAsync();
            var populationObject = JsonConvert.DeserializeObject<Population>(populationJson);

            Assert.Equal(2, populationObject.generationsCounter);
            Assert.Equal(10, populationObject.citiesCount);
        }
    }
}