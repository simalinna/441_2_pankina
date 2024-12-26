using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;
using ClassLibrary;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Lab4.Controllers
{
    [Route("api/solver")]
    [ApiController]
    public partial class HomeController : Controller
    {

        public HomeController()
        {

        }



        [HttpGet("initial")]
        public async Task<ActionResult<string>> createPopulation(int citiesCount, int populationSize)
        {
            try
            {
                if (citiesCount < 5)
                    return BadRequest("Количество городов должно быть больше 5!");

                if (citiesCount > 100)
                    return BadRequest("Количество городов должно быть не более 100!");

                if (populationSize <= 1)
                    return BadRequest("Количество экземпляров в популяции должно быть больше 1!");

                RandomDistances randomDistances = new RandomDistances(citiesCount);
                int[,] distances = randomDistances.distances;

                Population population = new Population(populationSize, distances);

                var res = await Task.FromResult(JsonConvert.SerializeObject(population));

                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("next")]
        public async Task<IActionResult> makeEvolution([FromBody] PopulationDeserialized pop)
        {
            try
            {
                Population population = new Population(pop.routes, pop.distances, pop.citiesCount, pop.populationSize, pop.generationsCounter, pop.bestDistance, pop.meanDistance, pop.bestRoute);
                population.evolution();
                var res = await Task.FromResult(JsonConvert.SerializeObject(population));

                return Ok(res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
