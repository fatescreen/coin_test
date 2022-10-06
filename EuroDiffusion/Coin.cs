using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class Coin : ICoin
    {
        public ICity CreatorCity;
        public string CreatorCountry;
        
        
        public Coin (ICity creatorCity)
        {
            this.CreatorCity = creatorCity;
            this.CreatorCountry = creatorCity.CountryName;
        }
    }
}
