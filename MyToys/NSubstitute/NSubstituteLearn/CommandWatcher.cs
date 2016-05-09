using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public class CommandWatcher
    {
        ICommand _command;

        public CommandWatcher(ICommand command)
        {
            this._command = command;
            this._command.Executed += OnExecuted;
        }

        public bool DidStuff { get; private set; }

        public void OnExecuted(object o, EventArgs e)
        {
            DidStuff = true; 
        }
    }
}
