namespace Database.Operations
{
    public interface ITopOperation<out TOperation>
    {
        TOperation Top(long top);
    }
}