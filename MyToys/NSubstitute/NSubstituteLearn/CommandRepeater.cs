using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public class CommandRepeater
    {
        ICommand _command;
        int _numberOfTimesToCall;

        public CommandRepeater(ICommand command, int numberOfTimesToCall)
        {
            this._command = command;
            this._numberOfTimesToCall = numberOfTimesToCall;
        }

        public void Execute()
        {
            for (var i = 0; i < _numberOfTimesToCall; i++)
            { 
                _command.Execute(); 
            }
        }
    }
}
