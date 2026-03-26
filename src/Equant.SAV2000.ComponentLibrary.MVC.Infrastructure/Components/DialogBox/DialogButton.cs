namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    /// <summary>
    /// Represents a button in a dialog box.
    /// </summary>
    public class DialogButton
    {
        public string Text { get; set; }
        public string CssClass { get; set; }
        public string OnClick { get; set; }
        public bool IsPrimary { get; set; }
    }
}
