using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public interface IEngine
    {
        event EventHandler Idling;
        event EventHandler<LowFuelWarningEventArgs> LowFuelWarning;
        event Action<int> RevvedAt;
    }

    public class LowFuelWarningEventArgs : EventArgs
    {
        public int PercentLeft { get; private set; }

        public LowFuelWarningEventArgs(int percentLeft)
        {
            PercentLeft = percentLeft;
        }
    }
}
