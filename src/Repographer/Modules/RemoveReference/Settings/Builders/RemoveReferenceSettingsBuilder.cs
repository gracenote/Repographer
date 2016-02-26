using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.RemoveReference.Settings.Builders
{
    public class RemoveReferenceSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly IValidator<RemoveReferenceSettings> _validator;
        private string[] _args;
        private bool _help;

        public RemoveReferenceSettingsBuilder(IValidator<RemoveReferenceSettings> validator)
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

            var settings = new RemoveReferenceSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Remove a Reference", options);

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(RepoActionSettings repoActionSettings)
        {
            return repoActionSettings.RemoveReference;
        }
    }
}