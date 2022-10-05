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
        private Dictionary<string, IList<int>> countriesWithCoordinate;

        public CoinDiffusion(Dictionary<string, IList<int>> countriesWithCoordinate, IList<ICountry> countries)
        {
            this.countriesWithCoordinate = countriesWithCoordinate;
            this.Countries = countries;

            foreach (var item in countriesWithCoordinate)
            {
                this.Countries.Add(new Country(item.Key, item.Value));
            }
        }

        public void SetCountriesCount(uint count)
        {
            this.CountriesCount = count;
        }


    }
}
