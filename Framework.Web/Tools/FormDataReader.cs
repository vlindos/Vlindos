using System.Collections.Generic;
using Vlindos.Common.Streams;

namespace Framework.Web.Tools
{
    public interface IFormDataMaximumDataConstantProvider
    {
        int FormDataMaximumLength { get; }
    }

    public interface IFormDataReader
    {
        bool ReadFormData(IInputStream inputStream, List<string> messages, out Dictionary<string, string> formData);
    }

    public class FormDataReader : IFormDataReader
    {
        private readonly int _formDataMaximumLength;

        public FormDataReader(IFormDataMaximumDataConstantProvider provider)
        {
            _formDataMaximumLength = provider.FormDataMaximumLength;
        }

        public bool ReadFormData(IInputStream inputStream, List<string> messages, out Dictionary<string, string> formData)
        {
            throw new System.NotImplementedException();
        }
    }
}