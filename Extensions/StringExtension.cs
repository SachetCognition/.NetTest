using System.Globalization;
using System.Text;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions;

/// <summary>
/// Extension methods for string operations.
/// </summary>
public static class StringExtension
{
        /// <summary>
        /// This method appends to the given string using string builder.
        /// </summary>
        /// <param name="startValue">
        /// The start string.
        /// </param>
        /// <param name="valuesToAppend">
        /// The strings to append.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AppendWithBuilder(this string startValue, params string[] valuesToAppend)
        {
            if (valuesToAppend == null)
            {
                return startValue;
            }

            var sbResult = new StringBuilder(startValue);

            foreach (var strToAppend in valuesToAppend)
            {
                if (!string.IsNullOrEmpty(strToAppend))
                {
                    sbResult.Append(strToAppend);
                }
            }

            return sbResult.ToString();
        }

        /// <summary>
        /// Checks if string is null.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNull(this string value)
        {
            return (value == null);
        }

        /// <summary>
        /// The is empty.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// This formats the given string with invariant culture.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string FormatInvariant(this string value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", value);
        }

        /// <summary>
        /// Utility function to espace character which are not supported in jquery selector
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string JQuerySelectorEscape(this string value)
        {
            const string CharsToEscape = @"!""#$%&'()*+,./:;<=>?@[\]^`{|}~";
            var stringBuilder = new StringBuilder();
            if (value != null)
            {
                foreach (var c in value)
                {
                    if (CharsToEscape.IndexOf(c) > 0)
                    {
                        stringBuilder.Append(@"\\");
                    }

                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
}
