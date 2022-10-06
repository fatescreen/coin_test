using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public interface ICity
    {
        public int XCoordinate { get; }
        public int YCoordinate { get; }
        public IList<ICity> Neighbors { get; set; }
        public IList<ICoin> AddCoin(ICoin coin);
        public void MakeDiffusion();
        public int UniqueCoinsTypeAmmount { get; set; }
        public bool IsComplete { get; set; }
        public string CountryName { get; }
        public int UniqueCoinsTypeCount();
    }
}
