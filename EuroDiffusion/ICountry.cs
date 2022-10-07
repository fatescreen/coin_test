using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public interface ICountry
    {
        public IList<ICity> Cities { get; set; }
        public void MakeDiffusion();
        public bool IsComplete { get; set; }
        public int DayWhenComplete { get; set; }
        public bool CheckIsComplete(int countriesAmount);
    }
}
