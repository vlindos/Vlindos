using System;

namespace Users.Common.Services
{
    public interface IRunnable : IDisposable
    {
        string Identifier { get; set; }
        void Run();
    }
}