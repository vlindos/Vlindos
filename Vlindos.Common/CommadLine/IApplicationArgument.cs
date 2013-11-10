namespace Vlindos.Common.CommadLine
{
    public interface IApplicationArgument
    {
        string Id { get; }
        string ShortCommand { get; }
        string LongCommand { get; }
        bool ExpectsValue { get; }
        string DefaultValue { get; }
        string HelpMessage { get; }
    }
}
