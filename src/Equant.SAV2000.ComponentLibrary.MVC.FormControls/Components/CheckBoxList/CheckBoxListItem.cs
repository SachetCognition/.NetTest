namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;

    [Serializable]
    public class CheckBoxListItem
    {
        public bool Selected { set; get; }
        public string Text { set; get; }
        public string Value { set; get; }
        public bool Disabled { get; set; }
    }
}
