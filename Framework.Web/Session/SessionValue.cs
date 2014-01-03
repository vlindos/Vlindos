using System;

namespace Framework.Web.Session
{
    public class SessionValue
    {
        string Id { get; set; }
        DateTimeOffset? Expires { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}