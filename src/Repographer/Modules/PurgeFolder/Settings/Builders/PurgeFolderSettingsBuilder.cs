using Repographer.Components;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Mono.Options;

namespace Repographer.Modules.PurgeFolder.Settings.Builders
{
    public class PurgeFolderSettingsBuilder : ISettingsBuilder<RepoActionSettings>
    {
        private readonly IValidator<PurgeFolderSettings> _validator;
        private string[] _args;
        private bool _help;

        public PurgeFolderSettingsBuilder(IValidator<PurgeFolderSettings> validator)
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

            var settings = new PurgeFolderSettings(options);
            settings.Parse(_args);

            if (_help)
                return new ShowHelpSettings("Purge Folder Options", options); 

            _validator.Validate(settings).ThrowIfAny(options);

            return settings;
        }

        public bool IsBuilderFor(RepoActionSettings repoActionSettings)
        {
            return repoActionSettings.PurgeFolder;
        }
    }
}