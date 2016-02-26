#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Exceptions;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;

namespace Repographer.Modules.RemoveReference
{
    public class RemoveReferenceAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        private readonly string _directory;
        private readonly IEnumerable<string> _excludes;

        private readonly bool _isProject;
        private readonly string _itemName;
        private readonly string _itemPath;

        public RemoveReferenceAction(
            ITraceWriter traceWriter,
            IDirectoryWalkerFactory directoryWalkerFactory,
            string directory,
            IEnumerable<string> excludes,
            bool isProject,
            string itemName,
            string itemPath
            )
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;

            _directory = directory;
            _excludes = excludes;

            _isProject = isProject;
            _itemName = itemName;
            _itemPath = itemPath;
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
                            var project = new Project(filePath);

                            if (!TryRemoveReference(project, _isProject, _itemName, _itemPath))
                                return;

                            _traceWriter.Output(string.Format("Saving project: {0}", filePath));
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
    }
}
