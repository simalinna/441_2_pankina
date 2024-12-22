using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class RandomDistances
    {
        public const int MAX_DISTANCE = 100;
        public int[,] distances { get; set; }

        public RandomDistances(int citiesCount)
        {
            distances = createRandomDistances(citiesCount);
        }

        public int[,] createRandomDistances(int citiesCount)
        {
            int maxDistance = MAX_DISTANCE;
            int minDistance = maxDistance % 2 == 0 ? maxDistance / 2 : maxDistance / 2 + 1;

            Random random = new Random();

            int[,] distances = new int[citiesCount, citiesCount];

            for (int i = 0; i < citiesCount - 1; i++)
            {
                for (int j = i + 1; j < citiesCount; j++)
                {
                    distances[i, j] = random.Next(minDistance, maxDistance + 1);
                    distances[j, i] = distances[i, j];
                }
            }

            for (int i = 0; i < citiesCount - 1; i++)
            {
                for (int j = i + 1; j < citiesCount; j++)
                {
                    int currentMaxDistance = 2 * maxDistance;
                    int currentMinDistance = 0;
                    for (int k = 0; k < citiesCount; k++)
                    {
                        if (k != i && k != j)
                        {
                            int currentDistance = distances[i, k] + distances[k, j];
                            int currentDifference = Math.Abs(distances[i, k] - distances[k, j]);
                            currentMaxDistance = currentDistance < currentMaxDistance ? currentDistance : currentMaxDistance;
                            currentMinDistance = currentDifference > currentMinDistance ? currentDifference : currentMinDistance;
                        }
                    }
                    distances[i, j] = random.Next(currentMinDistance, currentMaxDistance + 1);
                    distances[j, i] = distances[i, j];
                }
            }
            return distances;
        }

    }
}
