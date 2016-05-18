using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public interface ILookup
    {
        bool TryLookup(string key, out string value);
    }
}
