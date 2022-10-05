using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class City : ICity
    {
        const int initialCoinValue = 1000000;
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }
        IList<ICity> ICity.Neighbors { get; set; }
        IList<ICoin> Coins;
        public string CountryName;
        public bool isComplete = false;

        public City(string countryName, int xCoordinate, int yCoordinate)
        {
            this.CountryName = countryName;
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;

            ICoin coin = new Coin(this);
            Coins = Enumerable.Repeat(coin, initialCoinValue).ToList();
        }
    }
}
