// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeConstants.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 12/05/2014
//   Author:  Sharma Siddharth (54626)
//   Description: The class holding the constant values used within all classes for date time control
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    /// <summary>
    /// The class holding the constant values used within all classes for date time control.
    /// </summary>
    public static class DateTimeConstants
    {
        /// <summary>
        /// The display format english.
        /// </summary>
        public const string JsEnglishFormat = "mm/dd/yy";

        /// <summary>
        /// The display format french.
        /// </summary>
        public const string JsFrenchFormat = "dd/mm/yy";

        /// <summary>
        /// The parsing format for english.
        /// </summary>
        public const string EnglishFormat = "M/d/yyyy";

        /// <summary>
        /// The display format for english.
        /// </summary>
        public const string EnglishDisplayFormat = "MM/dd/yyyy";

        /// <summary>
        /// The parsing format for french.
        /// </summary>
        public const string FrenchFormat = "d/M/yyyy";

        /// <summary>
        /// The display format for french.
        /// </summary>
        public const string FrenchDisplayFormat = "dd/MM/yyyy";

        /// <summary>
        /// The time default value.
        /// </summary>
        public const string TimeDefaultValue = "";

        /// <summary>
        /// The model format.
        /// </summary>
        public const string ModelFormat = "model";

        /// <summary>
        /// The standard format.
        /// </summary>
        public const string StandardFormat = "standard";

        /// <summary>
        /// The regular expression for week in english.
        /// </summary>
        public const string RegexWeekEnglish = "^[W|w]{1}(([+-]{1}[0-99]{1,2})?)$";

        /// <summary>
        /// The regular expression for week in french.
        /// </summary>
        public const string RegexWeekFrench = "^[S|s]{1}(([+-]{1}[0-99]{1,2})?)$";

        /// <summary>
        /// The is UTC.
        /// </summary>
        public const string IsUtc = "1";

        /// <summary>
        /// The is non UTC.
        /// </summary>
        public const string IsNonUtc = "0";
    }

    /// <summary>
    /// The date languages.
    /// </summary>
    public static class DateLanguages
    {
        /// <summary>
        /// The english.
        /// </summary>
        public const string English = "English";

        /// <summary>
        /// The french.
        /// </summary>
        public const string French = "French";
    }
}
