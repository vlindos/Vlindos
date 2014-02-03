namespace Vlindos.Common.Extensions.String
{
    public static class StringFormat
    {
        public static string Args(this string s, params object[] args)
        {
            return string.Format(s, args);
        }
    }
}
