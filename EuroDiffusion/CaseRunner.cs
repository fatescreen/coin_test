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
        readonly ICoinDiffusion CoinDiffusion;
        public Dictionary<string, IList<int>> CountriesWithCoordinates;
        public IList<ICountry> Countries;

        public CaseRunner(ICoinDiffusion coinDiffusion, ILogger<CaseRunner> logger)
        {
            this.CoinDiffusion = coinDiffusion;
            this.Logger = logger;
            this.CountriesWithCoordinates= new Dictionary<string, IList<int>>();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return DoWork(stoppingToken);
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            
            while (true)
            {
                try
                {
                    Console.WriteLine($"Write countries count");
                    var countriesCount = uint.Parse(Console.ReadLine());
                    Console.Clear();
                    if (countriesCount <= 0)
                    {
                        this.Logger.LogInformation("Countries count are less than 1");
                        break;
                    }
                    this.CoinDiffusion.SetCountriesCount(countriesCount);

                    for (int i = 1; i <= countriesCount; i++)
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
                }
                catch (Exception e)
                {
                    this.Logger.LogError("Wrong input", e);
                }

                await Task.Delay(0);
            }
        }
    }
}
