using System;
using System.Collections.Generic;
using System.Linq;
using Repographer.Modules.Main.Factories;
using Repographer.Mono.Options;

namespace Repographer.Modules.Main.Settings
{
    public class RepoActionSettings : IRepoActionSettings
    {
        public OptionSet Set { get; private set; }

        public bool Help { get; private set; }
        public bool Graph { get; private set; }
        public bool PurgeFolder { get; private set; }
        public bool SwitchToNuget { get; private set; }
        public bool RemoveProject { get; private set; }
        public bool RemoveReference { get; private set; }
        public bool ChangeNuget { get; private set; }
        public bool ChangeFramework { get; private set; }

        public IEnumerable<bool> ActionSwitches { get { return new[] { Graph, PurgeFolder, SwitchToNuget, RemoveProject, RemoveReference, ChangeNuget, ChangeFramework }; } }

        public bool ActionSpecified { get { return ActionSwitches.Any(k => k); } }

        public RepoActionSettings(OptionSet set)
        {
            set.Add("?|h|help", "Shows help for the command", val => Help = val != null);
            set.Add("g|graph", "Dependency graph operations", val => Graph = val != null);
            set.Add("pf|purgefolder", "Purge folder instances from the repository", val => PurgeFolder = val != null);
            set.Add("s2n|switch2nuget", "Switch project or assembly reference to NuGet package reference", val => SwitchToNuget = val != null);
            set.Add("rmproj|removeproj", "Remove a project from all solutions", val => RemoveProject = val != null);
            set.Add("rmref|removeref", "Remove a reference from all projects", val => RemoveReference = val != null);
            set.Add("mvn|movenuget", "Update hint paths to new nuget packages location", val => ChangeNuget = val != null);
            set.Add("chnet|changenet", "Change target .NET framework for projects in the repository", val => ChangeFramework = val != null);

            Set = set;
        }

        public virtual IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<RepoActionSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }
    }
}
