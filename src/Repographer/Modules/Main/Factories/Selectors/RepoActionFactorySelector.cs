using System.Collections.Generic;
using System.Linq;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Main.Factories.Selectors
{
    public class RepoActionFactorySelector : IRepoActionFactorySelector
    {
        private readonly IEnumerable<IRepoActionFactory> _factories;

        public RepoActionFactorySelector(IEnumerable<IRepoActionFactory> factories)
        {
            _factories = factories;
        }

        public IRepoActionFactory GetFactory(IRepoActionSettings repoActionSettings)
        {
            return _factories.FirstOrDefault(f => f.IsFactoryFor(repoActionSettings));
        }
    }
}