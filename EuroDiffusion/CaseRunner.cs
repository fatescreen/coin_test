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
        public CaseRunner(ICoinDiffusion coinDiffusion, ILogger<CaseRunner> logger)
        {
            this.CoinDiffusion = coinDiffusion;

            this.Logger = logger;

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return DoWork(stoppingToken);
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            Dictionary<string, IList<int>> countriesWithCoordinates = new Dictionary<string, IList<int>>();

            while (true)
            {
                try
                {
                    this.Logger.LogInformation("test");
                    var countriesCount = uint.Parse(Console.ReadLine());
                    this.CoinDiffusion.SetCountriesCount(countriesCount);
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
