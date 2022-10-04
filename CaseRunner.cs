using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coin_test
{
    public class CaseRunner : BackgroundService
    {
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return DoWork(stoppingToken);
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (true)
            {
                var newLine = Console.ReadLine();
                await Console.Out.WriteAsync($"" + newLine);
                await Task.Delay(0);
            }
        }
    }
}
