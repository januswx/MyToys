using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public interface INumberParser
    {
        int[] Parse(string expression);
    }

    public interface INumberParserFactory
    {
        INumberParser Create(char delimiter);
    }
}
