using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class CoinDiffusion : ICoinDiffusion
    {
        uint CountriesCount;
        public IList<Country> Countries;

        public void SetCountriesCount(uint count)
        {
            this.CountriesCount = count;
        }
    }
}
