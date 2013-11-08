using System;
using System.Collections.Generic;
using System.Linq;

namespace Vlindos.DependencyInjection
{
    public static class AppllicationConfigurationManager
    {
        private static Dictionary<AppDomain, HashSet<IContainer>> Configured { get; set; }

        static AppllicationConfigurationManager()
        {
            Configured = new Dictionary<AppDomain, HashSet<IContainer>>();
        }

        public static void Configure(AppDomain appDomain, IContainer container, IConventionConfigurator[] conventionConfigurators)
        {
            lock (Configured)
            {
                if (Configured.ContainsKey(appDomain) && Configured[appDomain].Contains(container))
                {
                    throw new InvalidOperationException(
                        "Application from this domain has been configured for this container already. " +
                        "Did you called AppllicationConfigurationManager.Configure() more than once?");
                }
                if (Configured.ContainsKey(appDomain))
                {
                    Configured[appDomain].Add(container);
                }
                else
                {
                    Configured.Add(appDomain, new HashSet<IContainer>{container});
                }

                var assemblies = appDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    var assemblyTypes = assembly.GetTypes();
                    var assemblyConfiguratorType = assemblyTypes.FirstOrDefault(x => x.IsAssignableFrom(typeof(IAssemblyConfigurator)));
                    if (assemblyConfiguratorType != null)
                    {
                        var assemblyConfigurator = (IAssemblyConfigurator)Activator.CreateInstance(assemblyConfiguratorType);
                        assemblyConfigurator.Configure(container, assemblyTypes);
                    }
                    foreach (var assemblyType in assemblyTypes)
                    {
                        foreach (var conventionConfigurator in conventionConfigurators)
                        {
                            conventionConfigurator.Configure(container, assemblyType);
                        }
                    }
                }
            }
        }
    }
}
