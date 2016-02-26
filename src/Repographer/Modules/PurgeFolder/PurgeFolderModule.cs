using Autofac;
using Repographer.Components.Validation;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Modules.PurgeFolder.Factories;
using Repographer.Modules.PurgeFolder.Settings;
using Repographer.Modules.PurgeFolder.Settings.Builders;
using Repographer.Modules.PurgeFolder.Settings.Validators;

namespace Repographer.Modules.PurgeFolder
{
    public class PurgeFolderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadSettingsBuilders(builder);
            LoadSettingsValidators(builder);
            LoadActionFactories(builder);
        }

        private void LoadSettingsBuilders(ContainerBuilder builder)
        {
            builder
                .RegisterType<PurgeFolderSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<PurgeFolderSettingsValidator>()
                .As<IValidator<PurgeFolderSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<PurgeFolderActionFactory>()
                .As<IRepoActionFactory<PurgeFolderSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
