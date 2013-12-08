using System;
using System.Collections.Generic;
using System.Reflection;
using Vlindos.InversionOfControl.LifestyleManagers;

namespace Vlindos.InversionOfControl
{
    // http://fukyo-it.blogspot.com/2012/10/ioc-container-benchmark-performance.html
    // http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison
    public interface IContainer : IDisposable
    {
        void Register(Type serviceType, Type componentType, ILifestyleManager lifeStyleManager, string registrationId);
        void Register(Type serviceType, Func<object> lambdaResolve, ILifestyleManager lifeStyleManager, string registrationId);
        void RegisterAsProxyFactory(Type factoryType, IEnumerable<MethodInfo> factoryGetMethods);
        T ResolveAll<T>();
        T Resolve<T>();
        T Resolve<T>(List<object> arguments);
    }


    public class Container : IContainer
    {
        public void Register(Type serviceType, Type componentType, ILifestyleManager lifeStyleManager, string registrationId)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Func<object> lambdaResolve, ILifestyleManager lifeStyleManager, string registrationId)
        {
            throw new NotImplementedException();
        }

        public void RegisterAsProxyFactory(Type factoryType, IEnumerable<MethodInfo> factoryGetMethods)
        {
            throw new NotImplementedException();
        }

        public T ResolveAll<T>()
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>(List<object> arguments)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
