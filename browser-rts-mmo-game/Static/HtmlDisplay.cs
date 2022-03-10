using System.Text.RegularExpressions;

namespace BrowserGame.Static
{
	public static class HtmlDisplay
	{
        // From:
        // https://stackoverflow.com/a/5796793
        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        public static string ToDisplayName<TEnum>(this TEnum e) where TEnum : Enum
        {
            return e.ToString().SplitCamelCase();
        }
    }
}
