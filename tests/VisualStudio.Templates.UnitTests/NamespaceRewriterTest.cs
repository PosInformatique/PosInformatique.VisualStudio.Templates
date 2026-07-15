//-----------------------------------------------------------------------
// <copyright file="NamespaceRewriterTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public class NamespaceRewriterTest
    {
        private const string Company = "P.O.S Informatique";
        private const string RootName = "TheClass";
        private const string RootNamespace = "PosInformatique.VisualStudio.Templates.NamespaceTest";
        private const string SafeItemName = "TheClass";
        private const string SafeItemRootName = "TheClass";
        private const string NamespaceUnderTest = "PosInformatique.VisualStudio.Templates.NamespaceUnderTest";
        private const string ClassNameUnderTest = "TheClassUnderTest";

        public static IEnumerable<object[]> GetTemplateCsFiles()
        {
            var repositoryRoot = GetRepositoryRootPath();
            var templatesDirectory = Path.Combine(repositoryRoot, "src", "VisualStudio.Templates.Files");
            var expectedDirectory = Path.Combine(repositoryRoot, "tests", "VisualStudio.Templates.UnitTests");

            return new[]
            {
                new object[]
                {
                    Path.Combine(templatesDirectory, "Class.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_Class.cs_FileScoped.expected"),
                },
                new object[]
                {
                    Path.Combine(templatesDirectory, "Exception.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_Exception.cs_FileScoped.expected"),
                },
                new object[]
                {
                    Path.Combine(templatesDirectory, "ExceptionUnitTest.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_ExceptionUnitTest.cs_FileScoped.expected"),
                },
                new object[]
                {
                    Path.Combine(templatesDirectory, "Interface.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_Interface.cs_FileScoped.expected"),
                },
                new object[]
                {
                    Path.Combine(templatesDirectory, "RazorComponent.razor.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_RazorComponent.razor.cs_FileScoped.expected"),
                },
                new object[]
                {
                    Path.Combine(templatesDirectory, "XUnitTest.cs"),
                    Path.Combine(expectedDirectory, "NamespaceRewriterTest_XUnitTest.cs_FileScoped.expected"),
                },
            };
        }

        [Theory]
        [MemberData(nameof(GetTemplateCsFiles))]
        public void ConvertBlockToFileScoped_WithTemplateContent_MatchesExpected(string templateFilePath, string expectedFilePath)
        {
            var content = File.ReadAllText(templateFilePath);
            var expected = File.ReadAllText(expectedFilePath);

            content = ReplaceTemplateVariables(content);

            var converted = ConvertBlockToFileScoped(content);

            Assert.Equal(expected, converted);
        }

        private static string ReplaceTemplateVariables(string content)
        {
            return content
                .Replace("$company$", Company)
                .Replace("$rootname$", RootName)
                .Replace("$rootnamespace$", RootNamespace)
                .Replace("$safeitemname$", SafeItemName)
                .Replace("$safeitemrootname$", SafeItemRootName)
                .Replace("$namespaceundertest$", NamespaceUnderTest)
                .Replace("$classnameundertest$", ClassNameUnderTest);
        }

        private static string ConvertBlockToFileScoped(string content)
        {
            var namespaceRewriterType = Assembly.Load("PosInformatique.VisualStudio.Templates")
                .GetType("PosInformatique.VisualStudio.Templates.NamespaceRewriter", throwOnError: true);

            var method = namespaceRewriterType.GetMethod("ConvertBlockToFileScoped", BindingFlags.Static | BindingFlags.Public);

            return (string)method.Invoke(null, new object[] { content });
        }

        private static string GetRepositoryRootPath()
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            while (directory != null)
            {
                var solutionFile = Path.Combine(directory.FullName, "PosInformatique.VisualStudio.Templates.sln");

                if (File.Exists(solutionFile))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            throw new DirectoryNotFoundException("Unable to locate PosInformatique.VisualStudio.Templates.sln from test base directory.");
        }
    }
}
