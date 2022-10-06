using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class CaseRunner : BackgroundService
    {
        private ILogger<CaseRunner> Logger;
        private ICoinDiffusion CoinDiffusion;
        public Dictionary<string, IList<int>> CountriesWithCoordinates;
        public IList<ICountry> Countries;

        public CaseRunner(ILogger<CaseRunner> logger)
        {
            this.Logger = logger;
            this.CountriesWithCoordinates= new Dictionary<string, IList<int>>();
            this.Countries = new List<ICountry>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.CountriesWithCoordinates = ReadCase();
            SolveCase();

            Console.WriteLine(CountriesWithCoordinates.Keys.First());
        }

        public void SolveCase( )
        {
            try
            {
                this.CoinDiffusion = new CoinDiffusion(this.CountriesWithCoordinates, this.Countries);
                CoinDiffusion.SetCitiesNeighbors();
                CoinDiffusion.MakeDiffusion();
            }
            catch (Exception e)
            {
                Logger.LogError("Can't create coin diffusion case", e);
            }            
        }

        public Dictionary<string, IList<int>> ReadCase()
        {            
            try
            {
                Console.WriteLine($"Write countries amount");
                var countriesAmount = uint.Parse(Console.ReadLine());
                Console.Clear();
                if (countriesAmount <= 0)
                {
                    this.Logger.LogInformation("Countries amount are less than 1");                    
                }

                for (int i = 1; i <= countriesAmount; i++)
                {
                    Console.WriteLine($"Country №{i} name");
                    var countryName = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine($"Enter \"{countryName}\" XL coordinate");
                    var coordinateXL = int.Parse(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine($"Enter \"{countryName}\" YL coordinate");
                    var coordinateYL = int.Parse(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine($"Enter \"{countryName}\" XH coordinate");
                    var coordinateXH = int.Parse(Console.ReadLine());
                    Console.Clear();

                    Console.WriteLine($"Enter \"{countryName}\" YH coordinate");
                    var coordinateYH = int.Parse(Console.ReadLine());
                    Console.Clear();

                    IList<int> coordinates = new List<int>().ToList<int>();
                    coordinates.Add(coordinateXL);
                    coordinates.Add(coordinateYL);
                    coordinates.Add(coordinateXH);
                    coordinates.Add(coordinateYH);

                    this.CountriesWithCoordinates.Add(countryName, coordinates);
                }
                return CountriesWithCoordinates;
            }
            catch (Exception e)
            {
                this.Logger.LogError("Wrong input", e);                    
            }               
            
            return CountriesWithCoordinates;
        }
    }
}
