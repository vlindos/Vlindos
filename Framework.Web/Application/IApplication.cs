namespace Framework.Web.Application
{
    public interface IApplication
    {
        bool Initialize(out ApplicationConfiguration applicationConfiguration);
        void Shutdown();
    }
}