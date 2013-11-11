using System;
using System.Collections;
using System.Collections.Generic;

namespace Vlindos.Web.Tools
{
    public class TypeMergerPolicy
    {
        private readonly IList _ignored;

        public IList Ignored
        {
            get { return _ignored; }
        }

        public TypeMergerPolicy(IList ignored)
        {
            _ignored = ignored;
        }

        public TypeMergerPolicy(object ignoreValue)
        {
            _ignored = new List<object> {ignoreValue};
        }

        public TypeMergerPolicy Ignore(object value)
        {
            _ignored.Add(value);
            return this;
        }

        public Object MergeTypes(Object values1, Object values2)
        {
            return TypeMerger.MergeTypes(values1, values2, this);
        }
    }
}
