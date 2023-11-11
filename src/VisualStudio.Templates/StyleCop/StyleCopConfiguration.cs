//-----------------------------------------------------------------------
// <copyright file="StyleCopConfiguration.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class StyleCopConfiguration
    {
        public StyleCopConfiguration(StyleCopConfigurationSettings settings)
        {
            this.Settings = settings;
        }

        [JsonPropertyName("settings")]
        public StyleCopConfigurationSettings Settings { get; }

        public static StyleCopConfiguration LoadFrom(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            try
            {
                return JsonSerializer.Deserialize<StyleCopConfiguration>(stream, options);
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}
