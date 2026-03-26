namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    [Serializable]
    public class DateTimeWithFormat
    {
        private readonly string format;
        private readonly bool isModel;
        private double? timeOffset = 1;
        private string dateText;

        public double? TimeOffset
        {
            get { return this.timeOffset; }
            set
            {
                if (Convert.ToInt32(value, CultureInfo.CurrentCulture) == 0)
                {
                    this.IsUtcMode = true;
                }
                this.timeOffset = value ?? 1;
            }
        }

        public bool IsUtcMode { get; set; }

        public string DateText
        {
            get { return this.dateText; }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.dateText = value;
            }
        }

        public string HourValue { get; set; }
        public string MinuteValue { get; set; }

        public string Format
        {
            get { return this.format; }
        }

        public bool IsModel
        {
            get { return this.isModel; }
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.dateText)
                    && string.IsNullOrEmpty(this.HourValue)
                    && string.IsNullOrEmpty(this.MinuteValue);
            }
        }

        public DateTimeWithFormat(string format, bool isModel)
        {
            this.format = format;
            this.isModel = isModel;
            this.dateText = string.Empty;
            this.HourValue = string.Empty;
            this.MinuteValue = string.Empty;
        }

        public DateTime? Date
        {
            get
            {
                if (string.IsNullOrEmpty(this.dateText))
                {
                    return null;
                }

                if (this.isModel)
                {
                    var modelDate = DateComponentHelper.ParseModelDate(this.dateText, this.format, this.timeOffset, this.IsUtcMode);
                    if (modelDate == DateTime.MinValue)
                    {
                        return null;
                    }
                    return modelDate;
                }

                DateTime parsedDate;
                string parseFormat = this.format == DateTimeConstants.EnglishFormat
                    ? DateTimeConstants.EnglishDisplayFormat
                    : DateTimeConstants.FrenchDisplayFormat;

                if (!DateTime.TryParseExact(this.dateText, parseFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    if (!DateTime.TryParseExact(this.dateText, this.format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    {
                        return null;
                    }
                }

                int hour = 0, minute = 0;
                if (!string.IsNullOrEmpty(this.HourValue))
                {
                    int.TryParse(this.HourValue, out hour);
                }
                if (!string.IsNullOrEmpty(this.MinuteValue))
                {
                    int.TryParse(this.MinuteValue, out minute);
                }

                return new DateTime(parsedDate.Year, parsedDate.Month, parsedDate.Day, hour, minute, 0);
            }
        }
    }
}
