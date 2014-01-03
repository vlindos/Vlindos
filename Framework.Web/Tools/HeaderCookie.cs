using System;

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
    }
}