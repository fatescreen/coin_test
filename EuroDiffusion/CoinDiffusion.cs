using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class CoinDiffusion : ICoinDiffusion
    {
        uint CountriesAmount;
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

        public void SetCountriesCount(uint amount)
        {
            this.CountriesAmount = amount;
        }

        public void SetCitiesNeighbors()
        {
            List<ICity> allCities = new List<ICity>();
            foreach (var country in Countries)
            {
                allCities.AddRange(country.Cities);
            }

            foreach (var city in allCities)
            {
                var neighbors = allCities.Where(c => 
                {
                    bool isNeighborOnX = ((c.XCoordinate - city.XCoordinate) == 1) ^ ((city.XCoordinate - c.XCoordinate) == 1);
                    bool isNeighborOnY = ((c.YCoordinate - city.YCoordinate) == 1) ^ ((city.YCoordinate - c.YCoordinate) == 1);
                    bool isSameCoordinateX = c.XCoordinate == city.XCoordinate;
                    bool isSameCoordinateY = c.YCoordinate == city.YCoordinate;

                    bool isNeigbor = (isNeighborOnX && isSameCoordinateY) ^ (isNeighborOnY && isSameCoordinateX);

                    return isNeigbor;
                }).ToList();
                city.Neighbors = neighbors;
            }
        }

        public void MakeDiffusion()
        {
            foreach (var country in this.Countries)
            {
                country.MakeDiffusion();
            }
        }


    }
}
