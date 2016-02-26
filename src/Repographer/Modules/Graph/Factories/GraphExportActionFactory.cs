using Repographer.Components;
using Repographer.DataAccess.Graphing;
using Repographer.Modules.Graph.Settings;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Graph.Factories
{
    public class GraphExportActionFactory : IRepoActionFactory<GraphExportSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IGraphDatabase _graphDatabase;

        public GraphExportActionFactory(ITraceWriter traceWriter, IGraphDatabase graphDatabase)
        {
            _traceWriter = traceWriter;
            _graphDatabase = graphDatabase;
        }

        public IRepoAction Create(GraphExportSettings settings)
        {
            return new GraphExportAction(_traceWriter, _graphDatabase, settings.Creator, settings.Description);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof (GraphExportSettings);
        }
    }
}