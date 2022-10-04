using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.Printer
{
    public class Printer : IPrinter
    {
        public void PrintOutput(IList<string> strings)
        {
            strings.ToList().ForEach(item => Print(item));
        }

        public void PrintOutput(string line)
        {
            Print(line);
        }

        private void Print(string line)
        {
            Console.WriteLine(line);
        }
    }
}
