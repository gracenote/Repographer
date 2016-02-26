#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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