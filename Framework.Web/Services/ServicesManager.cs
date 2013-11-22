using System;

namespace Vlindos.Web.Services
{
    public interface IServicesManager : IDisposable
    {
        void StartAll();
        void StopAll();
    }
}