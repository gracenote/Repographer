using System;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings
{
    public class GraphExportSettings : IRepoActionSettings
    {
        public string Creator { get; private set; }
        public string Description { get; private set; }

        public OptionSet Set { get; private set; }

        public GraphExportSettings(OptionSet set)
        {
            set.Add("cr=|creator=", "Creator of the repository", val => Creator = val);
            set.Add("desc=|description=", "Description of the repository", val => Description = val);

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<GraphExportSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }
    }
}