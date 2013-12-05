using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web.Tools
{
    public interface IAcceptHeaderPrioritizer
    {
        IEnumerable<string> Prioritize(string acceptHeader);
    }

    public class AcceptHeaderPrioritizer : IAcceptHeaderPrioritizer
    {
        public IEnumerable<string> Prioritize(string acceptHeader)
        {
            var acceptTokens = acceptHeader.Split(',');
            var acceptDict = new Dictionary<string, float>();
            foreach (var acceptToken in acceptTokens)
            {
                var normalizedToken = acceptToken.Trim().ToLowerInvariant();
                var tokens = normalizedToken.Split(';');
                var priority = 1.0f;
                var retoken = new StringBuilder();
                foreach (var token in tokens)
                {
                    var subtokens = token.Split('=');
                    if (subtokens.Length == 2 && subtokens[0] == "q")
                    {
                        if (float.TryParse(subtokens[1], out priority) == false)
                        {
                            priority = 1.0f;
                        }
                    }
                    else
                    {
                        if (retoken.Length == 0)
                        {
                            retoken.Append(token);
                        }
                        else
                        {
                            retoken.AppendFormat(";{0}", token);
                        }
                    }
                }
                acceptDict[retoken.ToString()] = priority;
            }

            var sortedAcceptTokens = acceptDict.Keys.ToList();
            sortedAcceptTokens.Sort(
                (firstToken, secondToken) => (-1) * acceptDict[firstToken].CompareTo(acceptDict[secondToken]));

            return sortedAcceptTokens;
        }
    }
}