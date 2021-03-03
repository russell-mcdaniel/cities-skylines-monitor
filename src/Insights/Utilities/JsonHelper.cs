using System.Text;

namespace Insights.Utilities
{
    /// <summary>
    /// Provides JSON handling services.
    /// </summary>
    /// <remarks>
    /// No JavaScript or JSON helpers, such as System.Web.HttpUtility.JavaScriptStringEncode, are
    /// available in the release of Mono used by Cities: Skylines. Other resources were consulted
    /// to create a routine to escape text for JSON string properties:
    /// 
    /// https://www.json.org/
    /// https://stackoverflow.com/questions/1242118/how-to-escape-json-string
    /// https://stackoverflow.com/questions/19176024/how-to-escape-special-characters-in-building-a-json-string
    /// </remarks>
    public static class JsonHelper
    {
        public static string Escape(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            var builder = new StringBuilder();

            for (var i = 0; i < json.Length; i++)
            {
                var c = json[i];

                switch (c)
                {
                    case '"':
                    case '\\':
                    case '/':
                        builder.Append('\\');
                        builder.Append(c);
                        break;

                    case '\b':
                        builder.Append("\\b");
                        break;

                    case '\f':
                        builder.Append("\\f");
                        break;

                    case '\n':
                        builder.Append("\\n");
                        break;

                    case '\r':
                        builder.Append("\\r");
                        break;

                    case '\t':
                        builder.Append("\\t");
                        break;

                    default:
                        if (c < ' ')
                        {
                            builder.Append("\\u" + ((int)c).ToString("X4", System.Globalization.CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            builder.Append(c);
                        }
                        break;
                }
            }

            return builder.ToString();
        }
    }
}
