using Repographer.Components;
using Repographer.Modules.Main.Settings;

namespace Repographer.Modules.Main.Factories
{
    public class ShowHelpActionFactory : IRepoActionFactory<ShowHelpSettings>
    {
        private readonly ITraceWriter _traceWriter;

        public ShowHelpActionFactory(ITraceWriter traceWriter)
        {
            _traceWriter = traceWriter;
        }

        public IRepoAction Create(ShowHelpSettings repoActionSettings)
        {
            return new ShowHelpAction(_traceWriter, repoActionSettings.Message, repoActionSettings.Arguments);
        }

        public bool IsFactoryFor(IRepoActionSettings repoActionSettings)
        {
            return repoActionSettings.GetType() == typeof (ShowHelpSettings);
        }
    }
}