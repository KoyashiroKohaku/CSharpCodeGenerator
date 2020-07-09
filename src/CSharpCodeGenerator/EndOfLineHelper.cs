using System.Text.RegularExpressions;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class EndOfLineHelper
    {
        private static string EndOfLinePattern => "\r\n|\n|\r";

        public static bool Contain(string value)
        {
            return Regex.Match(value, EndOfLinePattern).Success;
        }

        public static string Replace(string input, string replacement)
        {
            return Regex.Replace(input, EndOfLinePattern, replacement);
        }

        public static string[] Split(string value)
        {
            return Regex.Split(value, EndOfLinePattern);
        }
    }
}
