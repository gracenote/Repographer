#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Exceptions;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.PackagingCore;
using NuGet.Versioning;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;

namespace Repographer.Modules.SwitchToNuget
{
    public class SwitchToNugetAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        private readonly string _directory;
        private readonly IEnumerable<string> _excludes;

        private readonly bool _isProject;
        private readonly string _itemName;
        private readonly string _itemPath;
        private readonly string _packageName;
        private readonly string _packagePath;
        private readonly string _packageVersion;

        public SwitchToNugetAction(
            ITraceWriter traceWriter,
            IDirectoryWalkerFactory directoryWalkerFactory,
            string directory,
            IEnumerable<string> excludes,
            bool isProject,
            string itemName,
            string itemPath,
            string packageName,
            string packagePath,
            string packageVersion
            )
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;

            _directory = directory;
            _excludes = excludes;

            _isProject = isProject;
            _itemName = itemName;
            _itemPath = itemPath;
            _packageName = packageName;
            _packagePath = packagePath;
            _packageVersion = packageVersion;
        }

        public void Execute()
        {
            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_directory);

                _excludes.ToList().ForEach(x.Exclude);

                x.FileRule(y =>
                {
                    y.Extension("csproj");

                    y.Execute(filePath =>
                    {
                        try
                        {
                            var filePathUri = new Uri(filePath);
                            var newHintPathUri = new Uri(_packagePath);

                            var newRelativeHintPathUri = filePathUri.MakeRelativeUri(newHintPathUri);

                            var project = new Project(filePath);

                            if (!TryRemoveReference(project, _isProject, _itemName, _itemPath))
                                return;

                            _traceWriter.Output(string.Format("Working on: {0}", filePath));

                            _traceWriter.Output(string.Format("Adding reference: {0}, {1}", _itemName, newRelativeHintPathUri));
                            AddAssemblyReference(project, _itemName, newRelativeHintPathUri);

                            _traceWriter.Output("Updating packages.config");
                            UpdatePackagesConfig(project, _packageName, _packageVersion);

                            AddPackagesConfigItem(project);

                            _traceWriter.Output("Saving...");
                            project.Save();
                        }
                        catch (InvalidProjectFileException)
                        {
                            _traceWriter.Output(string.Format("Skipping: {0}", filePath));
                        }
                    });
                });
            });

            walker.Execute();
        }

        private static bool TryRemoveReference(Project project, bool isProject, string itemName, string itemPath)
        {
            var oldReference = project.Items.FirstOrDefault(item =>
                (
                    isProject
                    && item.ItemType == "ProjectReference"
                    && item.Metadata.Any(md => md.Name == "Name" && md.UnevaluatedValue == itemName)
                )
                ||
                (
                    !isProject
                    && item.ItemType == "Reference"
                    && item.UnevaluatedInclude.Split(',')[0] == itemName
                    && (string.IsNullOrEmpty(itemPath) || item.Metadata.Any(md => md.Name == "HintPath" && md.UnevaluatedValue == itemPath))
                ));

            if (oldReference == null)
                return false;

            project.RemoveItem(oldReference);

            return true;
        }

        private static void AddAssemblyReference(Project project, string referenceName, Uri newRelativeHintPathUri)
        {
            project.AddItemFast(
                "Reference",
                referenceName,
                new[]
                {
                    new KeyValuePair<string, string>("HintPath", newRelativeHintPathUri.ToString())
                });
        }

        private static void AddPackagesConfigItem(Project project)
        {
            var packageItem = project.Items.FirstOrDefault(item =>
                item.ItemType == "None"
                && item.UnevaluatedInclude == "packages.config");

            if (packageItem != null)
                return;

            project.AddItemFast("None", "packages.config");
        }

        private void UpdatePackagesConfig(Project project, string packageName, string packageVersion)
        {
            var packagesFilePath = Path.Combine(project.DirectoryPath, "packages.config");

            IEnumerable<PackageReference> entries = new List<PackageReference>();

            if (File.Exists(packagesFilePath))
            {
                using (var stream = new FileStream(packagesFilePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    var reader = new PackagesConfigReader(stream, true);
                    entries = reader.GetPackages();

                    stream.Close();
                }
            }

            File.Delete(packagesFilePath);

            using (var stream = new FileStream(packagesFilePath, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new PackagesConfigWriter(stream))
                {
                    entries.ToList().ForEach(writer.WritePackageEntry);

                    try
                    {
                        writer.WritePackageEntry(packageName, NuGetVersion.Parse(packageVersion), NuGetFramework.AnyFramework);
                    }
                    catch (PackagingException)
                    {
                        _traceWriter.Output("packages.config already contains references");
                    }

                    writer.Close();
                }

                stream.Close();
            }
        }
    }
}
