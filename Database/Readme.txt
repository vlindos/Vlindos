structure will be:
namespace 1.2.3 { /// ie no database or table restrictions, namespace goes in directory and it needs permission to be accessed
    public class Entity { // entity structure will be automatically versioned to match exactly database file
		public Guid Id; // required identifier
		// following data types to be automatically supported:
		public DateTimeOffset DateTimeOffset;
		public string String;
		public Guid Guid;
		public int int;
		public byte byte;
		public boolean boolean;
		public byte[] bytes; // collections simple types
		public HashSet<int>; // collection of unique items
    }
}

Datatype specific indexes to supported. Per datatype specific logical operation to be supported.
Full LINQ support

