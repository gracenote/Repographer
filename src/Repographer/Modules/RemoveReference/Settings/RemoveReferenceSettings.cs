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

namespace Repographer.Modules.RemoveReference.Settings
{
    public class RemoveReferenceSettings : IRepoActionSettings
    {
        private readonly List<string> _excludes = new List<string>();
        public IEnumerable<string> Excludes { get { return _excludes; } }

        public string Directory { get; private set; }

        public bool Project { get; private set; }
        public bool Assembly { get; private set; }

        public string ItemName { get; private set; }
        public string ItemPath { get; private set; }

        public OptionSet Set { get; private set; }

        public RemoveReferenceSettings(OptionSet set)
        {
            set.Add("d=|dir=|directory=", "Absolute path to the repository", val => Directory = val);
            set.Add("x=|exclude=", "Subdirectories to exclude (recursive), comma-separated", val =>
            {
                val.Split(',').Select(v => v.TrimStart('\"').TrimEnd('\"')).ToList().ForEach(Exclude);
            });

            set.Add("p|project", "Reference to remove is a project", val => Project = val != null);
            set.Add("asm|assembly", "Reference to remove is an assembly", val => Assembly = val != null);
            set.Add("name=", "Reference item name being removed", val => ItemName = val);
            set.Add("path=", "Current path to the reference being switched (optional)", val => ItemPath = val);

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<RemoveReferenceSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }

        private void Exclude(string exclude)
        {
            if (_excludes.Contains(exclude))
                return;

            _excludes.Add(exclude);
        }
    }
}