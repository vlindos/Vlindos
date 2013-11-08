namespace Vlindos.DependencyInjection
{
    public static class LifesStyleManagers
    {
        private static ILifestyleManager _singleton;

        public static ILifestyleManager Singleton
        {
            get
            {
                return _singleton ?? (_singleton = new SingletonLifetyleManager());
            }
        }
    }
}
