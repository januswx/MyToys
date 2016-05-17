using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public class OnceOffCommandRunner
    {
        ICommand _command;

        public OnceOffCommandRunner(ICommand command)
        {
            this._command = command;
        }

        public void Run()
        {
            if (_command == null) return;
            _command.Execute();
            _command = null;
        }
    }
}
