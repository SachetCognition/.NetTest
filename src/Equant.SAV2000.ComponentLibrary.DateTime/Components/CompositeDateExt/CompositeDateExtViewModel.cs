namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;

    public class CompositeDateExtViewModel : CompositeDateViewModel
    {
        public int? DepthAdd { get; set; }
        public int? DepthSubtract { get; set; }

        public CompositeDateExtViewModel() : base()
        {
            this.DepthAdd = null;
            this.DepthSubtract = null;
        }
    }
}
