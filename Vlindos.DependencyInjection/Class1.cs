using System;
using System.Collections.Generic;

namespace Vlindos.DependencyInjection
{
    // http://ayende.com/blog/tags/ioc
    // http://fukyo-it.blogspot.com/2012/10/ioc-container-benchmark-performance.html
    // http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison
    public enum LifeStyle
    {
        Singleton,
        Transient
    }

    public interface ILifestyleManager
    {
        
    }

    public interface IContainer
    {
        void Register<T, T1>(string registrationId, ILifestyleManager lifeStyleManager)
            where T1 : T;
        void Register<T, T1>(string registrationId, Func<T1> lambdaResolve, ILifestyleManager lifeStyleManager)
            where T1 : T;
        T ResolveAll<T>();
        T Resolve<T>();
        T Resolve<T>(Dictionary<string, object> arguments);

    }
}
