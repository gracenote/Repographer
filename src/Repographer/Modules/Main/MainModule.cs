#region License
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
#endregion
using Autofac;
using Repographer.Components;
using Repographer.Components.IO;
using Repographer.Components.Validation;
using Repographer.DataAccess.Graphing;
using Repographer.Modules.Main.Factories;
using Repographer.Modules.Main.Factories.Selectors;
using Repographer.Modules.Main.Settings;
using Repographer.Modules.Main.Settings.Builders;
using Repographer.Modules.Main.Settings.Builders.Selectors;
using Repographer.Modules.Main.Settings.Validators;

namespace Repographer.Modules.Main
{
    public class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadComponents(builder);
            LoadSettingsBuilders(builder);
            LoadSettingsValidators(builder);
            LoadActionFactories(builder);
        }

        private void LoadComponents(ContainerBuilder builder)
        {
            builder
                .RegisterType<ConsoleTraceWriter>()
                .As<ITraceWriter>();

            builder
                .RegisterType<TrueCounter>()
                .As<ITrueCounter>();

            builder
                .RegisterType<DirectoryWalkerFactory>()
                .As<IDirectoryWalkerFactory>();

            builder
                .RegisterType<GraphDatabase>()
                .As<IGraphDatabase>();
        }

        private void LoadSettingsBuilders(ContainerBuilder builder)
        {
            builder
                .RegisterType<RepoActionSettingsFactory>()
                .As<IRepoActionSettingsFactory>();

            builder
                .RegisterType<RepoActionSettingsBuilderSelector>()
                .As<IBuilderSelector<RepoActionSettings>>();

            builder
                .RegisterType<RepoActionSettingsBuilder>()
                .As<ISettingsBuilder>();
        }

        private void LoadSettingsValidators(ContainerBuilder builder)
        {
            builder
                .RegisterType<RepoActionSettingsValidator>()
                .As<IValidator<RepoActionSettings>>();
        }

        private void LoadActionFactories(ContainerBuilder builder)
        {
            builder
                .RegisterType<RepoActionFactorySelector>()
                .As<IRepoActionFactorySelector>();
            builder
                .RegisterType<ShowHelpActionFactory>()
                .As<IRepoActionFactory<ShowHelpSettings>>()
                .As<IRepoActionFactory>();
        }
    }
}
