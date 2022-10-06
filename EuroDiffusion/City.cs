using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class City : ICity
    {
        const int initialCoinValue = 1000000;
        const int dailyCoinDivisor = 1000;
        static Random random = new Random();
        public int XCoordinate { get; private set; }
        public int YCoordinate { get; private set; }
        public IList<ICity> Neighbors { get; set; }
        public int UniqueCoinsTypeAmmount = 0;
        
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

        public void MakeDiffusion()
        {            
            var coinsToTransport = Coins.Count() / dailyCoinDivisor;

            for (int i = 0; i < coinsToTransport; i++)
            {
                var randomTargetNeigborCity = this.Neighbors.ElementAt(random.Next(this.Neighbors.Count));
                var randomCoin = this.Coins.ElementAt(random.Next(this.Coins.Count));

                this.Coins.Remove(randomCoin);
                randomTargetNeigborCity.AddCoin(randomCoin);
            };
        }

        public IList<ICoin> AddCoin(ICoin coin)
        {
            this.Coins.Add(coin);
            return this.Coins;
        }

        public int UniqueCoinsTypeCount() 
        {
            IList<ICoin> uniqueCoins = new List<ICoin>();

            

            return 1;
        }
    }
}
