namespace Database
{
    public enum TransactionResult
    {
        TransactionAlreadyStarted,
        OpenFailure,
        TimeoutFailure,
        Rollbacked,
        CommitFailure,
        Committed
    }
}