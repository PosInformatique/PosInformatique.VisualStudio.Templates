//-----------------------------------------------------------------------
// <copyright file="NamespaceRewriterTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates.UnitTests
{
    using System.IO;
    using System.Reflection;
    using Xunit;

    public class NamespaceRewriterTest
    {
        private const string Company = "P.O.S Informatique";
        private const string RootName = "TheClass";
        private const string RootNamespace = "PosInformatique.VisualStudio.Templates.NamespaceTest";
        private const string SafeItemName = "TheClass";
        private const string SafeItemRootName = "TheClass";
        private const string NamespaceUnderTest = "PosInformatique.VisualStudio.Templates.NamespaceUnderTest";
        private const string ClassNameUnderTest = "TheClassUnderTest";

        [Theory]
        [InlineData("Class.cs")]
        [InlineData("Exception.cs")]
        [InlineData("ExceptionUnitTest.cs")]
        [InlineData("Interface.cs")]
        [InlineData("RazorComponent.razor.cs")]
        [InlineData("XUnitTest.cs")]
        public async Task ConvertBlockToFileScoped_WithTemplateContent_MatchesExpected(string templateFileName)
        {
            var repositoryRoot = GetRepositoryRootPath();
            var templatePath = Path.Combine(repositoryRoot, "src", "VisualStudio.Templates.Files", templateFileName);

            var content = File.ReadAllText(templatePath);
            content = ReplaceTemplateVariables(content);

            var converted = ConvertBlockToFileScoped(content);

            await Verify(converted).UseFileName($"NamespaceRewriterTest_{templateFileName}_FileScoped");
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

            var method = namespaceRewriterType.GetMethod("ConvertBlockToFileScoped", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            return (string)method.Invoke(null, [content]);
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
