namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;

    /// <summary>
    /// Used for data binding and properties
    /// </summary>
    [Serializable]
    public class CheckBoxListItem
    {
        /// <summary>
        /// if the value is selected.
        /// </summary>
        public bool Selected { set; get; }
        /// <summary>
        /// text to be displayed as a label
        /// </summary>
        public string Text { set; get; }
        /// <summary>
        /// value for the text
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// the field will be displayed disabled.
        /// </summary>
        public bool Disabled { get; set; }
    }
}
