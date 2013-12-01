using System;

namespace Vlindos.Common.Services
{
    public interface IServicesManager : IDisposable
    {
        void StartAll();
        void StopAll();
    }
}