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
        public int DayOfDiffusion { get; set; } = 0;

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

        public void MakeDiffusion(int dayOfDiffusion)
        {
            this.DayOfDiffusion = dayOfDiffusion;
            foreach (var country in this.Countries)
            {
                country.MakeDiffusion();
            }
        }

        public bool CheckIsComplete()
        {
            bool isComplete = true;
            var citiesAmmount = 0;

            foreach (var country in this.Countries)
            {
                citiesAmmount += country.Cities.Count();
            }

            foreach (var country in this.Countries)
            {
                foreach (var city in country.Cities)
                {
                    if (city.UniqueCoinsTypeAmmount == citiesAmmount)
                    {
                        city.IsComplete = true;
                    }
                }
            }

            foreach (var country in this.Countries)
            {
                var isAllCitiesComplete = country.Cities.Where(c => c.IsComplete == true).Count() == country.Cities.Count();
                if (isAllCitiesComplete)
                {
                    country.IsComplete = true;
                    
                    if (country.DayWhenComplete == 0)
                    {
                        country.DayWhenComplete = this.DayOfDiffusion;
                    }
                }
            }


            foreach (var country in this.Countries)
            {
                isComplete &= country.IsComplete;
            }

            return isComplete;
        }
    }
}
