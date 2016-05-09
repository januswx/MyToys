using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public class SomethingThatNeedsACommand
    {
        ICommand _command;

        public SomethingThatNeedsACommand(ICommand command)
        {
            this._command = command;
        }

        public void DoSomething() { _command.Execute(); }

        public void DontDoAnything() { }
    }
}
