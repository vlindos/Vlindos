namespace Database.Datatypes
{
    public interface ILogicalOperation
    {
    }

    public interface IDatatype<T> where T : struct
    {
        ILogicalOperation[] LogicalOperations { get; set; }
    }
}
