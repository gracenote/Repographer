using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.RemoveProject.Settings;

namespace Repographer.Modules.RemoveProject.Factories
{
    public class RemoveProjectActionFactory : IRepoActionFactory<RemoveProjectSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public RemoveProjectActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(RemoveProjectSettings settings)
        {
            return new RemoveProjectAction(_traceWriter, _directoryWalkerFactory, settings.Directory, settings.Name, settings.Excludes);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(RemoveProjectSettings);
        }
    }
}