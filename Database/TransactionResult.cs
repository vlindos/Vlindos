namespace Database
{
    public enum TransactionResult
    {
        OpenFailure,
        TimeoutFailure,
        Rollbacked,
        CommitFailure,
        Committed
    }
}