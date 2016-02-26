using Repographer.Components.Validation;
using Repographer.Modules.Main.Settings.Builders;

namespace Repographer.Modules.Main.Settings
{
    public class RepoActionSettingsFactory : IRepoActionSettingsFactory
    {
        private readonly ISettingsBuilder _settingsBuilder;

        public RepoActionSettingsFactory(ISettingsBuilder settingsBuilder)
        {
            _settingsBuilder = settingsBuilder;
        }

        public IRepoActionSettings Create(string[] args)
        {
            try
            {
                _settingsBuilder.SetArgs(args);

                return _settingsBuilder.Build();
            }
            catch (AggregateValidationException ex)
            {
                return new ShowHelpSettings(ex.ToString(), ex.Set);
            }
        }
    }
}
