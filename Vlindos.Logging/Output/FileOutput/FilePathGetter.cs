using System;

namespace Vlindos.Logging.Output.FileOutput
{
    public interface IFilePathGetterFactory
    {
        IFilePathGetter GetFilePathGetter(string filePathTemplate);
    }

    public interface IFilePathGetter
    {
        string GetFilePath(Message message);
    }

    public class FilePathGetter : IFilePathGetter
    {
        private readonly string _filePathTemplate;

        public FilePathGetter(string filePathTemplate)
        {
            _filePathTemplate = filePathTemplate;
        }

        public string GetFilePath(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
