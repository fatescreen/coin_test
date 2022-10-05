using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class City : ICity
    {
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }
        IList<ICity> Neighbors { get; set; }

        public City(string countryName, int xCoordinate, int yCoordinate)
        {
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;            
        }
    }
}
