namespace Vlindos.Logging
{
    public interface IOutput
    {
        string Type { get; }
        IOutputEngine GetEngine();
    }
}
