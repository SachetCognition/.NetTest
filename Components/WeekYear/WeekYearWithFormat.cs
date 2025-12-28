namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

public enum WeekFormat
{
    English,
    French
}

public class WeekAndYear
{
    public int Week { get; set; }
    public int Year { get; set; }
}

public class WeekYearWithFormat
{
    public WeekYearWithFormat()
    {
        Week = 0;
        Year = DateTime.Now.Year;
        Format = WeekFormat.English;
        WeekText = string.Empty;
        YearText = string.Empty;
    }

    public WeekYearWithFormat(WeekFormat format)
    {
        Week = 0;
        Year = DateTime.Now.Year;
        Format = format;
        WeekText = string.Empty;
        YearText = string.Empty;
    }

    public int Week { get; set; }
    public int Year { get; set; }
    public WeekFormat Format { get; set; }
    public string WeekText { get; set; }
    public string YearText { get; set; }
    public double TimeOffset { get; set; }
    public bool IsUtcMode { get; set; }

    public bool IsEmpty => Week == 0 && Year == 0;

    public override string ToString()
    {
        return $"{Week:D2}/{Year}";
    }
}
