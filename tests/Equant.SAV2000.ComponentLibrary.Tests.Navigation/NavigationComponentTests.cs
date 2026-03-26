namespace Equant.SAV2000.ComponentLibrary.Tests.Navigation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    public class NavigationComponentTests
    {
        private static IDocument ParseHtml(string html)
        {
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            return parser.ParseDocument(html);
        }

        private static string RenderHtmlBuilder<T>(T htmlBuilder) where T : class
        {
            using (var sw = new StringWriter())
            {
                var buildMethod = typeof(T).GetMethod("Build");
                buildMethod.Invoke(htmlBuilder, new object[] { sw });
                return sw.ToString();
            }
        }

        #region BreadCrumbs Tests (US-NAV-001)

        /// <summary>
        /// TC-NAV-001-U01: BreadCrumbsComponent renders trail from items
        /// </summary>
        [Fact]
        public void TC_NAV_001_U01_BreadCrumbsComponent_RendersTrailFromItems()
        {
            // Arrange
            var component = new BreadCrumbsComponent
            {
                Id = "breadcrumb1",
                Items = new List<BreadCrumbsItem>
                {
                    new BreadCrumbsItem { Text = "Home", Url = "/home" },
                    new BreadCrumbsItem { Text = "Products", Url = "/products" },
                    new BreadCrumbsItem { Text = "Detail", IsCurrentPage = true }
                }
            };

            var htmlBuilder = new BreadCrumbsHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var nav = doc.QuerySelector("nav");
            nav.Should().NotBeNull("BreadCrumbs should render a nav element");
            nav.GetAttribute("id").Should().Be("breadcrumb1");
            nav.GetAttribute("aria-label").Should().Be("breadcrumb");

            var items = doc.QuerySelectorAll("li.breadcrumb-item");
            items.Length.Should().Be(3, "Should render 3 breadcrumb items");

            // First item should have a link
            var firstLink = items[0].QuerySelector("a");
            firstLink.Should().NotBeNull("First item should be a link");
            firstLink.GetAttribute("href").Should().Be("/home");
            firstLink.TextContent.Should().Be("Home");

            // Second item should have a link
            var secondLink = items[1].QuerySelector("a");
            secondLink.Should().NotBeNull("Second item should be a link");
            secondLink.GetAttribute("href").Should().Be("/products");
            secondLink.TextContent.Should().Be("Products");
        }

        /// <summary>
        /// TC-NAV-001-U02: BreadCrumbs last item is not a link (current page)
        /// </summary>
        [Fact]
        public void TC_NAV_001_U02_BreadCrumbs_LastItemIsNotALink()
        {
            // Arrange
            var component = new BreadCrumbsComponent
            {
                Id = "breadcrumb2",
                Items = new List<BreadCrumbsItem>
                {
                    new BreadCrumbsItem { Text = "Home", Url = "/home" },
                    new BreadCrumbsItem { Text = "Current Page" }
                }
            };

            var htmlBuilder = new BreadCrumbsHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var items = doc.QuerySelectorAll("li.breadcrumb-item");
            items.Length.Should().Be(2);

            // Last item should be active and NOT contain a link
            var lastItem = items.Last();
            lastItem.ClassList.Should().Contain("active", "Last item should have 'active' class");
            lastItem.GetAttribute("aria-current").Should().Be("page", "Last item should have aria-current='page'");

            var lastLink = lastItem.QuerySelector("a");
            lastLink.Should().BeNull("Last item (current page) should NOT be rendered as a link");
            lastItem.TextContent.Should().Be("Current Page");
        }

        #endregion

        #region HorizontalMenu Tests (US-NAV-002)

        /// <summary>
        /// TC-NAV-002-U01: HorizontalMenuComponent renders menu items as links
        /// </summary>
        [Fact]
        public void TC_NAV_002_U01_HorizontalMenuComponent_RendersMenuItemsAsLinks()
        {
            // Arrange
            var component = new HorizontalMenuComponent
            {
                Id = "hmenu1",
                MenuItems = new List<HorizontalMenuItemInfo>
                {
                    new HorizontalMenuItemInfo { Text = "Dashboard", Url = "/dashboard" },
                    new HorizontalMenuItemInfo { Text = "Reports", Url = "/reports" },
                    new HorizontalMenuItemInfo { Text = "Settings", Url = "/settings" }
                }
            };

            var htmlBuilder = new HorizontalMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var nav = doc.QuerySelector("nav");
            nav.Should().NotBeNull("HorizontalMenu should render a nav element");
            nav.GetAttribute("id").Should().Be("hmenu1");

            var links = doc.QuerySelectorAll("a.nav-link");
            links.Length.Should().Be(3, "Should render 3 menu items as links");

            links[0].TextContent.Should().Be("Dashboard");
            links[0].GetAttribute("href").Should().Be("/dashboard");

            links[1].TextContent.Should().Be("Reports");
            links[1].GetAttribute("href").Should().Be("/reports");

            links[2].TextContent.Should().Be("Settings");
            links[2].GetAttribute("href").Should().Be("/settings");
        }

        /// <summary>
        /// TC-NAV-002-U02: HorizontalMenu active item highlighting
        /// </summary>
        [Fact]
        public void TC_NAV_002_U02_HorizontalMenu_ActiveItemHighlighting()
        {
            // Arrange
            var component = new HorizontalMenuComponent
            {
                Id = "hmenu2",
                ActiveItem = "Reports",
                MenuItems = new List<HorizontalMenuItemInfo>
                {
                    new HorizontalMenuItemInfo { Text = "Dashboard", Url = "/dashboard" },
                    new HorizontalMenuItemInfo { Text = "Reports", Url = "/reports" },
                    new HorizontalMenuItemInfo { Text = "Settings", Url = "/settings" }
                }
            };

            var htmlBuilder = new HorizontalMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var navItems = doc.QuerySelectorAll("li.nav-item");
            navItems.Length.Should().Be(3);

            // First item should NOT be active
            navItems[0].ClassList.Should().NotContain("active", "Dashboard should not be active");

            // Second item (Reports) should be active
            navItems[1].ClassList.Should().Contain("active", "Reports should be active");

            // Third item should NOT be active
            navItems[2].ClassList.Should().NotContain("active", "Settings should not be active");
        }

        #endregion

        #region VerticalMenu Tests (US-NAV-003)

        /// <summary>
        /// TC-NAV-003-U01: VerticalMenuComponent renders sidebar menu with items
        /// </summary>
        [Fact]
        public void TC_NAV_003_U01_VerticalMenuComponent_RendersSidebarMenuWithItems()
        {
            // Arrange
            var component = new VerticalMenuComponent
            {
                Id = "vmenu1",
                Name = "vertMenu",
                CopyRightText = "Copyright 2024",
                MenuItems = new List<MenuItem>
                {
                    new MenuItem { MenuName = "Item 1", MenuType = EMenuCtrlType.Redirect, ActionUrl = "/item1" },
                    new MenuItem { MenuName = "Item 2", MenuType = EMenuCtrlType.Redirect, ActionUrl = "/item2" }
                }
            };

            var htmlBuilder = new VerticalMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var outerDiv = doc.QuerySelector("div.navbar");
            outerDiv.Should().NotBeNull("VerticalMenu should render an outer div with 'navbar' class");
            outerDiv.GetAttribute("id").Should().Be("vmenu1");

            var ul = doc.QuerySelector("ul.nav.nav-vertical");
            ul.Should().NotBeNull("Should have a UL with 'nav nav-vertical' classes");

            var menuLinks = doc.QuerySelectorAll("ul.nav.nav-vertical li a");
            menuLinks.Length.Should().BeGreaterOrEqualTo(2, "Should render at least 2 menu item links");

            // Verify menu items are rendered with correct text
            var linkTexts = menuLinks.Select(l => l.TextContent.Trim()).ToList();
            linkTexts.Should().Contain("Item 1");
            linkTexts.Should().Contain("Item 2");
        }

        /// <summary>
        /// TC-NAV-003-U02: VerticalMenu copyright image rendering
        /// </summary>
        [Fact]
        public void TC_NAV_003_U02_VerticalMenu_CopyrightImageRendering()
        {
            // Arrange
            var component = new VerticalMenuComponent
            {
                Id = "vmenu2",
                Name = "vertMenu2",
                CopyRightText = "Copyright 2024 OBS"
            };
            component.CopyRightImage.Src = "/images/logo.png";
            component.CopyRightImage.Alt = "Company Logo";

            var htmlBuilder = new VerticalMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var copyrightDiv = doc.QuerySelector("div.copyright");
            copyrightDiv.Should().NotBeNull("Should render a copyright div");

            var copyrightText = doc.QuerySelector("div.copyright p");
            copyrightText.Should().NotBeNull("Should render copyright text in a paragraph");
            copyrightText.TextContent.Should().Be("Copyright 2024 OBS");

            var img = doc.QuerySelector("div.copyright img");
            img.Should().NotBeNull("Should render the copyright image");
            img.GetAttribute("src").Should().Be("/images/logo.png");
            img.GetAttribute("alt").Should().Be("Company Logo");
        }

        #endregion

        #region HorizontalTab Tests (US-NAV-004)

        /// <summary>
        /// TC-NAV-004-U01: HorizontalTabComponent renders tab headers and panels
        /// </summary>
        [Fact]
        public void TC_NAV_004_U01_HorizontalTabComponent_RendersTabHeadersAndPanels()
        {
            // Arrange
            var component = new HorizontalTabComponent
            {
                Id = "tabs1",
                Tabs = new List<TabItem>
                {
                    new TabItem { Text = "Tab 1", ContentId = "panel1" },
                    new TabItem { Text = "Tab 2", ContentId = "panel2" },
                    new TabItem { Text = "Tab 3", ContentId = "panel3" }
                }
            };

            var htmlBuilder = new HorizontalTabHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var container = doc.QuerySelector("div.horizontal-tab");
            container.Should().NotBeNull("HorizontalTab should render a container div");
            container.GetAttribute("id").Should().Be("tabs1");

            // Tab headers
            var tabLinks = doc.QuerySelectorAll("ul.nav-tabs a.nav-link");
            tabLinks.Length.Should().Be(3, "Should render 3 tab headers");
            tabLinks[0].TextContent.Should().Be("Tab 1");
            tabLinks[1].TextContent.Should().Be("Tab 2");
            tabLinks[2].TextContent.Should().Be("Tab 3");

            // Tab panels
            var panels = doc.QuerySelectorAll("div.tab-pane");
            panels.Length.Should().Be(3, "Should render 3 tab panels");
            panels[0].GetAttribute("id").Should().Be("panel1");
            panels[1].GetAttribute("id").Should().Be("panel2");
            panels[2].GetAttribute("id").Should().Be("panel3");
        }

        /// <summary>
        /// TC-NAV-004-U02: HorizontalTab active tab selection
        /// </summary>
        [Fact]
        public void TC_NAV_004_U02_HorizontalTab_ActiveTabSelection()
        {
            // Arrange
            var component = new HorizontalTabComponent
            {
                Id = "tabs2",
                ActiveTab = "Tab 2",
                Tabs = new List<TabItem>
                {
                    new TabItem { Text = "Tab 1", ContentId = "panel1" },
                    new TabItem { Text = "Tab 2", ContentId = "panel2" },
                    new TabItem { Text = "Tab 3", ContentId = "panel3" }
                }
            };

            var htmlBuilder = new HorizontalTabHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var tabLinks = doc.QuerySelectorAll("a.nav-link");
            tabLinks.Length.Should().Be(3);

            // Tab 1 should NOT be active
            tabLinks[0].ClassList.Should().NotContain("active", "Tab 1 should not be active");

            // Tab 2 should be active
            tabLinks[1].ClassList.Should().Contain("active", "Tab 2 should be active");

            // Tab 3 should NOT be active
            tabLinks[2].ClassList.Should().NotContain("active", "Tab 3 should not be active");

            // Corresponding panel should also be active
            var panels = doc.QuerySelectorAll("div.tab-pane");
            panels[0].ClassList.Should().NotContain("active");
            panels[1].ClassList.Should().Contain("active", "Panel 2 should be active");
            panels[2].ClassList.Should().NotContain("active");
        }

        #endregion

        #region DropDownMenu Tests (US-NAV-005)

        /// <summary>
        /// TC-NAV-005-U01: DropDownMenuComponent renders menu with child items
        /// </summary>
        [Fact]
        public void TC_NAV_005_U01_DropDownMenuComponent_RendersMenuWithChildItems()
        {
            // Arrange
            var component = new DropDownMenuComponent
            {
                Id = "ddmenu1",
                MenuItems = new List<DropdownMenu>
                {
                    new DropdownMenu
                    {
                        Text = "File",
                        Url = "#",
                        Children = new List<ChildMenu>
                        {
                            new ChildMenu { Text = "New", Url = "/file/new" },
                            new ChildMenu { Text = "Open", Url = "/file/open" },
                            new ChildMenu { Text = "Save", Url = "/file/save" }
                        }
                    },
                    new DropdownMenu
                    {
                        Text = "Edit",
                        Url = "/edit"
                    }
                }
            };

            var htmlBuilder = new DropDownMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var nav = doc.QuerySelector("nav");
            nav.Should().NotBeNull("DropDownMenu should render a nav element");
            nav.GetAttribute("id").Should().Be("ddmenu1");

            var topLevelItems = doc.QuerySelectorAll("ul.dropdown-nav > li.nav-item");
            topLevelItems.Length.Should().Be(2, "Should render 2 top-level menu items");

            // First item (File) should have dropdown class and children
            topLevelItems[0].ClassList.Should().Contain("dropdown", "File should have dropdown class");
            var fileLink = topLevelItems[0].QuerySelector("a.nav-link");
            fileLink.TextContent.Should().Be("File");
            fileLink.ClassList.Should().Contain("dropdown-toggle");

            var childItems = topLevelItems[0].QuerySelectorAll("ul.dropdown-menu > li a.dropdown-item");
            childItems.Length.Should().Be(3, "File should have 3 child menu items");
            childItems[0].TextContent.Should().Be("New");
            childItems[1].TextContent.Should().Be("Open");
            childItems[2].TextContent.Should().Be("Save");

            // Second item (Edit) should not have dropdown
            topLevelItems[1].ClassList.Should().NotContain("dropdown", "Edit has no children");
        }

        /// <summary>
        /// TC-NAV-005-U02: DropDownMenu nested child menu rendering
        /// </summary>
        [Fact]
        public void TC_NAV_005_U02_DropDownMenu_NestedChildMenuRendering()
        {
            // Arrange
            var component = new DropDownMenuComponent
            {
                Id = "ddmenu2",
                MenuItems = new List<DropdownMenu>
                {
                    new DropdownMenu
                    {
                        Text = "View",
                        Url = "#",
                        Children = new List<ChildMenu>
                        {
                            new ChildMenu
                            {
                                Text = "Zoom",
                                Url = "#",
                                Children = new List<ChildMenu>
                                {
                                    new ChildMenu { Text = "Zoom In", Url = "/view/zoom-in" },
                                    new ChildMenu { Text = "Zoom Out", Url = "/view/zoom-out" }
                                }
                            },
                            new ChildMenu { Text = "Full Screen", Url = "/view/fullscreen" }
                        }
                    }
                }
            };

            var htmlBuilder = new DropDownMenuHtmlBuilder(component);

            // Act
            var html = RenderHtmlBuilder(htmlBuilder);
            var doc = ParseHtml(html);

            // Assert
            var topLevelItem = doc.QuerySelector("ul.dropdown-nav > li.nav-item.dropdown");
            topLevelItem.Should().NotBeNull("View should be a dropdown item");

            // First level children
            var firstLevelChildren = topLevelItem.QuerySelectorAll(":scope > ul.dropdown-menu > li");
            firstLevelChildren.Length.Should().Be(2, "View should have 2 direct children");

            // Zoom should have nested submenu
            var zoomItem = firstLevelChildren[0];
            zoomItem.ClassList.Should().Contain("dropdown-submenu", "Zoom should have dropdown-submenu class");

            var nestedMenu = zoomItem.QuerySelector("ul.dropdown-menu");
            nestedMenu.Should().NotBeNull("Zoom should have a nested dropdown-menu");

            var nestedItems = nestedMenu.QuerySelectorAll("li a.dropdown-item");
            nestedItems.Length.Should().Be(2, "Zoom should have 2 nested items");
            nestedItems[0].TextContent.Should().Be("Zoom In");
            nestedItems[0].GetAttribute("href").Should().Be("/view/zoom-in");
            nestedItems[1].TextContent.Should().Be("Zoom Out");
            nestedItems[1].GetAttribute("href").Should().Be("/view/zoom-out");

            // Full Screen should not have nested submenu
            var fullScreenItem = firstLevelChildren[1];
            fullScreenItem.ClassList.Should().NotContain("dropdown-submenu");
        }

        #endregion
    }
}
