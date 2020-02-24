using System.Text.RegularExpressions;

namespace Framework.Utils
{
    public static class StringUtils
    {
        public static string CutNonDigitCharacters(string valueToCut) =>
            Regex.Match(valueToCut, @"^[^aA-aA-zZ-яЯ]*")
                .Value
                .Replace(" ", string.Empty);
    }
}