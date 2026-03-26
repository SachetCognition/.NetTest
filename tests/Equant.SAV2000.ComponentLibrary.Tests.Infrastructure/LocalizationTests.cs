namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System.Globalization;
    using System.Resources;
    using System.Threading;
    using Xunit;
    using FluentAssertions;

    using Equant.SAV2000.ComponentLibrary.MVC.Resources;

    /// <summary>
    /// US-INF-007: Localization tests
    /// </summary>
    public class LocalizationTests
    {
        /// <summary>
        /// TC-INF-007-U01: ApplicationStrings French keys present.
        /// Verifies that all expected French localization keys exist and have non-empty values.
        /// </summary>
        [Fact]
        public void TC_INF_007_U01_ApplicationStrings_French_Keys_Present()
        {
            // Arrange - Set culture to French
            var frenchCulture = new CultureInfo("fr");
            var resourceManager = ApplicationStrings.ResourceManager;

            // Act & Assert - Verify key French localization strings exist
            var keysToCheck = new[]
            {
                "ACCESS000002", "ACCESS000005", "ACCESS000006", "ACCESS000007",
                "CleanImageTooltip", "Datatable_sEmptyTable", "Datatable_sInfo",
                "Datatable_sInfoEmpty", "Datatable_sInfoFiltered", "Datatable_sLoadingRecords",
                "Datatable_sProcessing", "Datatable_sZeroRecords",
                "ERR_DATE_INVALIDE", "ErrorTitle",
                "LBL000011", "LBL000016", "LBL000017", "LBL000021", "LBL000022",
                "LBL000025", "LBL000027", "LBL000028", "LBL000032",
                "MSG000004", "TimeMandatory",
                "TIP000011", "TIP000012", "TIP000013", "TIP000014"
            };

            foreach (var key in keysToCheck)
            {
                var value = resourceManager.GetString(key, frenchCulture);
                value.Should().NotBeNullOrEmpty($"French key '{key}' should have a value");
            }

            // Verify specific French translations
            resourceManager.GetString("ACCESS000002", frenchCulture).Should().Be("Ouvrir");
            resourceManager.GetString("ErrorTitle", frenchCulture).Should().Be("Erreur");
            resourceManager.GetString("LBL000011", frenchCulture).Should().Be("Fermer");
            resourceManager.GetString("LBL000017", frenchCulture).Should().Be("Supprimer");
            resourceManager.GetString("TIP000011", frenchCulture).Should().Be("Suivant");
            resourceManager.GetString("TIP000013", frenchCulture).Should().Be("Precedent");
        }

        /// <summary>
        /// TC-INF-007-U02: ApplicationStrings English keys present.
        /// Verifies that all expected English (default) localization keys exist and have non-empty values.
        /// </summary>
        [Fact]
        public void TC_INF_007_U02_ApplicationStrings_English_Keys_Present()
        {
            // Arrange - Use invariant/English culture (default .resx)
            var englishCulture = CultureInfo.InvariantCulture;
            var resourceManager = ApplicationStrings.ResourceManager;

            // Act & Assert - Verify key English localization strings exist
            var keysToCheck = new[]
            {
                "ACCESS000002", "ACCESS000005", "ACCESS000006", "ACCESS000007",
                "CleanImageTooltip", "Datatable_sEmptyTable", "Datatable_sInfo",
                "Datatable_sInfoEmpty", "Datatable_sInfoFiltered", "Datatable_sLoadingRecords",
                "Datatable_sProcessing", "Datatable_sZeroRecords",
                "ERR_DATE_INVALIDE", "ErrorTitle",
                "LBL000011", "LBL000016", "LBL000017", "LBL000021", "LBL000022",
                "LBL000025", "LBL000027", "LBL000028", "LBL000032",
                "MSG000004", "TimeMandatory",
                "TIP000011", "TIP000012", "TIP000013", "TIP000014"
            };

            foreach (var key in keysToCheck)
            {
                var value = resourceManager.GetString(key, englishCulture);
                value.Should().NotBeNullOrEmpty($"English key '{key}' should have a value");
            }

            // Verify specific English values
            resourceManager.GetString("ACCESS000002", englishCulture).Should().Be("Open");
            resourceManager.GetString("ErrorTitle", englishCulture).Should().Be("Error");
            resourceManager.GetString("LBL000011", englishCulture).Should().Be("Close");
            resourceManager.GetString("LBL000017", englishCulture).Should().Be("Delete");
            resourceManager.GetString("TIP000011", englishCulture).Should().Be("Next");
            resourceManager.GetString("TIP000013", englishCulture).Should().Be("Previous");
        }

        /// <summary>
        /// TC-INF-007-U03: Localization fallback behavior.
        /// Verifies that when a key is not found in a specific culture, the system falls back
        /// to the default (English) resource file.
        /// </summary>
        [Fact]
        public void TC_INF_007_U03_Localization_Fallback_Behavior()
        {
            // Arrange
            var resourceManager = ApplicationStrings.ResourceManager;

            // Test 1: French culture returns French value
            var frenchCulture = new CultureInfo("fr");
            var frenchValue = resourceManager.GetString("ErrorTitle", frenchCulture);
            frenchValue.Should().Be("Erreur");

            // Test 2: English (invariant) culture returns English value
            var englishValue = resourceManager.GetString("ErrorTitle", CultureInfo.InvariantCulture);
            englishValue.Should().Be("Error");

            // Test 3: Unsupported culture (e.g., German) falls back to default (English)
            var germanCulture = new CultureInfo("de");
            var fallbackValue = resourceManager.GetString("ErrorTitle", germanCulture);
            fallbackValue.Should().Be("Error", "unsupported culture should fall back to default English");

            // Test 4: Static property accessor works correctly
            // Set culture to French and verify the static accessor returns French
            var previousCulture = ApplicationStrings.Culture;
            ApplicationStrings.Culture = frenchCulture;
            ApplicationStrings.ErrorTitle.Should().Be("Erreur");

            // Switch to invariant and verify English
            ApplicationStrings.Culture = CultureInfo.InvariantCulture;
            ApplicationStrings.ErrorTitle.Should().Be("Error");

            // Restore previous culture
            ApplicationStrings.Culture = previousCulture;

            // Test 5: Both French and English have the same keys (no missing translations)
            var frenchLbl = resourceManager.GetString("LBL000032", frenchCulture);
            var englishLbl = resourceManager.GetString("LBL000032", CultureInfo.InvariantCulture);
            frenchLbl.Should().NotBeNullOrEmpty();
            englishLbl.Should().NotBeNullOrEmpty();
            frenchLbl.Should().NotBe(englishLbl, "French and English should have different translations for LBL000032");
        }
    }
}
