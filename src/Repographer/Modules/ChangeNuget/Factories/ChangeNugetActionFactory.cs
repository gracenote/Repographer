using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.ChangeNuget.Settings;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.ChangeNuget.Factories
{
    public class ChangeNugetActionFactory : IRepoActionFactory<ChangeNugetSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public ChangeNugetActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(ChangeNugetSettings settings)
        {
            return new ChangeNugetAction(_traceWriter, _directoryWalkerFactory, settings.Directory, settings.NewPath);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(ChangeNugetSettings);
        }
    }
}