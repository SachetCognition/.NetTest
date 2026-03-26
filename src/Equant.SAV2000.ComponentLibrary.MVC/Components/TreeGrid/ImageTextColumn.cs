namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System.Collections.Generic;

    public class ImageTextColumn
    {
        public ImageTextColumn()
        {
            LstImage = new List<TreeGridimage>();
        }

        public string Text { get; set; }
        public List<TreeGridimage> LstImage { get; set; }
    }
}
