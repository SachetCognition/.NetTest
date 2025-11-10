// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateComponentHelper.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 13/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: Helper class for date functionality.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

    /// <summary>
    /// The date helper.
    /// </summary>
    public static class DateComponentHelper
    {
        /// <summary>
        /// Gets the week date types double.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static string[] WeekDateTypesDouble()
        {
            var weekDateTypes = new[] { EnumDateTypes.WeekBetween.ToString() };
            return weekDateTypes;
        }

        /// <summary>
        /// The standard date types.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>HashSet</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static HashSet<string> StandardDateTypes()
        {
            var standardDateTypes = new HashSet<string>
                                        {
                                            EnumDateTypes.Empty.ToString(),
                                            EnumDateTypes.Between.ToString(),
                                            EnumDateTypes.Equal.ToString(),
                                            EnumDateTypes.GreaterThan.ToString(),
                                            EnumDateTypes.GreaternThanOrEqual.ToString(),
                                            EnumDateTypes.LessThan.ToString(),
                                            EnumDateTypes.LessThanOrEqual.ToString(),
                                            EnumDateTypes.EqualCurrentDate.ToString(),
                                            EnumDateTypes.Between.ToString()
                                        };
            return standardDateTypes;
        }

        /// <summary>
        /// The model date types.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>HashSet</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static HashSet<string> ModelDateTypes()
        {
            var modelDateTypes = new HashSet<string>
                                        {
                                            EnumDateTypes.ModelBetween.ToString(),
                                            EnumDateTypes.ModelEqual.ToString(),
                                            EnumDateTypes.ModelGreaterThan.ToString(),
                                            EnumDateTypes.ModelGreaterThanOrEqual.ToString(),
                                            EnumDateTypes.ModelLessThan.ToString(),
                                            EnumDateTypes.ModelLessThanOrEqual.ToString()
                                        };
            return modelDateTypes;
        }

        /// <summary>
        /// Gets the date types double.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>string[]</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static string[] GetDateTypesDouble()
        {
            var dateTypes = new[] { EnumDateTypes.Between.ToString(), EnumDateTypes.ModelBetween.ToString() };
            return dateTypes;
        }

        /// <summary>
        /// The parse model date.
        /// </summary>
        /// <param name="modelDate">
        /// The model date.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <param name="timeOffset"></param>
        /// <param name="utcMode"></param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static DateTime ParseModelDate(string modelDate, string format, double? timeOffset, bool utcMode)
        {
            if (!CheckValidFormat(format))
            {
                return DateTime.MinValue;
            }

            string regexPattern = format == DateTimeConstants.EnglishFormat ? "^[D|d]{1}(([+-]{1}[0-999]{1,3})?)$" : "^[J|j]{1}(([+-]{1}[0-999]{1,3})?)$";

            var match = Regex.Match(modelDate, regexPattern);

            if (!match.Success)
            {
                return DateTime.MinValue;
            }

            var parsedDate = GetCurrentDate(timeOffset, utcMode);

            if (!String.IsNullOrEmpty(match.Groups[1].Value))
            {
                parsedDate = parsedDate.AddDays(Convert.ToInt32(match.Groups[1].Value, CultureInfo.InvariantCulture));
            }

            return parsedDate;
        }

        /// <summary>
        /// The check valid format.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckValidFormat(string format)
        {
            if (format != DateTimeConstants.FrenchFormat && format != DateTimeConstants.EnglishFormat)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The resolve offset.
        /// </summary>
        /// <param name="dateTimeWithFormat">
        /// The date time with format.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double ResolveOffset(object dateTimeWithFormat)
        {
            if (dateTimeWithFormat == null)
            {
                throw new ArgumentException("Provided dateTimeWithFormat cannot be null");
            }

            dynamic dtf = dateTimeWithFormat;
            return ResolveOffset(dtf.TimeOffset, dtf.IsUtcMode);
        }

        /// <summary>
        /// The resolve offset.
        /// </summary>
        /// <param name="timeOffset">
        /// The time offset.
        /// </param>
        /// <param name="utcMode">
        /// The UTC mode.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double ResolveOffset(double? timeOffset, bool utcMode)
        {
            var timeOffsetParsed = timeOffset.HasValue ? timeOffset.Value : 1;

            // If UTC mode is used, then return offset from UTC. This offset when added to the current date will convert it into UTC date
            // Also if the timeoffset is zero, then the date which is used is UTC date, so this shall also be converted into UTC date
            if (utcMode || (Math.Abs(timeOffsetParsed) < Double.Epsilon))
            {
                return -TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            }

            //if timeoffset is 1, then it should not be used
            if ((Math.Abs(timeOffsetParsed - 1.0) < Double.Epsilon))
            {
                return 0;
            }

            //finally, return the difference of timeoffset in milliseconds with the UTC time offset
            return timeOffsetParsed * 60 * 1000 - TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
        }

        /// <summary>
        /// The resolve offset.
        /// </summary>
        /// <param name="timeOffset">
        /// The time offset.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double ResolveOffset(double? timeOffset)
        {
            if (timeOffset.HasValue)
            {
                return timeOffset.Value;
            }

            return 1;
        }

        /// <summary>
        /// The get current date.
        /// </summary>
        /// <param name="timeOffset">
        /// The time offset.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetCurrentDate(double timeOffset)
        {
            var offsetDateTime = DateTime.Now.AddMilliseconds(timeOffset);
            var currentDate = new DateTime(offsetDateTime.Year, offsetDateTime.Month, offsetDateTime.Day);
            return currentDate;
        }

        /// <summary>
        /// The get current date.
        /// </summary>
        /// <param name="dateTimeWithFormat">
        /// The date time with format.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetCurrentDate(object dateTimeWithFormat)
        {
            var offSet = ResolveOffset(dateTimeWithFormat);
            return GetCurrentDate(offSet);
        }

        /// <summary>
        /// The get current date.
        /// </summary>
        /// <param name="timeOffset">
        /// The time offset.
        /// </param>
        /// <param name="utcMode">
        /// The UTC mode.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime GetCurrentDate(double? timeOffset, bool utcMode)
        {
            var offset = ResolveOffset(timeOffset, utcMode);
            return GetCurrentDate(offset);
        }

        /// <summary>
        /// The text values for date types.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>Dictionary</cref>
        ///     </see>
        ///     .
        /// </returns>
        public static Dictionary<EnumDateTypes, string> TextValuesForDateTypes()
        {
            var toReturn = new Dictionary<EnumDateTypes, string>
                               {
                                   { EnumDateTypes.Between, ApplicationStrings.TPOAR02F02T77CE11_ENTRE },
                                   { EnumDateTypes.Empty, string.Empty },
                                   { EnumDateTypes.Equal, "=".FormatInvariant() },
                                   { EnumDateTypes.EqualCurrentDate, ApplicationStrings.TPOAR02F02T77CE11_DATE_DU_JOUR },
                                   { EnumDateTypes.LessThan, "<".FormatInvariant() },
                                   { EnumDateTypes.GreaterThan, ">".FormatInvariant() },
                                   { EnumDateTypes.LessThanOrEqual, "<=".FormatInvariant() },
                                   { EnumDateTypes.ModelBetween, ApplicationStrings.MULBL001708 },
                                   { EnumDateTypes.ModelEqual, ApplicationStrings.MULBL001709 },
                                   { EnumDateTypes.ModelGreaterThan, ApplicationStrings.MULBL001722 },
                                   { EnumDateTypes.ModelGreaterThanOrEqual, ApplicationStrings.MULBL001724 },
                                   { EnumDateTypes.ModelLessThan, ApplicationStrings.MULBL001723 },
                                   { EnumDateTypes.ModelLessThanOrEqual, ApplicationStrings.MULBL001725 },
                                   { EnumDateTypes.GreaternThanOrEqual, ">=".FormatInvariant() },
                                   { EnumDateTypes.Week, ApplicationStrings.LBL000911 },
                                   { EnumDateTypes.WeekBetween, ApplicationStrings.LBL000912 }
                               };
            return toReturn;
        }

        /// <summary>
        /// The text values for date types.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<EnumExtDateTypes, string> TextValuesForExtDateTypes()
        {
            var toReturn = new Dictionary<EnumExtDateTypes, string>
                               {
                                   { EnumExtDateTypes.Between, ApplicationStrings.TPOAR02F02T77CE11_ENTRE },
                                   { EnumExtDateTypes.EqualCurrentDate, ApplicationStrings.Today_Date },
                                   { EnumExtDateTypes.Equal, ApplicationStrings.equals_To },
                                   { EnumExtDateTypes.LessThan, "<".FormatInvariant() },
                                   { EnumExtDateTypes.GreaterThan, ">".FormatInvariant() }
                               };    
            return toReturn;
        }

        /// <summary>
        /// The text values for days.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<EnumExtDateTypes, string> TextValueForDepth()
        {
           return new Dictionary<EnumExtDateTypes, string>
                  {
                      { EnumExtDateTypes.Add, "+".FormatInvariant() },
                      { EnumExtDateTypes.Subtract, "-".FormatInvariant() },
                      { EnumExtDateTypes.AddSubtract, "+/-".FormatInvariant() }
                  };
        }
    }
}
