#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings
{
    public class GraphSettings : IRepoActionSettings
    {
        public bool Compile { get; set; }
        public bool Export { get; set; }

        public OptionSet Set { get; private set; }

        public GraphSettings(OptionSet set)
        {
            set.Add("c|compile", "Compiles the dependency graph and stores in the database", val => Compile = val != null);
            set.Add("e|export", "Exports the dependency graph from the database and outputs into a repository.gexf file", val => Export = val != null);

            Set = set;
        }

        public IRepoAction CreateAction(IRepoActionFactory factory)
        {
            var repoActionFactory = factory as IRepoActionFactory<GraphSettings>;

            if (repoActionFactory == null)
                throw new InvalidOperationException("An incompatible factory was provided for these settings");

            return repoActionFactory.Create(this);
        }
    }
}