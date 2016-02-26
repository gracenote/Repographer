using System;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Exceptions;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;

namespace Repographer.Modules.ChangeNuget
{
    public class ChangeNugetAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;
        private readonly string _directory;
        private readonly string _newPath;

        public ChangeNugetAction(
            ITraceWriter traceWriter, 
            IDirectoryWalkerFactory directoryWalkerFactory, 
            string directory, 
            string newPath)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
            _directory = directory;
            _newPath = newPath;
        }

        public void Execute()
        {
            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_directory);

                x.FileRule(y =>
                {
                    y.Extension("csproj");
                    
                    y.Execute(filePath =>
                    {
                        try
                        {
                            var filePathUri = new Uri(filePath);

                            var project = new Project(filePath);
                        
                            var items = project.Items.Where(item => 
                                item.ItemType == "Reference"
                                && item.Metadata.Any(md => 
                                    md.Name =="HintPath" && md.UnevaluatedValue.Contains("packages")
                                    )
                                ).ToList();

                            items.ForEach(item =>
                            {
                                var hintPathMetadata = item.Metadata.First(md => md.Name == "HintPath");

                                var oldHintPath = hintPathMetadata.UnevaluatedValue;
                            
                                var oldHintPathParts = oldHintPath.Split(
                                    new[] {"packages"},
                                    StringSplitOptions.RemoveEmptyEntries);

                                var newHintPath = _newPath + oldHintPathParts[1];
                                var newHintPathUri = new Uri(newHintPath);

                                var newRelativeHintPathUri = filePathUri.MakeRelativeUri(newHintPathUri);

                                hintPathMetadata.UnevaluatedValue = newRelativeHintPathUri.ToString();
                            
                                _traceWriter.Output(string.Format("Switch: {0} to {1}", oldHintPath, hintPathMetadata.UnevaluatedValue));
                            });

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
