namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    public class WeekAndYear
    {
        public int Week { get; set; }
        public int Year { get; set; }
        public bool IsEmpty { get { return Week == 0 && Year == 0; } }
    }
}
