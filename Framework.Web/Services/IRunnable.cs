using System;

namespace Vlindos.Web.Services
{
    public interface IRunnable : IDisposable
    {
        string Identifier { get; set; }
        void Run();
    }
}