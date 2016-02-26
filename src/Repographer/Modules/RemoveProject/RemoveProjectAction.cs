using System.Collections.Generic;
using System.Linq;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Components.Parsing;
using Repographer.Modules.Main;

namespace Repographer.Modules.RemoveProject
{
    public class RemoveProjectAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;
        private readonly string _directory;
        private readonly string _projectName;
        private readonly IEnumerable<string> _excludes;

        public RemoveProjectAction(
            ITraceWriter traceWriter,
            IDirectoryWalkerFactory directoryWalkerFactory,
            string directory,
            string projectName,
            IEnumerable<string> excludes
            )
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
            _directory = directory;
            _projectName = projectName;
            _excludes = excludes;
        }

        public void Execute()
        {
            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_directory);

                _excludes.ToList().ForEach(x.Exclude);

                x.FileRule(y =>
                {
                    y.Extension("sln");

                    y.Execute(filePath =>
                    {
                        Solution.RemoveProject(_directory, filePath, _projectName);
                    });
                });
            });

            walker.TraceAction(_traceWriter.Output);
            walker.Execute();
        }
    }
}