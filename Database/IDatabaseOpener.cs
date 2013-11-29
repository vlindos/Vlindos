namespace Database
{
    public interface IDatabaseOpener
    {
        IDatabase<T> OpenDatabase<T>(string directoryPath) where T : IEntity;
    }
}