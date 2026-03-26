namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    public class TreeViewColumn
    {
        public string HeaderText { get; set; }
        public string PropertyName { get; set; }
        public ColumnType TreeColumnType { get; set; }
        public bool IsHeaderVisible { get; set; } = true;
        public string AccessText { get; set; }
        public string Render { get; set; }
    }

    public enum ColumnType
    {
        Text,
        CheckBox,
        Image,
        Custom
    }
}
