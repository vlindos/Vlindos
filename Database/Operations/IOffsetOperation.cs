namespace Database.Operations
{
    public interface IOffsetOperation<out TOperation>
    {
        TOperation Offset(long offset);
    }
}