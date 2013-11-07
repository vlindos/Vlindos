using System;
using System.Collections.Generic;

namespace Vlindos.Logging
{
    public class Message
    {
        public DateTimeOffset DateTimeOffset { get; set; }
        public string Format { get; set; }
        public object[] FormatArguments { get; set; }
        public Level Level { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string OriginNamespace { get; set; }
        public string OriginMethod { get; set; }
        public string FileName { get; set; }
        public uint FileNumber { get; set; }
        public Dictionary<string, string> CustomProperties { get; set; }
    }
}
