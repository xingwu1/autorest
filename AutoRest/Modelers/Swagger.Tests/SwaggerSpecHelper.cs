﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using Microsoft.Rest.Generator;
using Microsoft.Rest.Generator.Utilities;
using Xunit;

namespace Microsoft.Rest.Modeler.Swagger.Tests
{
    public static class SwaggerSpecHelper
    {
        public static void RunTests<T>(string specFile, string resultFolder, string modeler = "Swagger",
            Settings settings = null)
        {
            if (settings == null)
            {
                settings = new Settings
                {
                    Input = specFile,
                    OutputDirectory = "X:\\Output",
                    Header = "MICROSOFT_MIT",
                    Modeler = modeler,
                    PayloadFlatteningThreshold = 1
                };

            }

            RunTests<T>(settings, resultFolder);
        }

        public static void RunTests<T>(Settings settings, string resultFolder)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (resultFolder == null)
            {
                throw new ArgumentNullException("settings");
            }

            settings.FileSystem = new MemoryFileSystem();
            settings.FileSystem.WriteFile("AutoRest.json", File.ReadAllText("AutoRest.json"));
            settings.FileSystem.CreateDirectory(Path.GetDirectoryName(settings.Input));
            settings.FileSystem.WriteFile(settings.Input, File.ReadAllText(settings.Input));
            var flavor =
                (CodeGenerator)typeof(T).GetConstructor(new[] { typeof(Settings) }).Invoke(new object[] { settings });
            settings.CodeGenerator = flavor.Name;

            var expectedWithSeparator = "Expected" + Path.DirectorySeparatorChar;
            var specFileName = resultFolder.StartsWith(expectedWithSeparator, StringComparison.Ordinal)
                ? resultFolder.Substring(expectedWithSeparator.Length)
                : resultFolder;
            settings.Namespace = string.IsNullOrEmpty(settings.Namespace)
                ? "Fixtures." + (flavor.Name.Contains("Azure") ? "Azure." : "") + specFileName.
                    Replace(".cs", "").Replace(".Cs", "").Replace(".java", "").
                    Replace(".js", "").Replace(".", "").
                    Replace(Path.DirectorySeparatorChar.ToString(), "").Replace("-", "")
                : settings.Namespace;

            AutoRest.Generate(settings);
            Assert.NotEmpty(((MemoryFileSystem)settings.FileSystem).VirtualStore);

            var actualFiles = settings.FileSystem.GetFiles("X:\\Output", "*.*", SearchOption.AllDirectories).OrderBy(f => f).ToArray();
            var expectedFiles = Directory.GetFiles(resultFolder, "*.*", SearchOption.AllDirectories).OrderBy(f => f).ToArray();
            Assert.Equal(expectedFiles.Length, actualFiles.Length);

            for (int i = 0; i < expectedFiles.Length; i++)
            {
                var actualFile = actualFiles[i];
                var expectedFile = expectedFiles[i];
                EnsureFilesMatch(File.ReadAllText(expectedFile), settings.FileSystem.ReadFileAsText(actualFile));
            }
        }

        private static void EnsureFilesMatch(string expectedFileContent, string actualFileContent)
        {
            char[] wsChars = { '\r', ' ' };
            string[] expectedLines = expectedFileContent.Split('\n').Select(p => p.TrimEnd(wsChars)).ToArray();
            string[] actualLines = actualFileContent.Split('\n').Select(p => p.TrimEnd(wsChars)).ToArray();

            Assert.Equal(expectedLines.Length, actualLines.Length);

            for (int i = 0; i < expectedLines.Length; i++)
            {
                Assert.Equal(expectedLines[i], actualLines[i]);
            }
        }
    }
}