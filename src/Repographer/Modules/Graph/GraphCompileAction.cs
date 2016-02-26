using System.Collections.Generic;
using System.Linq;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Components.Parsing;
using Repographer.DataAccess.Graphing;
using Repographer.Modules.Main;

namespace Repographer.Modules.Graph
{
    public class GraphCompileAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IGraphDatabase _graphDatabase;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;
        private readonly string _directory;
        private readonly IEnumerable<string> _excludes;

        public GraphCompileAction(
            ITraceWriter traceWriter,
            IGraphDatabase graphDatabase,
            IDirectoryWalkerFactory directoryWalkerFactory,
            string directory,
            IEnumerable<string> excludes
            )
        {
            _traceWriter = traceWriter;
            _graphDatabase = graphDatabase;
            _directoryWalkerFactory = directoryWalkerFactory;
            _directory = directory;
            _excludes = excludes;
        }

        public void Execute()
        {
            var graph = new DataAccess.Graphing.Graph();
            var nodes = new List<INode>();

            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_directory);

                _excludes.ToList().ForEach(x.Exclude);

                x.FileRule(y =>
                {
                    y.Extension("sln");
                    y.Execute(filePath =>
                    {
                        Solution solution;
                        Solution.TryParse(_directory, filePath, out solution);

                        nodes.Add(solution);
                    });
                });

                x.FileRule(y =>
                {
                    y.Extension("csproj");
                    y.Execute(filePath =>
                    {
                        Project project;
                        Project.TryParse(_directory, filePath, out project);

                        nodes.Add(project);
                    });
                });
            });

            walker.TraceAction(_traceWriter.Output);
            walker.Execute();

            _traceWriter.Output("Adding nodes to graph...");
            nodes.ForEach(n => graph.AddNode(n));

            _traceWriter.Output("Computing edge weights");
            graph.ComputeWeights();

            _traceWriter.Output("Saving to database...");
            _graphDatabase.Save(graph);
        }
    }
}