using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings.Builders
{
    public class GraphExportSettingsBuilder : ISettingsBuilder<GraphSettings>
    {
        private readonly IValidator<GraphExportSettings> _validator;

        private string[] _args;
        private bool _help;

        public GraphExportSettingsBuilder(IValidator<GraphExportSettings> validator)
        {
            _validator = validator;
        }

        public void SetArgs(string[] args)
        {
            _args = args;
        }

        public void SetHelp(bool help)
        {
            _help = help;
        }

        public IRepoActionSettings Build()
        {
            var options = new OptionSet();

            var settings = new GraphExportSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Graph Export Options", options);

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(GraphSettings repoActionSettings)
        {
            return repoActionSettings.Export;
        }
    }
}