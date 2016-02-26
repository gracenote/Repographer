using Autofac;
using Repographer.Components.Validation;
using Repographer.Modules.Graph.Factories;
using Repographer.Modules.Graph.Settings;
using Repographer.Modules.Graph.Settings.Builders;
using Repographer.Modules.Graph.Settings.Validators;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Modules.Main.Settings.Builders.Selectors;

namespace Repographer.Modules.Graph
{
    public class GraphModule : Module
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
                .RegisterType<GraphSettingsBuilderSelector>()
                .As<IBuilderSelector<GraphSettings>>();

            builder
                .RegisterType<GraphSettingsBuilder>()
                .As<ISettingsBuilder<RepoActionSettings>>();

            builder
                .RegisterType<GraphCompileSettingsBuilder>()
                .As<ISettingsBuilder<GraphSettings>>();

            builder
                .RegisterType<GraphExportSettingsBuilder>()
                .As<ISettingsBuilder<GraphSettings>>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<GraphSettingsValidator>()
                .As<IValidator<GraphSettings>>();

            builder
                .RegisterType<GraphCompileSettingsValidator>()
                .As<IValidator<GraphCompileSettings>>();

            builder
                .RegisterType<GraphExportSettingsValidator>()
                .As<IValidator<GraphExportSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
               .RegisterType<GraphCompileActionFactory>()
               .As<IRepoActionFactory<GraphCompileSettings>>()
               .As<IRepoActionFactory>();

            builder
                .RegisterType<GraphExportActionFactory>()
                .As<IRepoActionFactory<GraphExportSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
