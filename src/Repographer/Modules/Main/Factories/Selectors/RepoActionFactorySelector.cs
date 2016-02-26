#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
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