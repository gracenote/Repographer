using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings.Builders.Selectors;
using Repographer.Mono.Options;

namespace Repographer.Modules.Main.Settings.Builders
{
    public class RepoActionSettingsBuilder : ISettingsBuilder
    {
        private readonly IBuilderSelector<RepoActionSettings> _builderSelector;
        private readonly IValidator<RepoActionSettings> _validator;

        private string[] _args;
        private bool _help;

        public RepoActionSettingsBuilder(
            IBuilderSelector<RepoActionSettings> builderSelector,
            IValidator<RepoActionSettings> validator 
            )
        {
            _builderSelector = builderSelector;
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
            var settings = new RepoActionSettings(options);

            settings.Parse(_args);

            SetHelp(settings.Help);

            if (_help && !settings.ActionSpecified)
                return new ShowHelpSettings("Repographer Options", options);
            
            _validator.Validate(settings).ThrowIfAny(options);

            var subBuilder = _builderSelector.GetBuilder(settings);
            
            subBuilder.SetHelp(_help);
            subBuilder.SetArgs(_args);

            return subBuilder.Build();
        }
    }
}