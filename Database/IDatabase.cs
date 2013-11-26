using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public interface IDatabase : IDisposable
    {
        string Namespace { get; set; }
    }

    public interface IDatabaseManager
    {
        IDatabase OpenDatabase(string filepath);
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
