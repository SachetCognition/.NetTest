namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    public class VerticalMenuBuilder : ComponentBuilderBase<VerticalMenuComponent, VerticalMenuBuilder>
    {
        private readonly ImageBuilder copyRightImgBuilder;

        public VerticalMenuBuilder Image(Action<ImageBuilder> setup)
        {
            if (setup != null)
            {
                setup(this.copyRightImgBuilder);
            }

            return this;
        }

        public VerticalMenuBuilder(VerticalMenuComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.copyRightImgBuilder = new ImageBuilder(this.Component.CopyRightImage, modelMetadata);
        }

        public VerticalMenuBuilder(VerticalMenuComponent component)
            : base(component)
        {
            this.copyRightImgBuilder = new ImageBuilder(this.Component.CopyRightImage);
        }

        public VerticalMenuBuilder DataBind(IEnumerable<MenuItem> dataSource)
        {
            if (dataSource != null)
            {
                this.Component.MenuItems.Clear();
                foreach (var item in dataSource)
                {
                    this.Component.MenuItems.Add(item);
                }
            }

            return this;
        }

        public VerticalMenuBuilder CopyRightText(string copyRightTxt)
        {
            this.Component.CopyRightText = copyRightTxt;
            return this;
        }

        public VerticalMenuBuilder SelectedMenu(string value)
        {
            this.Component.SelectedMenu = value;
            return this;
        }

        public VerticalMenuBuilder CauseValidation(bool value)
        {
            this.Component.CauseValidation = value;
            return this;
        }
    }
}
