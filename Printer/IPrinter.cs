using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coin_test.Printer
{
    public interface IPrinter
    {
        void PrintOutput(IList<string> strings);
    }
}
