using System;
using System.Collections.Generic;
using Vlindos.Common;
using Vlindos.Common.Logging;

namespace Vlindos.Logging.Tests
{
    public class Class1
    {
        public void Tests()
        {
            Logging.ISystemFactory loggingSystemFactory = null;
            var loggingSystem = loggingSystemFactory.GetSystem("Logging.config");
            var messages = new List<string>();
            if (loggingSystem.Start(messages) == false)
            {
                throw new Exception(string.Join(Environment.NewLine, messages));
            }

            ILogger logger = null;
            logger.Debug(new Exception(), "{0}", "s");
            logger.Debug(new Exception(), "{0}", "s");

            loggingSystem.Stop(messages);
        }
    }
}
