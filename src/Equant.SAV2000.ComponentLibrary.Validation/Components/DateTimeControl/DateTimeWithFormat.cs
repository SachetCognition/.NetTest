namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using System.Globalization;

    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    [Serializable]
    public class DateTimeWithFormat
    {
        private readonly string format;
        private readonly bool isModel;
        private double? timeOffset = 1;
        private string dateText;
        private string hourValue;
        private string minuteValue;

        public double? TimeOffset
        {
            get
            {
                return this.timeOffset;
            }
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
            get
            {
                return this.dateText;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.dateText = value;
            }
        }

        public bool IsModel
        {
            get
            {
                return this.isModel;
            }
        }

        public string HourValue
        {
            get
            {
                return this.hourValue;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    this.hourValue = value;
                    return;
                }

                int hourParsed;
                var isHourParsed = int.TryParse(value, out hourParsed);
                if (isHourParsed && (hourParsed <= 23 && hourParsed >= 0))
                {
                    this.hourValue = hourParsed.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public string MinuteValue
        {
            get
            {
                return this.minuteValue;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(value))
                {
                    this.minuteValue = value;
                    return;
                }

                int minuteParsed;
                var isMinuteParsed = int.TryParse(value, out minuteParsed);
                if (isMinuteParsed && (minuteParsed <= 59 && minuteParsed >= 0))
                {
                    this.minuteValue = minuteParsed.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public DateTime? Date
        {
            get
            {
                if (string.IsNullOrEmpty(this.DateText))
                {
                    if (!string.IsNullOrEmpty(this.HourValue) || !string.IsNullOrEmpty(this.MinuteValue))
                    {
                        return null;
                    }
                }

                DateTime parsedDate;

                bool isDate;
                if (this.IsModel)
                {
                    parsedDate = DateComponentHelper.ParseModelDate(this.DateText, this.Format, this.TimeOffset, this.IsUtcMode);
                    isDate = (parsedDate != DateTime.MinValue);
                }
                else
                {
                    isDate = DateTime.TryParseExact(this.DateText, this.Format, null, DateTimeStyles.None, out parsedDate);
                }

                if (!isDate)
                {
                    return null;
                }

                if (string.IsNullOrEmpty(this.HourValue) ^ string.IsNullOrEmpty(this.MinuteValue))
                {
                    return null;
                }

                TimeSpan time;
                int hourParsed;
                var isHourParsed = int.TryParse(this.HourValue, out hourParsed);
                if (isHourParsed && (hourParsed <= 23 && hourParsed >= 0))
                {
                    int minuteParsed;
                    var isMinuteParsed = int.TryParse(this.MinuteValue, out minuteParsed);
                    if (isMinuteParsed && (minuteParsed <= 59 && minuteParsed >= 0))
                    {
                        time = new TimeSpan(hourParsed, minuteParsed, 0);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

                var parsedDateTime = parsedDate + time;
                return parsedDateTime;
            }
        }

        public string Format
        {
            get
            {
                return this.format;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return (string.IsNullOrEmpty(this.dateText) && string.IsNullOrEmpty(this.hourValue) && string.IsNullOrEmpty(this.minuteValue));
            }
        }

        public DateTimeWithFormat(string format, bool isModel)
        {
            if (format != DateTimeConstants.EnglishFormat && format != DateTimeConstants.FrenchFormat)
            {
                throw new ArgumentException("Format value :" + format + " is not correct");
            }

            this.format = format;
            this.isModel = isModel;
            this.DateText = string.Empty;
            this.hourValue = string.Empty;
            this.minuteValue = string.Empty;
            this.TimeOffset = 0;
            this.IsUtcMode = false;
        }

        public override string ToString()
        {
            var hourText = this.HourValue.IsEmpty() ? string.Empty : this.HourValue.PadLeft(2, '0');
            var minuteText = this.MinuteValue.IsEmpty() ? string.Empty : this.MinuteValue.PadLeft(2, '0');
            return "Date :".AppendWithBuilder(this.DateText, ", Time :", hourText, ":", minuteText);
        }
    }
}
