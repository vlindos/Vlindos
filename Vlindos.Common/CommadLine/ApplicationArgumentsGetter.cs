using System.Collections.Generic;

namespace Vlindos.Common.CommadLine
{
    public interface IArgumentParsers
    {
        Dictionary<IApplicationArgument, List<string>> GetApplicationArguments();
    }

    public class ApplicationArgumentsGetter : IArgumentParsers
    {
        private readonly IApplicationArgument[] _applicationArguments;

        public ApplicationArgumentsGetter(IApplicationArgument[] applicationArguments)
        {
            _applicationArguments = applicationArguments;
        }

        public Dictionary<IApplicationArgument, List<string>> GetApplicationArguments()
        {
            var dictionary = new Dictionary<IApplicationArgument, List<string>>();
            var values = new List<string>();
            var args = System.Environment.GetCommandLineArgs();

            foreach (var applicationArgument in _applicationArguments)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg.ToLowerInvariant() != applicationArgument.ShortCommand.ToLowerInvariant() &&
                        arg.ToLowerInvariant() != applicationArgument.LongCommand.ToLowerInvariant()) continue;
                    if (i + 1 >= args.Length) continue;
                    i++;
                    values.Add(args[i]);
                }
                dictionary.Add(applicationArgument, values);
            }

            return dictionary;
        }
    }
}