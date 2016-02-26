#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;

namespace Repographer.Modules.PurgeFolder
{
    public class PurgeFolderAction : IRepoAction
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;
        private readonly string _directory;
        private readonly IEnumerable<string> _includes;
        private readonly IEnumerable<string> _excludes;

        public PurgeFolderAction(
            ITraceWriter traceWriter,
            IDirectoryWalkerFactory directoryWalkerFactory,
            string directory,
            IEnumerable<string> includes,
            IEnumerable<string> excludes
            )
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
            _directory = directory;
            _includes = includes;
            _excludes = excludes;
        }

        public void Execute()
        {
            var walker = _directoryWalkerFactory.New(x =>
            {
                x.Directory(_directory);

                _excludes.ToList().ForEach(x.Exclude);

                _includes.ToList().ForEach(i =>
                {
                    x.DirectoryRule(r =>
                    {
                        r.Name(i);
                        r.Execute(dir => Directory.Delete(dir, true));
                    });
                });
            });

            walker.TraceAction(_traceWriter.Output);
            walker.Execute();
        }
    }
}