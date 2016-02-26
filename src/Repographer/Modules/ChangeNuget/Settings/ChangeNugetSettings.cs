using System;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Modules.ChangeNuget.Settings
{
    public class ChangeNugetSettings : IRepoActionSettings
    {
        public OptionSet Set { get; private set; }

        public string Directory { get; private set; }
        
        public string NewPath { get; private set; }

        public ChangeNugetSettings(OptionSet set)
        {
            set.Add("d=|dir=|directory=", "Absolute path to the repository", val => Directory = val);
            set.Add("p=|path=", "New path for the reference", val => NewPath = val);

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<ChangeNugetSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }
    }
}