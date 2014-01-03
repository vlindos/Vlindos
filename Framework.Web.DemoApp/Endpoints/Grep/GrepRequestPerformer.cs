using System;
using System.Text;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.DemoApp.Endpoints.Grep
{
    public interface IGrepRequestPerformer : IPerformer<GrepRequest, string>
    {
    }

    public class GrepRequestPerformer : IGrepRequestPerformer
    {
        public string Perform(GrepRequest request)
        {
            var lines = request.InputString.Split(Environment.NewLine.ToCharArray());
            
            var sb = new StringBuilder();

            foreach (var line in lines)
            {
                if(line.Contains(line) == false) continue;
                sb.Append(line);
            }

            return sb.ToString();
        }
    }
}