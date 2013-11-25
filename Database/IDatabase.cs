using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IDatabase
    {
        string Namespace { get; set; }

        IDatabaseHandle Open(string filepath);

        IDatabaseHandle Handle { get; set; }
    }

    public interface IDatabaseHandle : IDisposable
    {

    }

    public interface IOperation
    {
        IOperationType OperationType { get; set; }
    }

    public interface IOperationType
    {
    }

    public interface IOperationResult
    {
    }
}
