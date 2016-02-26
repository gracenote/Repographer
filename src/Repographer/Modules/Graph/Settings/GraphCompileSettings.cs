using System;
using System.Collections.Generic;
using System.Linq;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings
{
    public class GraphCompileSettings : IRepoActionSettings
    {
        private readonly List<string> _excludes = new List<string>();
        public IEnumerable<string> Excludes { get { return _excludes; } }

        public string Directory { get; private set; }

        public OptionSet Set { get; private set; }

        public GraphCompileSettings(OptionSet set)
        {
            set.Add("d=|dir=|directory=", "Absolute path to the repository", val => Directory = val);
            set.Add("x=|exclude=", "Subdirectories to exclude (recursive), comma-separated", val =>
            {
                val.Split(',').Select(v => v.TrimStart('\"').TrimEnd('\"')).ToList().ForEach(Exclude);
            });

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<GraphCompileSettings>;

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