﻿using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.RemoveProject.Settings.Builders
{
    public class RemoveProjectSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly IValidator<RemoveProjectSettings> _validator;
        private string[] _args;
        private bool _help;

        public RemoveProjectSettingsBuilder(IValidator<RemoveProjectSettings> validator)
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

            var settings = new RemoveProjectSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Remove Project Options", options); 

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(RepoActionSettings repoActionSettings)
        {
            return repoActionSettings.RemoveProject;
        }
    }
}