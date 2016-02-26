using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.Graph.Settings.Builders
{
    public class GraphCompileSettingsBuilder : ISettingsBuilder<GraphSettings>
    {
        private readonly IValidator<GraphCompileSettings> _validator;
        private string[] _args;
        private bool _help;

        public GraphCompileSettingsBuilder(IValidator<GraphCompileSettings> validator)
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
            var settings = new GraphCompileSettings(options);

            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Graph Compile Options", options);

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(GraphSettings repoActionSettings)
        {
            return repoActionSettings.Compile;
        }
    }
}