using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class Country : ICountry
    {
        public string Name;
        private int XL, YL, XH, YH;
        private readonly int XLIndex = 0, YLIndex = 1, XHIndex = 2, YHIndex = 3;
        public readonly int CountryWidth;
        public readonly int CountryHeight;
        public IList<ICity> Cities { get; set; }

        public Country(string name, IList<int> coordinates)
        {
            Cities = new List<ICity>();
            this.Name = name;
            this.XL = coordinates[XLIndex];
            this.YL = coordinates[YLIndex];
            this.XH = coordinates[XHIndex];
            this.YH = coordinates[YHIndex];

            this.CountryWidth = this.XH - this.XL;
            this.CountryHeight = this.YH - this.YL;

            for (int x = this.XL; x <= this.XH; x++)
            {
                for (int y = this.YL; y <= this.YH; y++)
                {
                    this.Cities.Add(new City(name, x, y));
                }
            }
        }

        public void MakeDiffusion()
        {
            foreach (var city in this.Cities)
            {
                city.MakeDiffusion();
            }
        }
    }
}
