using ClassLibrary;

namespace Lab4.Controllers
{
    public class PopulationDeserialized
    {

        public List<Chromosome> routes { get; set; }

        //public int[,]? distances { get; set; }

        public List<List<int>> distances { get; set; }

        public int citiesCount { get; set; }

        public int populationSize { get; set; }

        public int generationsCounter { get; set; }

        public double bestDistance { get; set; }

        public double meanDistance { get; set; }

        public List<int> bestRoute { get; set; }

        public PopulationDeserialized()
        {

        }

        public PopulationDeserialized(Population pop)
        {
            this.routes = pop.routes;
            this.citiesCount = pop.citiesCount;

            distances = new List<List<int>>();
            for (int i = 0; i < citiesCount; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < citiesCount; j++)
                {
                    row.Add(pop.distances[i, j]);
                }
                distances.Add(row);
            }

            this.populationSize = pop.populationSize;
            this.generationsCounter = pop.generationsCounter;
            this.bestDistance = pop.bestDistance;
            this.meanDistance = pop.meanDistance;
            this.bestRoute = pop.bestRoute;
        }
    }
}
