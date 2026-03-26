namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System;
    using System.IO;
    using System.Linq;
    using Xunit;
    using FluentAssertions;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// US-INF-002: Script Resource Management tests
    /// </summary>
    public class ScriptResourceManagementTests
    {
        /// <summary>
        /// TC-INF-002-U01: JsResource priority ordering (200, 210, 220).
        /// Verifies that scripts are returned in ascending priority order.
        /// </summary>
        [Fact]
        public void TC_INF_002_U01_JsResource_Priority_Ordering()
        {
            // Arrange
            var manager = new ScriptResourceManager();

            var script220 = new JsResource("datatable-filter", "/js/datatable-filter.js", 220, typeof(object));
            var script200 = new JsResource("jquery", "/js/jquery.min.js", 200, typeof(object));
            var script210 = new JsResource("savbutton", "/js/savbutton.js", 210, typeof(object));

            // Register in non-priority order
            manager.Register(script220);
            manager.Register(script200);
            manager.Register(script210);

            // Act
            var orderedScripts = manager.GetOrderedScripts();

            // Assert - Scripts should be ordered by priority ascending
            orderedScripts.Should().HaveCount(3);
            orderedScripts[0].Name.Should().Be("jquery");
            orderedScripts[0].Priority.Should().Be(200);
            orderedScripts[1].Name.Should().Be("savbutton");
            orderedScripts[1].Priority.Should().Be(210);
            orderedScripts[2].Name.Should().Be("datatable-filter");
            orderedScripts[2].Priority.Should().Be(220);
        }

        /// <summary>
        /// TC-INF-002-U02: Conditional script loading (filter scripts only when needed).
        /// Verifies that scripts are only registered when the condition is true.
        /// </summary>
        [Fact]
        public void TC_INF_002_U02_Conditional_Script_Loading()
        {
            // Arrange
            var manager = new ScriptResourceManager();

            var baseScript = new JsResource("datatable-base", "/js/datatable-base.js", 200, typeof(object));
            var filterScript = new JsResource("datatable-filter", "/js/datatable-filter.js", 210, typeof(object));

            // Act - Register base script unconditionally
            manager.Register(baseScript);

            // Act - Register filter script conditionally (IsFilter = false)
            manager.RegisterConditional(filterScript, false);

            // Assert - Only base script should be registered when condition is false
            manager.Count.Should().Be(1);
            manager.IsRegistered("datatable-base").Should().BeTrue();
            manager.IsRegistered("datatable-filter").Should().BeFalse();

            // Act - Register filter script conditionally (IsFilter = true)
            manager.RegisterConditional(filterScript, true);

            // Assert - Both scripts should be registered when condition is true
            manager.Count.Should().Be(2);
            manager.IsRegistered("datatable-filter").Should().BeTrue();
        }

        /// <summary>
        /// TC-INF-002-U03: Script deduplication (no duplicate registrations).
        /// Verifies that registering the same script multiple times does not create duplicates.
        /// </summary>
        [Fact]
        public void TC_INF_002_U03_Script_Deduplication()
        {
            // Arrange
            var manager = new ScriptResourceManager();

            var jquery1 = new JsResource("jquery", "/js/jquery-1.0.js", 200, typeof(object));
            var jquery2 = new JsResource("jQuery", "/js/jquery-2.0.js", 200, typeof(object)); // Same name, different case
            var jquery3 = new JsResource("jquery", "/js/jquery-3.0.js", 210, typeof(object)); // Same name, different path

            // Act - Register same script multiple times
            manager.Register(jquery1);
            manager.Register(jquery2);  // Should be deduplicated (case-insensitive)
            manager.Register(jquery3);  // Should be deduplicated (same name)

            // Assert - Only one script should be registered
            manager.Count.Should().Be(1);

            var ordered = manager.GetOrderedScripts();
            ordered.Should().HaveCount(1);
            ordered[0].ResourcePath.Should().Be("/js/jquery-1.0.js"); // First one wins

            // Verify deduplication also works with RegisterRange
            manager.Clear();
            var scripts = new[]
            {
                new JsResource("lib-a", "/js/lib-a.js", 200, typeof(object)),
                new JsResource("lib-b", "/js/lib-b.js", 210, typeof(object)),
                new JsResource("lib-a", "/js/lib-a-v2.js", 220, typeof(object)), // Duplicate
            };
            manager.RegisterRange(scripts);

            manager.Count.Should().Be(2);
            manager.IsRegistered("lib-a").Should().BeTrue();
            manager.IsRegistered("lib-b").Should().BeTrue();
        }
    }
}
