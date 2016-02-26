using System;
using System.Linq;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Exceptions;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.ChangeFramework.Settings;
using Repographer.Modules.Main;

namespace Repographer.Modules.ChangeFramework
{
    public class ChangeFrameworkAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;
        private readonly ChangeFrameworkSettings _settings;

        public ChangeFrameworkAction(
            ITraceWriter traceWriter,
            IDirectoryWalkerFactory directoryWalkerFactory,
            ChangeFrameworkSettings settings)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
            _settings = settings;
        }

        public void Execute()
        {
            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_settings.Directory);
                _settings.Excludes.ToList().ForEach(x.Exclude);

                x.FileRule(y =>
                {
                    y.Extension("csproj");

                    y.Execute(filePath =>
                    {
                        try
                        {
                            var frameworks = new[] {"v3.5", "v4.0", "v4.5"};

                            var project = new Project(filePath);

                            var targetFrameworkVersion = project.Properties.FirstOrDefault(p => p.Name == "TargetFrameworkVersion");

                            if (targetFrameworkVersion == null || !frameworks.Contains(targetFrameworkVersion.UnevaluatedValue))
                                return;

                            targetFrameworkVersion.UnevaluatedValue = _settings.TargetFramework;

                            project.Save();
                        }
                        catch (InvalidProjectFileException)
                        {
                            _traceWriter.Output(string.Format("Skipping: {0}", filePath));
                        }
                    });
                });
            });

            walker.Execute();
        }
    }
}
