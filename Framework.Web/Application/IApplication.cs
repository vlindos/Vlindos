using System;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IApplication : IDisposable
    {
        bool Initialize();
        ApplicationConfiguration Configuration { get; }
    }
}