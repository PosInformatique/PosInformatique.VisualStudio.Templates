//-----------------------------------------------------------------------
// <copyright file="StyleCopConfigurationSettings.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.Text.Json.Serialization;

    public class StyleCopConfigurationSettings
    {
        public StyleCopConfigurationSettings(StyleCopConfigurationDocumentationRules documentationRules)
        {
            this.DocumentationRules = documentationRules;
        }

        [JsonPropertyName("documentationRules")]
        public StyleCopConfigurationDocumentationRules DocumentationRules { get; }
    }
}
