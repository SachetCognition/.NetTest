namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDuration
{
    using System;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

    [Serializable]
    public class DateDuration
    {
        public DateTimeWithFormat DateValue { get; set; }
        public int? DurationDays { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }

        public DateDuration()
        {
            this.DurationDays = null;
            this.DurationHours = null;
            this.DurationMinutes = null;
        }

        public TimeSpan? GetDuration()
        {
            if (!this.DurationDays.HasValue && !this.DurationHours.HasValue && !this.DurationMinutes.HasValue)
            {
                return null;
            }

            return new TimeSpan(
                this.DurationDays ?? 0,
                this.DurationHours ?? 0,
                this.DurationMinutes ?? 0,
                0);
        }
    }
}
