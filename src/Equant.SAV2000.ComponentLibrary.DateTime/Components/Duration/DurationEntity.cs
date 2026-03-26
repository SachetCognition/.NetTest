namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System;

    [Serializable]
    public class DurationEntity
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public DurationEntity()
        {
            this.Hours = 0;
            this.Minutes = 0;
            this.Seconds = 0;
            this.Milliseconds = 0;
        }

        public DurationEntity(int hours, int minutes, int seconds, int milliseconds)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Milliseconds = milliseconds;
        }

        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(0, this.Hours, this.Minutes, this.Seconds, this.Milliseconds);
        }

        public static DurationEntity FromTimeSpan(TimeSpan timeSpan)
        {
            return new DurationEntity(
                (int)timeSpan.TotalHours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
        }

        public bool IsEmpty
        {
            get
            {
                return this.Hours == 0 && this.Minutes == 0 && this.Seconds == 0 && this.Milliseconds == 0;
            }
        }
    }
}
