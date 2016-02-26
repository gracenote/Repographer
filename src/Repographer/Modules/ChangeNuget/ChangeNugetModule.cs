using Autofac;
using Repographer.Components.Validation;
using Repographer.Modules.ChangeNuget.Factories;
using Repographer.Modules.ChangeNuget.Settings;
using Repographer.Modules.ChangeNuget.Settings.Builders;
using Repographer.Modules.ChangeNuget.Settings.Validators;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;

namespace Repographer.Modules.ChangeNuget
{
    public class ChangeNugetModule : Module
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
                .RegisterType<ChangeNugetSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChangeNugetSettingsValidator>()
                .As<IValidator<ChangeNugetSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChangeNugetActionFactory>()
                .As<IRepoActionFactory<ChangeNugetSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
