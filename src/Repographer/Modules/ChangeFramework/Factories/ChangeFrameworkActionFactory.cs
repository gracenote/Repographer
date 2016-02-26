using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.ChangeFramework.Settings;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.ChangeFramework.Factories
{
    public class ChangeFrameworkActionFactory : IRepoActionFactory<ChangeFrameworkSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public ChangeFrameworkActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(ChangeFrameworkSettings settings)
        {
            return new ChangeFrameworkAction(_traceWriter, _directoryWalkerFactory, settings);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(ChangeFrameworkSettings);
        }
    }
}