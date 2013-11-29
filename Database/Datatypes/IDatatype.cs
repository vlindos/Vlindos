namespace Database.Datatypes
{
    public interface IDataType<T>
    {
        ILogicalOperation[] LogicalOperations { get; set; }
    }
}
