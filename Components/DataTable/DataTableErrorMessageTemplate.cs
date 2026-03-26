namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Web.UI;

    public class DataTableErrorMessageTemplate
    {
        private readonly DataTableComponent component;
        public DataTableErrorMessageTemplate(DataTableComponent component) { this.component = component; }
        public void Build(HtmlTextWriter writer) { }
    }
}
