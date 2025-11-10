using System;
using System.Reflection;
using System.Runtime.InteropServices;
// using System.Web.UI; // Not available in .NET 8 - WebResource attributes removed

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Equant.SAV2000.ComponentLibrary.MVC")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ORANGE")]
[assembly: AssemblyProduct("Equant.SAV2000.ComponentLibrary.MVC")]
[assembly: AssemblyCopyright("Copyright © ORANGE 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7af993f3-9eab-4a6e-8432-d867634de6df")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: CLSCompliant(true)]


// Common Scripts
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Common.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Validator.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.BreadCrumbs.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBox.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.RadioButton.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.RadioButtonList.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.HyperLink.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Button.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTime.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateTimeCommon.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DurationValidator.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DateDuration.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.WeekYear.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ImageToolTip.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TextBox.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TextArea.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DropDownList.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ListBox.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CommentBox.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DualList.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.VerticalMenu.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.HorizontalMenu.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.HorizontalTab.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ActionButton.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CompositeDate.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTable.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTableSelection.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DataTableModifyDelete.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBoxList.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Popup.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.PopupImage.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DropDownMenu.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ClickToVoice.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TreeView.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TreeGrid.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TreeCommon.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.PopOver.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ClickToVoice.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Communicator.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ErrorDisplay.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CustomHeader.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CustomValidator.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DialogBox.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CompositeDateExt.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ProgressBar.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Duration.js", "text/javascript")]
// [assembly: WebResource("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Image.js", "text/javascript")]

