using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.ChangeNuget.Settings.Builders
{
    public class ChangeNugetSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly IValidator<ChangeNugetSettings> _validator;
        private bool _help;
        private string[] _args;

        public ChangeNugetSettingsBuilder(IValidator<ChangeNugetSettings> validator)
        {
            _validator = validator;
        }

        public void SetHelp(bool help)
        {
            _help = help;
        }

        public void SetArgs(string[] args)
        {
            _args = args;
        }

        public IRepoActionSettings Build()
        {
            var options = new OptionSet();

            var settings = new ChangeNugetSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Change NuGet Package Location", options);

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(RepoActionSettings settings)
        {
            return settings.ChangeNuget;
        }
    }
}
