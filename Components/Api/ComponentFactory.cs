namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomValidator;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateDurationControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DualList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Duration;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ListBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.PopOver;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Popup;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ProgressBar;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButtonList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

    public class ComponentFactory<TModel>
    {
        private readonly HtmlHelper<TModel> htmlHelper;
        private readonly ScriptRendererBuilder scriptRendererBuilder;
        private readonly StyleRendererBuilder styleRendererBuilder;

        public ComponentFactory(HtmlHelper<TModel> htmlHelper, ScriptRendererBuilder scriptRendererBuilder, StyleRendererBuilder styleRendererBuilder)
        {
            this.htmlHelper = htmlHelper;
            this.scriptRendererBuilder = scriptRendererBuilder;
            this.styleRendererBuilder = styleRendererBuilder;
        }

        public ActionButtonBuilder ActionButton()
        {
            var component = new ActionButtonComponent(this.htmlHelper);
            return new ActionButtonBuilder(component, null);
        }

        public BreadCrumbsBuilder BreadCrumbs()
        {
            var component = new BreadCrumbsComponent(this.htmlHelper);
            return new BreadCrumbsBuilder(component, null);
        }

        public ButtonBuilder Button()
        {
            var component = new ButtonComponent(this.htmlHelper);
            return new ButtonBuilder(component, null);
        }

        public CheckBoxBuilder CheckBox()
        {
            var component = new CheckBoxComponent(this.htmlHelper);
            return new CheckBoxBuilder(component, null);
        }

        public CheckBoxListBuilder CheckBoxList()
        {
            var component = new CheckBoxListComponent(this.htmlHelper);
            return new CheckBoxListBuilder(component, null);
        }

        public ClickToVoiceBuilder ClickToVoice()
        {
            var component = new ClickToVoiceComponent(this.htmlHelper);
            return new ClickToVoiceBuilder(component, null);
        }

        public CommentBoxBuilder CommentBox()
        {
            var component = new CommentBoxComponent(this.htmlHelper);
            return new CommentBoxBuilder(component, null);
        }

        public CompositeDateBuilder CompositeDate()
        {
            var component = new CompositeDateComponent(this.htmlHelper);
            return new CompositeDateBuilder(component, null);
        }

        public CompositeDateExtBuilder CompositeDateExt()
        {
            var component = new CompositeDateExtComponent(this.htmlHelper);
            return new CompositeDateExtBuilder(component, null);
        }

        public CommunicatorBuilder Communicator()
        {
            var component = new CommunicatorComponent(this.htmlHelper);
            return new CommunicatorBuilder(component, null);
        }

        public CustomHeaderBuilder CustomHeader()
        {
            var component = new CustomHeaderComponent(this.htmlHelper);
            return new CustomHeaderBuilder(component, null);
        }

        public CustomLabelBuilder CustomLabel()
        {
            var component = new CustomLabelComponent(this.htmlHelper);
            return new CustomLabelBuilder(component, null);
        }

        public CustomValidatorBuilder CustomValidator()
        {
            var component = new CustomValidatorComponent(this.htmlHelper);
            return new CustomValidatorBuilder(component, null);
        }

        public DataTableBuilder DataTable()
        {
            var component = new DataTableComponent(this.htmlHelper);
            return new DataTableBuilder(component, null);
        }

        public DateDurationBuilder DateDuration()
        {
            var component = new DateDurationComponent(this.htmlHelper);
            return new DateDurationBuilder(component, null);
        }

        public DateTimeBuilder DateTime()
        {
            var component = new DateTimeComponent(this.htmlHelper);
            return new DateTimeBuilder(component, null);
        }

        public DialogBoxBuilder DialogBox()
        {
            var component = new DialogBoxComponent(this.htmlHelper);
            return new DialogBoxBuilder(component, null);
        }

        public DropDownListBuilder DropDownList()
        {
            var component = new DropDownListComponent(this.htmlHelper);
            return new DropDownListBuilder(component, null);
        }

        public DropDownMenuBuilder DropdownMenu()
        {
            var component = new DropDownMenuComponent(this.htmlHelper);
            return new DropDownMenuBuilder(component, null);
        }

        public DualListBuilder DualList()
        {
            var component = new DualListComponent(this.htmlHelper);
            return new DualListBuilder(component, null);
        }

        public DurationBuilder Duration()
        {
            var component = new DurationComponent(this.htmlHelper);
            return new DurationBuilder(component, null);
        }

        public ErrorBuilder Error()
        {
            var component = new ErrorComponents(this.htmlHelper);
            return new ErrorBuilder(component, null);
        }

        public FileUploadBuilder FileUpload()
        {
            var component = new FileUploadComponent(this.htmlHelper);
            return new FileUploadBuilder(component, null);
        }

        public HorizontalMenuBuilder HorizontalMenu()
        {
            var component = new HorizontalMenuComponent(this.htmlHelper);
            return new HorizontalMenuBuilder(component, null);
        }

        public HorizontalTabBuilder HorizontalTab()
        {
            var component = new HorizontalTabComponent(this.htmlHelper);
            return new HorizontalTabBuilder(component, null);
        }

        public HyperLinkBuilder HyperLink()
        {
            var component = new HyperLinkComponent(this.htmlHelper);
            return new HyperLinkBuilder(component, null);
        }

        public ImageBuilder Image()
        {
            var component = new ImageComponent(this.htmlHelper);
            return new ImageBuilder(component, null);
        }

        public ImageToolTipBuilder ImageToolTip()
        {
            var component = new ImageToolTipComponent(this.htmlHelper);
            return new ImageToolTipBuilder(component, null);
        }

        public LabelBuilder Label()
        {
            var component = new LabelComponent(this.htmlHelper);
            return new LabelBuilder(component, null);
        }

        public ListBoxBuilder ListBox()
        {
            var component = new ListBoxComponent(this.htmlHelper);
            return new ListBoxBuilder(component, null);
        }

        public PopOverBuilder PopOver()
        {
            var component = new PopOverComponent(this.htmlHelper);
            return new PopOverBuilder(component, null);
        }

        public PopupImageBuilder Popup()
        {
            var component = new PopupImageComponent(this.htmlHelper);
            return new PopupImageBuilder(component, null);
        }

        public ProgressBarBuilder ProgressBar()
        {
            var component = new ProgressBarComponent(this.htmlHelper);
            return new ProgressBarBuilder(component, null);
        }

        public RadioButtonBuilder RadioButton()
        {
            var component = new RadioButtonComponent(this.htmlHelper);
            return new RadioButtonBuilder(component, null);
        }

        public RadioButtonListBuilder RadioButtonList()
        {
            var component = new RadioButtonListComponent(this.htmlHelper);
            return new RadioButtonListBuilder(component, null);
        }

        public SpanLabelBuilder SpanLabel()
        {
            var component = new SpanLabelComponent(this.htmlHelper);
            return new SpanLabelBuilder(component, null);
        }

        public TextAreaBuilder TextArea()
        {
            var component = new TextAreaComponent(this.htmlHelper);
            return new TextAreaBuilder(component, null);
        }

        public TextBoxBuilder TextBox()
        {
            var component = new TextBoxComponent(this.htmlHelper);
            return new TextBoxBuilder(component, null);
        }

        public TreeGridBuilder TreeGrid()
        {
            var component = new TreeGridComponent(this.htmlHelper, null);
            return new TreeGridBuilder(component, null);
        }

        public VerticalMenuBuilder VerticalMenu()
        {
            var component = new VerticalMenuComponent(this.htmlHelper);
            return new VerticalMenuBuilder(component, null);
        }

        public WeekYearBuilder WeekYear()
        {
            var component = new WeekYearComponent(this.htmlHelper);
            return new WeekYearBuilder(component, null);
        }

        public ScriptRendererBuilder ScriptRenderer()
        {
            return this.scriptRendererBuilder;
        }

        public StyleRendererBuilder StyleRenderer()
        {
            return this.styleRendererBuilder;
        }
    }
}
