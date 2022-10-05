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

        public void SetCitiesNeighbors()
        {
            IList<ICity> allCities = new List<ICity>();
            foreach (var country in Countries)
            {
                allCities.Concat(country.Cities);
            }

            foreach (var city in allCities)
            {
                var neighbors = allCities.Where(c => 
                {
                    bool isNeighborOnX = ((c.XCoordinate - city.XCoordinate) == 1) || ((c.XCoordinate + city.XCoordinate) == 1);
                    bool isNeighborOnY = ((c.YCoordinate - city.YCoordinate) == 1) || ((c.YCoordinate + city.YCoordinate) == 1);
                    bool isNeigbor = (isNeighborOnX || isNeighborOnY);

                    return isNeigbor;
                }).ToList();
                city.Neighbors = neighbors;
            }
        }


    }
}
