namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System.Globalization;
    using System.Text;

    public static class StringExtension
    {
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

        public static bool IsNull(this string value)
        {
            return (value == null);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string FormatInvariant(this string value)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}", value);
        }
    }
}
