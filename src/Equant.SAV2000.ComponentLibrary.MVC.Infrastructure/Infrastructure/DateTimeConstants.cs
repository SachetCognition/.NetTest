namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    public static class DateTimeConstants
    {
        public const string JsEnglishFormat = "mm/dd/yy";
        public const string JsFrenchFormat = "dd/mm/yy";
        public const string EnglishFormat = "M/d/yyyy";
        public const string EnglishDisplayFormat = "MM/dd/yyyy";
        public const string FrenchFormat = "d/M/yyyy";
        public const string FrenchDisplayFormat = "dd/MM/yyyy";
        public const string TimeDefaultValue = "";
        public const string ModelFormat = "model";
        public const string StandardFormat = "standard";
        public const string RegexWeekEnglish = "^[W|w]{1}(([+-]{1}[0-99]{1,2})?)$";
        public const string RegexWeekFrench = "^[S|s]{1}(([+-]{1}[0-99]{1,2})?)$";
        public const string IsUtc = "1";
        public const string IsNonUtc = "0";
    }

    public static class DateLanguages
    {
        public const string English = "English";
        public const string French = "French";
    }
}
