﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace coin_test.EuroDiffusion
{
    public class CaseRunner : BackgroundService
    {
        private const char wordsSeparator = ' ';
        private const string endOfFile = "0";
        private const string inputPath = "input.config";
        private const int countryNameIndex = 0;
        private const int XLCoordinateIndex = 1;
        private const int YLCoordinateIndex = 2;
        private const int XHCoordinateIndex = 3;
        private const int YHCoordinateIndex = 4;

        private ILogger<CaseRunner> Logger;
        private ICoinDiffusion CoinDiffusion;
        public Dictionary<string, IList<int>> CountriesWithCoordinates;
        public IList<ICountry> Countries;
        public IList<Dictionary<string, IList<int>>> Cases;

        public CaseRunner(ILogger<CaseRunner> logger)
        {
            this.Logger = logger;
            this.CountriesWithCoordinates= new Dictionary<string, IList<int>>();
            this.Cases = new List<Dictionary<string, IList<int>>>();
            this.Countries = new List<ICountry>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ReadCases();
            SolveCase(this.CountriesWithCoordinates);

            Console.ReadLine();
        }

        public int SolveCase(Dictionary<string, IList<int>> countriesWithCoordinates)
        {            
            try
            {
                this.CoinDiffusion = new CoinDiffusion(countriesWithCoordinates, this.Countries);
                this.CoinDiffusion.SetCitiesNeighbors();

                var a = !CoinDiffusion.CheckIsComplete();

                while (!this.CoinDiffusion.CheckIsComplete())
                {
                    this.CoinDiffusion.MakeDiffusion();
                }
            }
            catch (Exception e)
            {
                Logger.LogError("Can't create coin diffusion case", e);
            }
            return this.CoinDiffusion.DayOfDiffusion;
        }

        public IList<Dictionary<string, IList<int>>> ReadCases()
        {
            Dictionary<string, IList<int>> countriesWithCoordinates = new Dictionary<string, IList<int>>();

            try
            {
                using (StreamReader reader = new StreamReader(inputPath))
                {
                    string? line;
                    int caseAmount = 0;
                    bool isCaseAmount = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        isCaseAmount = int.TryParse(line, out caseAmount);
                        if (line == endOfFile) 
                        {
                            Cases.Add(countriesWithCoordinates);
                            break; 
                        };

                        if (isCaseAmount) 
                        {
                            if (countriesWithCoordinates.Count() > 0)
                            {
                                Cases.Add(countriesWithCoordinates);
                                countriesWithCoordinates = new Dictionary<string, IList<int>>();
                            }
                            continue; 
                        };

                        var words = line.Split(wordsSeparator);

                        IList<int> coordinates = new List<int>();
                        coordinates.Add(int.Parse(words[XLCoordinateIndex]));
                        coordinates.Add(int.Parse(words[YLCoordinateIndex]));
                        coordinates.Add(int.Parse(words[XHCoordinateIndex]));
                        coordinates.Add(int.Parse(words[YHCoordinateIndex]));

                        countriesWithCoordinates.Add(words[countryNameIndex], coordinates);
                    }                  

                }
            }
            catch (Exception e)
            {
                this.Logger.LogError("Wrong input", e);                    
            }               
            
            return this.Cases;
        }
    }
}
