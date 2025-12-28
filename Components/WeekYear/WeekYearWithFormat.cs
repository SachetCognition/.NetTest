namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

public class WeekYearWithFormat
{
    public WeekYearWithFormat()
    {
        Week = 0;
        Year = DateTime.Now.Year;
        Format = "WW/YYYY";
    }

    public WeekYearWithFormat(string format)
    {
        Week = 0;
        Year = DateTime.Now.Year;
        Format = format;
    }

    public int Week { get; set; }
    public int Year { get; set; }
    public string Format { get; set; }

    public override string ToString()
    {
        return $"{Week:D2}/{Year}";
    }
}
