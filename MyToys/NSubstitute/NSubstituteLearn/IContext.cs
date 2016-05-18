using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSubstituteLearn
{
    public interface IContext
    {
        IRequest CurrentRequest { get; }
    }

    public interface IRequest
    {
        IIdentity Identity { get; }
        IIdentity NewIdentity(string name);
    }

    public interface IIdentity
    {
        string Name { get; }
        string[] Roles();
    }
}
