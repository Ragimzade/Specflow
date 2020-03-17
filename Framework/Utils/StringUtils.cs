using System.Text.RegularExpressions;

namespace Framework.Utils
{
    public static class StringUtils
    {
        public static string CutNonDigitCharacters(this string valueToCut) =>
            Regex.Match(valueToCut, @"^[^aA-aA-zZ-яЯ]*")
                .Value
                .Replace(" ", string.Empty);

        public static string CutNonDigitCharacter(this string valueToCut) =>
            Regex.Replace(valueToCut, @"^\s+$[\r\n]*",
                string.Empty, RegexOptions.Multiline);
        
        public static string RemoveExcessCharacters(this string value, int maxLen)
        {
            return value.Length > maxLen ? value.Substring(0, maxLen) : value;
        }

        public static string ReplaceNewLineWithSpace(this string value)
        {
            return Regex.Replace(value, "\\s+", " ");
        }
    }
}