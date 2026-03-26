namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Collections.Generic;

    public class DataCollection
    {
        public DataCollection() { this.Items = new List<DataCollectionItem>(); }
        public List<DataCollectionItem> Items { get; set; }
    }

    public class DataCollectionItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}
