using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class Coin : ICoin
    {
        public string CreatorCountry { get; set; }

        public Coin (string creatorCountry)
        {
            this.CreatorCountry = creatorCountry;
        }
    }
}
