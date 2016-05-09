using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public interface ICalculator
    {
        int Add(int a, int b);

        int Subtract(int a, int b);

        string Mode { get; set; }

        event EventHandler PoweringUp;
    }
}
