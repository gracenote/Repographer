using System.Collections.Generic;
using System.Linq;

namespace Repographer.Modules.Main.Settings.Builders.Selectors
{
    public class RepoActionSettingsBuilderSelector : IBuilderSelector<RepoActionSettings>
    {
        private readonly IEnumerable<ISettingsBuilder<RepoActionSettings>> _builders;

        public RepoActionSettingsBuilderSelector(IEnumerable<ISettingsBuilder<RepoActionSettings>> builders)
        {
            _builders = builders;
        }

        public ISettingsBuilder GetBuilder(RepoActionSettings repoActionSettings)
        {
            return _builders.FirstOrDefault(b => b.IsBuilderFor(repoActionSettings));
        }
    }
}