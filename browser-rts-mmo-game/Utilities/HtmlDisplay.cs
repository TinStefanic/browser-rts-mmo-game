using System.Text.RegularExpressions;

namespace BrowserGame.Utilities
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

        public static string ToHtmlDisplay(this TimeSpan span)
		{
            string text = 
                $"{span.Days} day{(span.Days == 1 ? "" : "s")}, " + 
                $"{span.Hours} hour{(span.Hours == 1 ? "" : "s")}, " +
                $"{span.Minutes} minute{(span.Minutes == 1 ? "" : "s")} " +
                $"and {span.Seconds} second{(span.Seconds == 1 ? "" : "s")}";

            return text;
        }

        public static string FirstCharToLower(this string s)
		{
            if (s == null || s.Length == 1) return s?.ToLowerInvariant();
            return char.ToLowerInvariant(s[0]) + s[1..];
		}
    }
}
