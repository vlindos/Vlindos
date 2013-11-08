namespace Vlindos.InversionOfControl.LifestyleManagers
{
    public static class Lifestyle
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
