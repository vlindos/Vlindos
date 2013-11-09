namespace Vlindos.Common.CommadLine
{
    public interface IApplicationArgument
    {
        string Id { get; set; }
        string ShortCommand { get; set; }
        string LongCommand { get; set; }
        bool ExpectsValue { get; set; }
        string DefaultValue { get; set; }
        string HelpMessage { get; set; }
    }
}
