﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public interface ICountry
    {
        public IList<ICity> Cities { get; set; }
    }
}
