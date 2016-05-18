using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public class Controller
    {
        private IConnection connection;
        private ICommand command;
        public Controller(IConnection connection, ICommand command)
        {
            this.connection = connection;
            this.command = command;
        }

        public void DoStuff()
        {
            connection.Open();
            command.Run(connection);
            connection.Close();
        }

    }

    public class IConnection
    {
        public void Open() { }

        public void Close() { }

        public event Action SomethingHappened;
    }
}
