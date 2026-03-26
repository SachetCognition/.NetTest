using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
using Equant.SAV2000.ComponentLibrary.Common.Helper;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

namespace Equant.SAV2000.ComponentLibrary.Tests.DataDisplay
{
    /// <summary>
    /// US-DD-002: DataTable Server-Side Tests
    /// </summary>
    public class DataTableServerSideTests
    {
        /// <summary>
        /// TC-DD-002-U01: DataTable ServiceUri POST configuration
        /// </summary>
        [Fact]
        public void TC_DD_002_U01_DataTable_ServiceUri_POSTConfiguration()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "serverTable";
            component.ServiceUri = "/api/data";
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();
            var jObj = JObject.Parse(json);

            // Assert
            var isServerSide = jObj.SelectToken("dataTableInit.bServerSide");
            isServerSide.Should().NotBeNull("bServerSide should be present");
            isServerSide.Value<bool>().Should().BeTrue("server-side should be enabled when ServiceUri is set");

            var ajaxSource = jObj.SelectToken("dataTableInit.sAjaxSource");
            ajaxSource.Should().NotBeNull("sAjaxSource should be present");
            ajaxSource.Value<string>().Should().Be("/api/data", "ajax source should be the ServiceUri");

            var serverMethod = jObj.SelectToken("dataTableInit.sServerMethod");
            serverMethod.Should().NotBeNull("sServerMethod should be present");
            serverMethod.Value<string>().Should().Be("POST", "server method should be POST");
        }

        /// <summary>
        /// TC-DD-002-U02: DataTable throws NotSupportedException when both ServiceUri and Data set
        /// </summary>
        [Fact]
        public void TC_DD_002_U02_DataTable_ThrowsNotSupportedException_BothServiceUriAndData()
        {
            // Arrange
            var dt = new DataTable("Test");
            dt.Columns.Add("Col0", typeof(string));
            dt.Rows.Add("value");

            var component = new DataTableComponent();
            component.Id = "conflictTable";
            component.ServiceUri = "/api/data";
            component.Data = dt;
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act & Assert
            Action act = () =>
            {
                using (var sw = new StringWriter())
                {
                    component.WriteHtml(sw);
                }
            };

            act.Should().Throw<NotSupportedException>()
                .WithMessage("*ServiceUri*Data*cannot be used at the same time*");
        }

        /// <summary>
        /// TC-DD-002-U03: DataTable criteria parameter encryption
        /// </summary>
        [Fact]
        public void TC_DD_002_U03_DataTable_CriteriaParameterEncryption()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "encryptTable";
            component.ServiceUri = "/api/data";
            component.IsEncryptionRequired = true;
            component.EncryptedParameters = new List<string> { "SecretParam" };
            component.Criteria = new List<CriteriaParameter>
            {
                new CriteriaParameter
                {
                    PropertyName = "SecretParam",
                    Value = "secret_value",
                    EvalType = CriteriaParameterEvaluationType.None
                },
                new CriteriaParameter
                {
                    PropertyName = "NormalParam",
                    Value = "normal_value",
                    EvalType = CriteriaParameterEvaluationType.None
                }
            };
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            var serializer = new DataTableOptionsSerializer(component);
            var json = serializer.Serialize();
            var jObj = JObject.Parse(json);

            // Assert
            var criteria = jObj.SelectToken("criteria") as JArray;
            criteria.Should().NotBeNull("criteria should be present");
            criteria.Count.Should().Be(2, "there should be 2 criteria parameters");

            // The encrypted parameter should have its value encrypted (Base64)
            var encryptedParam = criteria[0];
            var encryptedValue = encryptedParam["value"].Value<string>();
            encryptedValue.Should().Be(Crypt.Encrypt("secret_value"),
                "SecretParam value should be encrypted");

            // The non-encrypted parameter should keep its original value
            var normalParam = criteria[1];
            normalParam["value"].Value<string>().Should().Be("normal_value",
                "NormalParam value should not be encrypted");
        }

        /// <summary>
        /// TC-DD-002-I01: DataTable server-side init script with encrypted params
        /// </summary>
        [Fact]
        public void TC_DD_002_I01_DataTable_ServerSideInitScript_EncryptedParams()
        {
            // Arrange
            var component = new DataTableComponent();
            component.Id = "serverInitTable";
            component.ServiceUri = "/api/data";
            component.IsEncryptionRequired = true;
            component.EncryptedParameters = new List<string> { "EncParam" };
            component.Criteria = new List<CriteriaParameter>
            {
                new CriteriaParameter
                {
                    PropertyName = "EncParam",
                    Value = "test_value",
                    EvalType = CriteriaParameterEvaluationType.None
                }
            };
            component.Columns = new List<DataTableColumn>
            {
                new DataTableColumn { PropertyName = "Col0", HeaderText = "Column 0" }
            };

            // Act
            using (var sw = new StringWriter())
            {
                component.WriteInitScript(sw);
                var initScript = sw.ToString();

                // Assert
                initScript.Should().Contain("$('#serverInitTable').dataTableCore(");
                initScript.Should().Contain("bServerSide", "server-side flag should be in init script");
                initScript.Should().Contain("/api/data", "service URI should be in init script");
                initScript.Should().Contain("isEncryptionRequired", "encryption flag should be in init script");

                // Verify the encrypted value is Base64
                var expectedEncrypted = Crypt.Encrypt("test_value");
                initScript.Should().Contain(expectedEncrypted,
                    "encrypted parameter value should appear in init script");
            }
        }
    }
}
