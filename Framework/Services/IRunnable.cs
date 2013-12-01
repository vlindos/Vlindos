using System;

namespace Vlindos.Common.Services
{
    public interface IRunnable : IDisposable
    {
        string Identifier { get; set; }
        void Run();
    }
}