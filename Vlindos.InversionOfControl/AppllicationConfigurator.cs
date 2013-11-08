using System;
using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Extensions.IEnumerable;
using Vlindos.InversionOfControl.ConventionConfigurators;

namespace Vlindos.InversionOfControl
{
    public static class AppllicationConfigurator
    {
        private static Dictionary<AppDomain, HashSet<IContainer>> Configured { get; set; }

        static AppllicationConfigurator()
        {
            Configured = new Dictionary<AppDomain, HashSet<IContainer>>();
        }

        public static void Configure(
            AppDomain appDomain, IContainer container, IConventionConfigurator[] conventionConfigurators)
        {
            lock (Configured)
            {
                if (Configured.ContainsKey(appDomain) && Configured[appDomain].Contains(container))
                {
                    throw new InvalidOperationException(
                        "Application from this domain has been configured for this container already. " +
                        "AppllicationConfigurator.Configure() must be called once per AppDomain per Container.");
                }
                if (Configured.ContainsKey(appDomain))
                {
                    Configured[appDomain].Add(container);
                }
                else
                {
                    Configured.Add(appDomain, new HashSet<IContainer> {container});
                }
            }

            var assemblies = appDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var assemblyTypes = assembly.GetTypes();
                Type assemblyConfiguratorType;
                try
                {
                    assemblyConfiguratorType =
                        assemblyTypes.SingleOrDefault(x => x.IsAssignableFrom(typeof (IAssemblyConfigurator)));
                }
                catch (Exception exception)
                {
                    var s = string.Join(string.Format(",{0}", Environment.NewLine),
                                        assemblyTypes.Where(x => x.IsAssignableFrom(typeof (IAssemblyConfigurator)))
                                                        .Select(x => x.FullName));
                    throw new Exception(string.Format("Found more than one assembly configurators in assembly '{0}'. " +
                                                        "There must be only one configurator in assembly. " +
                                                        "Configurator types found:{1}{2}", 
                                                        assembly.FullName, Environment.NewLine, s), 
                                        exception);
                }
                if (assemblyConfiguratorType != null)
                {
                    var assemblyConfigurator = (IAssemblyConfigurator)Activator.CreateInstance(assemblyConfiguratorType);
                    assemblyConfigurator.Configure(container, assemblyTypes);
                }
                assemblyTypes.ForEach(assemlyType => 
                    conventionConfigurators.ForEach(x => x.Configure(container, assemlyType)));
            }
        }
    }
}
