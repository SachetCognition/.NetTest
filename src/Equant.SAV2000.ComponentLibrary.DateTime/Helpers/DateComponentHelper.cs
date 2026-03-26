namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Globalization;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    public static class DateComponentHelper
    {
        public static DateTime ParseModelDate(string modelText, string format, double? timeOffset, bool isUtcMode)
        {
            if (string.IsNullOrEmpty(modelText))
            {
                return DateTime.MinValue;
            }

            var now = DateTime.Now;
            if (isUtcMode)
            {
                now = DateTime.UtcNow;
            }

            modelText = modelText.Trim().ToUpperInvariant();

            // Parse model format: D+5, D-3, J+2, J-1
            if (modelText.Length >= 2)
            {
                char prefix = modelText[0];
                if (prefix == 'D' || prefix == 'J')
                {
                    string offsetStr = modelText.Substring(1);
                    if (!string.IsNullOrEmpty(offsetStr))
                    {
                        if (int.TryParse(offsetStr, NumberStyles.Any, CultureInfo.InvariantCulture, out int dayOffset))
                        {
                            return now.AddDays(dayOffset);
                        }
                    }
                    else
                    {
                        return now;
                    }
                }
            }

            return DateTime.MinValue;
        }

        public static string GetJsDateFormat(string format)
        {
            if (format == DateTimeConstants.FrenchFormat)
            {
                return DateTimeConstants.JsFrenchFormat;
            }
            return DateTimeConstants.JsEnglishFormat;
        }

        public static string GetDisplayFormat(string format)
        {
            if (format == DateTimeConstants.FrenchFormat)
            {
                return DateTimeConstants.FrenchDisplayFormat;
            }
            return DateTimeConstants.EnglishDisplayFormat;
        }
    }
}
