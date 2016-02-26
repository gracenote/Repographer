using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Modules.Main;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.RemoveReference.Settings;

namespace Repographer.Modules.RemoveReference.Factories
{
    public class RemoveReferenceActionFactory : IRepoActionFactory<RemoveReferenceSettings>
    {
        private readonly ITraceWriter _traceWriter;
        private readonly IDirectoryWalkerFactory _directoryWalkerFactory;

        public RemoveReferenceActionFactory(ITraceWriter traceWriter, IDirectoryWalkerFactory directoryWalkerFactory)
        {
            _traceWriter = traceWriter;
            _directoryWalkerFactory = directoryWalkerFactory;
        }

        public IRepoAction Create(RemoveReferenceSettings settings)
        {
            return new RemoveReferenceAction(
                _traceWriter, 
                _directoryWalkerFactory, 
                settings.Directory, 
                settings.Excludes,
                settings.Project,
                settings.ItemName,
                settings.ItemPath
            );
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof(RemoveReferenceSettings);
        }
    }
}