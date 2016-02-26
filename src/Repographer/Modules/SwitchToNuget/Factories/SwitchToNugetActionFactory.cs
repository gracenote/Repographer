#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.SwitchToNuget.Settings;

namespace Repographer.Modules.SwitchToNuget.Factories
{
    public class SwitchToNugetActionFactory : IRepoActionFactory<SwitchToNugetSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public SwitchToNugetActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(SwitchToNugetSettings settings)
        {
            return new SwitchToNugetAction(
                _traceWriter, 
                _directoryWalkerFactory, 
                settings.Directory, 
                settings.Excludes,
                settings.Project,
                settings.ItemName,
                settings.ItemPath,
                settings.PackageName, 
                settings.PackagePath, 
                settings.PackageVersion
            );
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(SwitchToNugetSettings);
        }
    }
}