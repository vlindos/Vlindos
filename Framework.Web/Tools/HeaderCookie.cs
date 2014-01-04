using System;
using System.Globalization;
using System.Text;

namespace Framework.Web.Tools
{
    public class HeaderCookie
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public bool? Secure { get; set; }
        public bool? HttpOnly { get; set; }

        public new string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}={1}", Key, Value);
            if (Domain != null)
            {
                sb.AppendFormat("; Domain={0}", Domain);
            } 
            if (Path != null)
            {
                sb.AppendFormat("; Path={0}", Path);
            } 
            if (Expires != null)
            {
                sb.AppendFormat("; Expires={0}", Expires.Value.ToString("R", CultureInfo.InvariantCulture));
            }
            if (Secure != null)
            {
                sb.Append("; Secure");
            }
            if (HttpOnly != null)
            {
                sb.Append("; HttpOnly");
            }

            return sb.ToString();
        }
    }
}