﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class Country : ICountry
    {
        public string Name;
        public Country(string name)
        {
            this.Name = name;
        }
    }
}
