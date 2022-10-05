using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class CoinDiffusion : ICoinDiffusion
    {
        uint CountriesCount;
        public IList<ICountry> Countries;
        public Dictionary<string, IList<int>> CountriesWithCoordinates { get; set; } = new Dictionary<string, IList<int>>();

        public CoinDiffusion(IList<ICountry> countries)
        {
            this.Countries = countries;
        }

        public void SetCountriesCount(uint count)
        {
            this.CountriesCount = count;
        }


    }
}
