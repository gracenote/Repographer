using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.PurgeFolder.Settings;

namespace Repographer.Modules.PurgeFolder.Factories
{
    public class PurgeFolderActionFactory : IRepoActionFactory<PurgeFolderSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public PurgeFolderActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(PurgeFolderSettings settings)
        {
            return new PurgeFolderAction(_traceWriter, _directoryWalkerFactory, settings.Directory, settings.Includes, settings.Excludes);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(PurgeFolderSettings);
        }
    }
}