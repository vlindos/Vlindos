using System;

namespace Users.Common.Services
{
    public interface IServicesManager : IDisposable
    {
        void StartAll();
        void StopAll();
    }
}