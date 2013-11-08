using System;
using System.Collections.Generic;
using System.Reflection;

namespace Vlindos.DependencyInjection
{
    // http://fukyo-it.blogspot.com/2012/10/ioc-container-benchmark-performance.html
    // http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison
    public interface IContainer
    {
        void Register(Type serviceType, Type componentType, ILifestyleManager lifeStyleManager, string registrationId);
        void Register(Type serviceType, Func<object> lambdaResolve, ILifestyleManager lifeStyleManager, string registrationId);
        void RegisterAsProxyFactory(Type factoryType, IEnumerable<MethodInfo> factoryGetMethods);
        T ResolveAll<T>();
        T Resolve<T>();
        T Resolve<T>(Dictionary<string, object> arguments);
    }
}
