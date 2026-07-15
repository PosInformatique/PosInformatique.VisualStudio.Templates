//-----------------------------------------------------------------------
// <copyright file="EditorConfig.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System;
    using global::EditorConfig.Core;

    internal sealed class EditorConfig
    {
        private readonly EditorConfigParser parser;

        public EditorConfig()
        {
            this.parser = new EditorConfigParser();
        }

        public bool? GetInsertFinalNewline(string targetFilePath)
        {
            try
            {
                // Parse the .editorconfig for this file using the official library
                var config = this.parser.Parse(targetFilePath);

                return config.InsertFinalNewline;
            }
            catch (Exception)
            {
                // If parsing fails, return null (fallback to default behavior)
                return null;
            }
        }

        public bool GetUseFileScopedNamespace(string targetFilePath)
        {
            try
            {
                var config = this.parser.Parse(targetFilePath);

                if (!config.Properties.TryGetValue("csharp_style_namespace_declarations", out var rawValue) ||
                    string.IsNullOrWhiteSpace(rawValue))
                {
                    return false;
                }

                var value = rawValue.Split(':')[0].Trim();

                if (value.Equals("file_scoped", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (value.Equals("block_scoped", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
