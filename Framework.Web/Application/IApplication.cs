namespace Framework.Web.Application
{
    public interface IApplication
    {
        ApplicationConfiguration Initialize();
        void Shutdown();
    }
}