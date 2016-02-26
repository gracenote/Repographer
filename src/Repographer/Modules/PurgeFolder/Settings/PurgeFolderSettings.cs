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
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Modules.PurgeFolder.Settings
{
    public class PurgeFolderSettings : IRepoActionSettings
    {
        private readonly List<string> _includes = new List<string>();
        public IEnumerable<string> Includes { get { return _includes; } }

        private readonly List<string> _excludes = new List<string>();
        public IEnumerable<string> Excludes { get { return _excludes; } }

        public string Directory { get; private set; }

        public OptionSet Set { get; private set; }

        public PurgeFolderSettings(OptionSet set)
        {
            set.Add("d=|dir=|directory=", "Absolute path to the repository", val => Directory = val);
            set.Add("i=|inc=|include=", "Folders to purge (recursive), comma-separated", val =>
            {
                val.Split(',').Select(v => v.TrimStart('\"').TrimEnd('\"')).ToList().ForEach(Include);
            });
            set.Add("x=|exclude=", "Subdirectories to exclude (recursive), comma-separated", val =>
            {
                val.Split(',').Select(v => v.TrimStart('\"').TrimEnd('\"')).ToList().ForEach(Exclude);
            });

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<PurgeFolderSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }

        private void Include(string include)
        {
            if (_includes.Contains(include))
                return;

            _includes.Add(include);
        }
        
        private void Exclude(string exclude)
        {
            if (_excludes.Contains(exclude))
                return;

            _excludes.Add(exclude);
        }
    }
}