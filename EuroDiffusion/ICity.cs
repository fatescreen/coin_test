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
        IList<ICity> Neighbors { get; set; }
    }
}
