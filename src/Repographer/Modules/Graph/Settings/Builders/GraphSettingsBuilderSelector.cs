#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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