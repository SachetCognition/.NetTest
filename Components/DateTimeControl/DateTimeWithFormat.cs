using System;
using System.Globalization;
using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

[Serializable]
public class DateTimeWithFormat
{
        /// <summary>
        /// The format.
        /// </summary>
        private readonly string format;

        /// <summary>
        /// Gets or sets a value indicating whether is model.
        /// </summary>
        private readonly bool isModel;

        /// <summary>
        /// The time offset.
        /// </summary>
        private double? timeOffset = 1;

        /// <summary>
        /// The date text.
        /// </summary>
        private string dateText;

        /// <summary>
        /// Gets or sets the time offset.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value indicating whether is UTC mode.
        /// </summary>
        public bool IsUtcMode { get; set; }

        /// <summary>
        /// Gets or sets the date text.
        /// </summary>
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

        /// <summary>
        /// Gets a value indicating whether it is model date. Model Date means recognized by specific pattern like D+1 or J+1 etc.
        /// </summary>
        public bool IsModel
        {
            get
            {
                return this.isModel;
            }
        }

        /// <summary>
        /// The hour value.
        /// </summary>
        private string hourValue;

        /// <summary>
        /// The minute value.
        /// </summary>
        private string minuteValue;

        /// <summary>
        /// Gets or sets the hour value.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the minute value.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? Date 
        { 
            get
            {
                if (string.IsNullOrEmpty(this.DateText))
                {
                    //if date is empty but time is present, then it is incomplete date and is a model state error
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

                //if date is not empty and exactly one of the time is not submit, then it is incomplete time and is a model state error
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

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        public string Format
        {
            get
            {
                return this.format;
            } 
        }

        /// <summary>
        /// Gets a value indicating whether is empty.
        /// </summary>
        public bool IsEmpty 
        {
            get
            {
                return (string.IsNullOrEmpty(this.dateText) && string.IsNullOrEmpty(this.hourValue) && string.IsNullOrEmpty(this.minuteValue));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeWithFormat"/> class.
        /// </summary>
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

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var hourText = this.HourValue.IsEmpty() ? string.Empty : this.HourValue.PadLeft(2, '0');
            var minuteText = this.MinuteValue.IsEmpty() ? string.Empty : this.MinuteValue.PadLeft(2, '0');
        return "Date :".AppendWithBuilder(DateText, ", Time :", hourText, ":", minuteText);
    }
}
