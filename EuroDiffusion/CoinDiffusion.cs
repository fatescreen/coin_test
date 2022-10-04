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
        private Dictionary<string, IList<int>> CountriesWithCoordinates;       

        public CoinDiffusion(Dictionary<string, IList<int>> countriesWithCoordinates)
        {
            this.CountriesWithCoordinates = countriesWithCoordinates;
            Countries = new List<Country>();

            foreach (var item in countriesWithCoordinates)
            {
                Countries.Add(new Country(item.Key));
            }
        }

        public void SetCountriesCount(uint count)
        {
            this.CountriesCount = count;
        }


    }
}
