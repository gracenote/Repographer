using System.Collections.Generic;
using System.Linq;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Modules.Main.Settings.Builders.Selectors;

namespace Repographer.Modules.Graph.Settings.Builders
{
    public class GraphSettingsBuilderSelector : IBuilderSelector<GraphSettings>
    {
        private readonly IEnumerable<ISettingsBuilder<GraphSettings>> _builders;

        public GraphSettingsBuilderSelector(IEnumerable<ISettingsBuilder<GraphSettings>> builders)
        {
            _builders = builders;
        }

        public ISettingsBuilder GetBuilder(GraphSettings repoActionSettings)
        {
            return _builders.FirstOrDefault(b => b.IsBuilderFor(repoActionSettings));
        }
    }
}