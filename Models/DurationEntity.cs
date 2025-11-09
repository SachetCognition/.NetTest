namespace Equant.SAV2000.ComponentLibrary.MVC.Models
{
    public class DurationEntity
    {
        #region Properties
        public string Hour { get; set; }
        public string Minut { get; set; }
        #endregion

        #region Constructor
        public DurationEntity()
        {
            this.Hour = string.Empty;
            this.Minut = string.Empty;
        }
        #endregion
    }
}
