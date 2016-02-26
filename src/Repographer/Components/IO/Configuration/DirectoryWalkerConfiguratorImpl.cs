#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repographer.Components.IO.Configuration
{
    public class DirectoryWalkerConfiguratorImpl : DirectoryWalkerConfigurator
    {
        private string _directory;
        private readonly List<FileTypeRuleConfiguratorImpl> _fileTypeRuleConfigurators = new List<FileTypeRuleConfiguratorImpl>();
        private readonly List<DirectoryRuleConfiguratorImpl> _directoryRuleConfigurators = new List<DirectoryRuleConfiguratorImpl>(); 
        private readonly List<string> _excludes = new List<string>();

        public void Directory(string directory)
        {
            _directory = directory;
        }

        public void Exclude(string exclude)
        {
            _excludes.Add(exclude);
        }

        public void FileRule(Action<FileTypeRuleConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var configurator = new FileTypeRuleConfiguratorImpl();

            configure(configurator);

            _fileTypeRuleConfigurators.Add(configurator);
        }

        public void DirectoryRule(Action<DirectoryRuleConfigurator> configure)
        {
            if (configure == null)
                throw new ArgumentNullException("configure");

            var configurator = new DirectoryRuleConfiguratorImpl();

            configure(configurator);

            _directoryRuleConfigurators.Add(configurator);
        }

        public DirectoryWalker BuildEngine()
        {
            var fileRules = _fileTypeRuleConfigurators.Select(c => c.BuildRule());
            var directoryRules = _directoryRuleConfigurators.Select(c => c.BuildRule());
            
            return new DirectoryWalker(_directory, _excludes, fileRules, directoryRules);
        }
    }
}