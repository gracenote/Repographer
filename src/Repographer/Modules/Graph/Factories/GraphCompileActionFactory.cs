#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.DataAccess.Graphing;
using Repographer.Modules.Graph.Settings;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Graph.Factories
{
    public class GraphCompileActionFactory : IRepoActionFactory<GraphCompileSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IGraphDatabase _graphDatabase;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public GraphCompileActionFactory(
            ITraceWriter traceWriter,
            IGraphDatabase graphDatabase,
            IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _graphDatabase = graphDatabase;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(GraphCompileSettings settings)
        {
            return new GraphCompileAction(_traceWriter, _graphDatabase, _directoryWalkerFactory, settings.Directory, settings.Excludes);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(GraphCompileSettings);
        }
    }
}