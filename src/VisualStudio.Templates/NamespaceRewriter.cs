//-----------------------------------------------------------------------
// <copyright file="NamespaceRewriter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal static class NamespaceRewriter
    {
        private static readonly Regex NamespaceRegex = new Regex(
            @"^\s*namespace\s+(?<name>[^\r\n;{]+)\s*$",
            RegexOptions.Compiled,
            timeoutMilliseconds: 1000);

        public static string ConvertBlockToFileScoped(string content)
        {
            var lines = new List<string>(content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None));

            var namespaceLineIndex = -1;
            Match namespaceMatch = null;

            for (var i = 0; i < lines.Count; i++)
            {
                namespaceMatch = Regex.Match(lines[i], @"^\s*namespace\s+(?<name>[^\r\n;{]+)\s*$");

                if (namespaceMatch.Success)
                {
                    namespaceLineIndex = i;
                    break;
                }
            }

            if (namespaceLineIndex < 0)
            {
                return content;
            }

            var openBraceLineIndex = -1;

            for (var i = namespaceLineIndex + 1; i < lines.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                {
                    continue;
                }

                if (lines[i].Trim() != "{")
                {
                    return content;
                }

                openBraceLineIndex = i;
                break;
            }

            if (openBraceLineIndex < 0)
            {
                return content;
            }

            var closeBraceLineIndex = FindMatchingNamespaceClosingBrace(lines, openBraceLineIndex);

            if (closeBraceLineIndex < 0)
            {
                return content;
            }

            // Unindent only content inside the namespace block.
            for (var i = openBraceLineIndex + 1; i < closeBraceLineIndex; i++)
            {
                if (lines[i].StartsWith("    ", StringComparison.Ordinal))
                {
                    lines[i] = lines[i].Substring(4);
                }
                else if (lines[i].StartsWith("\t", StringComparison.Ordinal))
                {
                    lines[i] = lines[i].Substring(1);
                }
            }

            // Convert namespace declaration.
            var namespaceName = namespaceMatch.Groups["name"].Value.Trim();
            lines[namespaceLineIndex] = "namespace " + namespaceName + ";";

            // Remove namespace braces.
            lines.RemoveAt(closeBraceLineIndex);
            lines.RemoveAt(openBraceLineIndex);

            // Add a blank line between file-scoped namespace and first declaration.
            var nextLineIndex = namespaceLineIndex + 1;

            if (nextLineIndex < lines.Count && !string.IsNullOrWhiteSpace(lines[nextLineIndex]))
            {
                lines.Insert(nextLineIndex, string.Empty);
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static int FindMatchingNamespaceClosingBrace(IList<string> lines, int openBraceLineIndex)
        {
            var depth = 0;

            for (var lineIndex = openBraceLineIndex; lineIndex < lines.Count; lineIndex++)
            {
                var line = lines[lineIndex];

                for (var charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    var c = line[charIndex];

                    if (c == '{')
                    {
                        depth++;
                    }
                    else if (c == '}')
                    {
                        depth--;

                        if (depth == 0)
                        {
                            return lineIndex;
                        }
                    }
                }
            }

            return -1;
        }
    }
}
