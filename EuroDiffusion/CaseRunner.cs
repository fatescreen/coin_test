using Microsoft.Extensions.DependencyInjection;
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
        private const int maxCountriesAmount = 20;
        private const int minCountriesAmount = 0;
        private const int maxCharactersInName = 25;
        private const int countryNameIndex = 0;
        private const int XLCoordinateIndex = 1;
        private const int YLCoordinateIndex = 2;
        private const int XHCoordinateIndex = 3;
        private const int YHCoordinateIndex = 4;
        private const int minCoordinateValue = 1;
        private const int maxCoordinateValue = 10;
        

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
            DoWork(stoppingToken);
        }

        public void DoWork(CancellationToken stoppingToken)
        {
            ReadCases();
            foreach (var item in Cases)
            {
                var completeCountries = SolveCase(item);
                CasePrinter(Cases.IndexOf(item), completeCountries);
            }
        }

        public void CasePrinter(int caseIndex, Dictionary<string, int> completeCountries)
        {            
            Console.WriteLine(String.Format($"{0} {1}", "Case Number", caseIndex.ToString()));

            foreach (var item in completeCountries)
            {
                var line = String.Format($"\t{0}\t{1}", item.Key, item.Value);
                Console.WriteLine(line);
            }
        }

        public Dictionary<string, int> SolveCase(Dictionary<string, IList<int>> countriesWithCoordinates)
        {
            Dictionary<string, int> completeCountries = new Dictionary<string, int>();

            try
            {
                this.CoinDiffusion = new CoinDiffusion(countriesWithCoordinates, this.Countries);
                this.CoinDiffusion.SetCitiesNeighbors();

                var a = !CoinDiffusion.CheckIsComplete();

                while (!this.CoinDiffusion.CheckIsComplete())
                {
                    this.CoinDiffusion.MakeDiffusion();
                }

                foreach (var country in this.CoinDiffusion.Countries)
                {
                    completeCountries.Add(country.Name, country.DayWhenComplete);
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Can't create {nameof(this.CoinDiffusion)}", e);
            }
            return completeCountries;
        }

        public IList<Dictionary<string, IList<int>>> ReadCases()
        {
            Dictionary<string, IList<int>> countriesWithCoordinates = new Dictionary<string, IList<int>>();

            try
            {
                using (StreamReader reader = new StreamReader(inputPath))
                {
                    string? line;
                    int countriesAmount = 0;
                    bool isCaseAmount = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        isCaseAmount = int.TryParse(line, out countriesAmount);
                        if (line == endOfFile) 
                        {
                            Cases.Add(countriesWithCoordinates);
                            break; 
                        };

                        if (isCaseAmount) 
                        {
                            if (countriesAmount < minCountriesAmount || countriesAmount > maxCountriesAmount)
                            {
                                throw new Exception("Invalid case amount");
                            }
                            if (countriesWithCoordinates.Count() > int.Parse(endOfFile))
                            {
                                Cases.Add(countriesWithCoordinates);
                                countriesWithCoordinates = new Dictionary<string, IList<int>>();
                            }
                            continue; 
                        };

                        var words = line.Split(wordsSeparator);

                        if (words[countryNameIndex].Length > maxCharactersInName)
                        {
                            throw new Exception($"Country name at most {maxCharactersInName} characters");
                        }

                        IList<int> coordinates = new List<int>();
                        coordinates.Add(int.Parse(words[XLCoordinateIndex]));
                        coordinates.Add(int.Parse(words[YLCoordinateIndex]));
                        coordinates.Add(int.Parse(words[XHCoordinateIndex]));
                        coordinates.Add(int.Parse(words[YHCoordinateIndex]));

                        foreach (var coordinate in coordinates)
                        {
                            var isOutOfRange = (coordinate < minCoordinateValue) || (coordinate > maxCoordinateValue);
                            if (isOutOfRange) { throw new Exception($"{nameof(coordinate)} out of range"); };
                        }

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
