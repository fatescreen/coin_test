using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public interface ICoinDiffusion
    {
        public IList<ICountry> Countries { get; set; }
        public void SetCountriesCount(uint count);
        public void SetCitiesNeighbors();
        public void MakeDiffusion();
        public bool CheckIsComplete();
        public int DayOfDiffusion { get; }
    }
}
