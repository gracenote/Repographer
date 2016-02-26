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

namespace Repographer.Components.IO
{
    public class DirectoryWalker
    {
        private readonly string _directory;
        private readonly IEnumerable<string> _excludes;
        private readonly List<FileTypeRule> _fileTypeRules;
        private readonly List<DirectoryRule> _directoryRules;
        private Action<string> _traceAction = str => { };

        public DirectoryWalker(
            string directory,
            IEnumerable<string> excludes,
            IEnumerable<FileTypeRule> fileTypeRules,
            IEnumerable<DirectoryRule> directoryRules)
        {
            _directory = directory;
            _excludes = excludes;
            _fileTypeRules = fileTypeRules.ToList();
            _directoryRules = directoryRules.ToList();
        }

        public void Execute()
        {
            _traceAction("DIRECTORY: " + _directory);

            _fileTypeRules.ForEach(rule =>
            {
                Directory
                    .GetFiles(_directory, rule.Extension)
                    .ToList()
                    .ForEach(file =>
                    {
                        _traceAction("RUNNING: " + file);
                        rule.Execute(file);
                    });
            });

            RunRulesOn(_directory);
        }

        public void TraceAction(Action<string> traceAction)
        {
            _traceAction = traceAction;
        }

        void RunRulesOn(string directory)
        {
            foreach (var subDirectory in Directory.GetDirectories(directory))
            {
                if (_excludes.Any(ex => subDirectory.Contains(ex)))
                {
                    _traceAction("EXCLUDING: " + subDirectory);
                    continue;
                }

                _traceAction("DIRECTORY: " + subDirectory);

                _fileTypeRules.ForEach(rule =>
                {
                    Directory
                        .GetFiles(subDirectory, rule.Extension)
                        .ToList()
                        .ForEach(file =>
                        {
                            _traceAction("RUNNING: " + file);
                            rule.Execute(file);
                        });
                });

                _directoryRules.ForEach(rule =>
                {
                    Directory
                        .GetDirectories(subDirectory)
                        .Select(d => new
                        {
                            new DirectoryInfo(d).Name,
                            FolderPath = d
                        })
                        .Where(d => d.Name == rule.Name)
                        .ToList()
                        .ForEach(subDir =>
                        {
                            _traceAction("RUNNING: " + subDir.FolderPath);
                            rule.Execute(subDir.FolderPath);
                        });
                });

                RunRulesOn(subDirectory);
            }
        }
    }
}
